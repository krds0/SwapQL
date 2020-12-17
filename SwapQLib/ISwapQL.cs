using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Net;
using System.Linq;

using SwapQLib.Config;

namespace SwapQLib
{
    public abstract class ISwapQL
    {
        public const string ConnectionString = "Server={0};Port={1};Database={2};Uid={3};Password={4}";

        public abstract DbProviderFactory database { get; }
        public DbConnection Connection { get; private set; }

        public DbConnection Connect(IPAddress host, int port, string databasename, string user, string password)
        {
            Connection = database.CreateConnection();
            Connection.ConnectionString = string.Format(ConnectionString, host, port, databasename, user, password);
            Connection.Open();

            return Connection;
        }

        public virtual string[] CreateTableStatement()
        {
            /*
            CREATE TABLE Persons (
            PersonID int,
            LastName varchar(255),
            FirstName varchar(255),
            Address varchar(255),
            City varchar(255)
            );
            */

            var statements = new List<string>();
            var tables = Connection.GetSchema("Tables", new[] {AccessConfig.Source.Databasename});

            foreach (var table in tables.Rows)
            {
                string statement = $"CREATE TABLE {true} (";

                DataTable dt = Connection.GetSchema("Columns", new[] {AccessConfig.Source.Databasename});   

                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    string colName = dt.Rows[i].Field<string>("column_name");
                    string colType = dt.Rows[i].Field<string>("data_type");
                    statement += $"{colName} {colType}";
    
                    statement += ", ";
                }
                statement += ");";   
            }

            return statements.ToArray();
        }

        // retrives metadata from DB and calls its overloeaded method
        public virtual SwapQLConstraint[] GetConstraints()
        {
            var columns = Connection.GetSchema("Columns", new[] {AccessConfig.Source.Databasename, null, null, null});

            return GetConstraints(columns);
        }

        // constracts constraints using the metadata from DB
        protected SwapQLConstraint[] GetConstraints(DataTable columns)
        {
            Debug.WriteLine("[GetConstraints]:");

            var constraints = new List<SwapQLConstraint>();

            foreach (DataRow meta in columns.Rows)
            {
                var table_name = meta[2] as string;
                var column_name = meta[3] as string;
                var is_nullable = meta[6] as string;

                Debug.WriteLine($"\t{meta[2]}.{meta[3]}");

                // primary / unique
                switch (meta[15])
                {
                    case "PRI":
                        Debug.WriteLine($"\t\tis PRIMARY KEY");
                        constraints.Add(new SwapQLPrimaryKeyConstraint(table_name, column_name));
                        break;
                    
                    case "MUL":
                        Debug.WriteLine($"\t\tis UNIQUE");
                        constraints.Add(new SwapQLUniqueConstraint(table_name, column_name));
                        break;
                }

                if (is_nullable == "NO")
                {
                    Debug.WriteLine($"\t\tis NOT NULL");
                    constraints.Add(new SwapQLNullConstraint(table_name, column_name));
                }
            }

            return constraints.ToArray();
        }
    
        public virtual string[] SetConstraints(SwapQLConstraint[] constraints)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Helper method to print the mappings from database type names to
        /// C# type names.
        /// </summary>
        public void PrintDataTypeMappings()
        {
            DataTable dt = Connection.GetSchema("DataTypes");
            List<DataColumn> cols = dt.Columns.AsGeneric().ToList();
            Console.WriteLine($"{cols[0].ColumnName} - {cols[3].ColumnName} - {cols[5].ColumnName}");
            foreach (DataRow item in dt.Rows)
            {
                Console.WriteLine(item[0] + " - " + item[3] + " - " + item[5]);
            }
        }

        public List<string> GetTableNames()
        {
            var tableNames = new List<string>();
            string[] restrictions = new string[] { null, Connection.Database, null, null };
            DataTable dt = Connection.GetSchema("Tables", restrictions);
            List<DataColumn> cols = dt.Columns.AsGeneric().ToList();
            foreach (DataRow item in dt.Rows)
            {
                // TODO: consider multiple schemas - so far we only care about the table name
                Console.WriteLine(item[2]);
                tableNames.Add((string)item[2]);
            }

            return tableNames;
        }

        public List<string> CreateInserts(string tblName)
        {
            //Läuft für jede Tabelle einmal
            //Datentypen und Spaltennamen einlesen
            //Für jede Zeile ein Insert
            //Datentypen: Für ''
            //Datareader Select * ansehen

            List<string> statements = new List<string>();

            ////// Die Spalten der angegeben Tabelle bekommen
            ////string[] restrictions = new string[] { null, null, tblName, null };
            ////DataTable dt = Connection.GetSchema("Columns", restrictions);
            ////List<DataColumn> cols = dt.Columns.AsGeneric().ToList();

            //////Den Datentyp jeder Spalte auslesen
            ////List<string> colTypes = new List<string>();
            ////foreach (var item in cols)
            ////{
            ////    string datatype = item.DataType.Name;
            ////    colTypes.Add(datatype);
            ////}
            
            //Alle Zeilen der Quell-Tabelle lesen
            var comm = Connection.CreateCommand();
            comm.CommandText = $"SELECT * FROM {tblName}";
            using (DbDataReader reader = comm.ExecuteReader())
            {
                int fieldCount = reader.FieldCount;

                //Für jede Zeile ein INSERT statement generieren
                string generalStatement = $"INSERT INTO {tblName} VALUES(";
                while (reader.Read())
                {
                    string statement = generalStatement;

                    //Alle Spalten der Zeile durchgehen
                    for (int colIndex = 0; colIndex < fieldCount; colIndex++)
                    {
                        statement += GetInsertValue(reader, colIndex); //Je nach Datentyp das insert statement ändern
                        if (colIndex != fieldCount - 1) //Checken, ob letzte Spalte in Zeile
                        {
                            statement += ", ";
                        }
                    }
                    statement += ");";
                    statements.Add(statement);
                }

                reader.Close();
            }

            return statements;
        }

        public void ExecuteInserts(List<string> insertStatements)
        {
            var comm = Connection.CreateCommand();

            foreach (var insert in insertStatements)
            {
                comm.CommandText = insert;
                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                    throw new InvalidOperationException($"INSERT operation returned incorrect value {rowsAffected}. Expected 1.");
            }
        }

        protected abstract string GetInsertValue(DbDataReader reader, int colIndex);
    }
}
