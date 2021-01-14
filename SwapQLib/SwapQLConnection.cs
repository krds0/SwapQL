using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Net;
using System.Linq;

using SwapQLib.Config;
using System.Globalization;

namespace SwapQLib
{
    public abstract class SwapQLConnection
    {
        protected const string ConnectionString = "Server={0};Port={1};Database={2};Uid={3};Password={4}";
        protected abstract DbProviderFactory database { get; }
        public DbConnection Connection { get; private set; }


        // connects to the database and authenticates the user
        public DbConnection Connect(IPAddress host, int port, string databasename, string user, string password)
        {
            Connection = database.CreateConnection();
            Connection.ConnectionString = string.Format(ConnectionString, host, port, databasename, user, password);
            Connection.Open();

            return Connection;
        }


        // A wrapper function around GetConstraints(DataTable) to override in an derived class
        public virtual SwapQLConstraint[] GetConstraints()
        {
            var columns = Connection.GetSchema("Columns", new[] { AccessConfig.Source.Databasename, null, null, null });

            return GetConstraints(columns);
        }
        public virtual SwapQLConstraint[] GetForeignKeyConstraints()
        {
            var columns = Connection.GetSchema("Foreign Key Columns", new[] { AccessConfig.Source.Databasename });

            return GetForeignKeyConstraints(columns);
        }
        // Extract the information of constraints and create an internal representation for each of them
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
        protected SwapQLConstraint[] GetForeignKeyConstraints(DataTable columns)
        {
            Debug.WriteLine("[GetForeignKeyConstraints]:");

            var constraints = new List<SwapQLConstraint>();

            foreach (DataRow meta in columns.Rows)
            {
                var foreignKey = new SwapQLForeignKeyConstraint(meta[2] as string, meta[10] as string, meta[11] as string, meta[5] as string, meta[6] as string);
                constraints.Add(foreignKey);
            }

            return constraints.ToArray();
        }

        // Given an array of constraints, construct SQL statements from them.
        public virtual string[] SetConstraints(SwapQLConstraint[] constraints)
        {
            throw new NotImplementedException();
        }

        public virtual SwapQLConstraint[] GetCheckConstraint()
        {
            var columns = Connection.GetSchema("IndexColumns", new[] { null, AccessConfig.Source.Databasename });

            if (columns.Rows.Count > 0)
                return null;

            return null;
        }


        // A wrapper function around GetDatabaseStructure(DataTable) to override in an derived class
        public virtual string[] GetDatabaseStructure()
        {
            var createStatements = new List<string>();
            var tables = Connection.GetSchema("Tables", new[] { AccessConfig.Source.Databasename });

            foreach (DataRow table in tables.Rows)
            {
                var tableName = table[2] as string;
                var dt = Connection.GetSchema("Columns", new[] { AccessConfig.Source.Databasename, tableName });

                string statement = $"CREATE TABLE {tableName} ({GetDatabaseStructure(dt)});";
            }

            return createStatements.ToArray();
        }
        // Construct an SQL statement for each column 
        protected string GetDatabaseStructure(DataTable columns)
        {
            var columnDefinition = "";
            string columnName, columnType;

            for (var i = 0; i < columns.Rows.Count - 1; i++)
            {
                columnName = columns.Rows[i].Field<string>("column_name");
                columnType = columns.Rows[i].Field<string>("data_type");

                if (columnType == "varchar")
                    columnType += "(255)";

                columnDefinition += $"{columnName} {columnType}, ";
            }

            columnName = columns.Rows[columns.Rows.Count - 1].Field<string>("column_name");
            columnType = columns.Rows[columns.Rows.Count - 1].Field<string>("data_type");

            if (columnType == "varchar")
                columnType += "(255)";

            columnDefinition += $"{columnName} {columnType}";

            return columnDefinition;
        }

        // Executes the given array of SQL statements
        public virtual void SetDatabaseStructure(string[] createStatements)
        {
            throw new NotImplementedException();
        }



        // Create SQL statements of each datarow on each table
        public string[] GetData(string table)
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
            comm.CommandText = $"SELECT * FROM {table}";
            using (DbDataReader reader = comm.ExecuteReader())
            {
                int fieldCount = reader.FieldCount;

                //Für jede Zeile ein INSERT statement generieren
                string generalStatement = $"INSERT INTO {table} VALUES(";
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

            return statements.ToArray();
        }

        // Executes the given array of SQL statements
        public void SetData(string[] insertStatements)
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



        // A wrapper function around GetTableNames(DataTable) to override in an derived class
        public virtual string[] GetTableNames()
        {
            var tables = Connection.GetSchema("Tables", new[] { Connection.Database });

            return GetTableNames(tables);
        }
        // Extract the names of the tables
        protected string[] GetTableNames(DataTable tables)
        {
            var tableNames = new List<string>();


            foreach (DataRow item in tables.Rows)
            {
                // TODO: consider multiple schemas - so far we only care about the table name
                Console.WriteLine(item[2]);
                tableNames.Add((string)item[2]);
            }

            return tableNames.ToArray();
        }



        protected void PrintDataTypeMappings()
        {
            DataTable dt = Connection.GetSchema("DataTypes");
            List<DataColumn> cols = dt.Columns.AsGeneric().ToList();
            Console.WriteLine($"{cols[0].ColumnName} - {cols[3].ColumnName} - {cols[5].ColumnName}");
            foreach (DataRow item in dt.Rows)
            {
                Console.WriteLine(item[0] + " - " + item[3] + " - " + item[5]);
            }
        }

        protected virtual string GetInsertValue(DbDataReader reader, int colIndex)
        {
            string typeName = reader.GetDataTypeName(colIndex);

            // TODO: figure out correct representation of all data types
            //       for insert statements
            // TODO: figure out how to read unsigned variants of SMALLINT, MEDIUMINT, INT, BIGINT
            switch (typeName)
            {
                case "BIT": return reader.GetFieldValue<UInt64>(colIndex).ToString();
                case "DATE": return reader.GetDateTime(colIndex).ToString(CultureInfo.InvariantCulture);
                case "DATETIME": return reader.GetDateTime(colIndex).ToString(CultureInfo.InvariantCulture);
                case "TIMESTAMP": return reader.GetDateTime(colIndex).ToString(CultureInfo.InvariantCulture);
                case "CHAR":        // fall through
                case "NCHAR":       // fall through
                case "VARCHAR":     // fall through
                case "NVARCHAR":    // fall through
                case "TINYTEXT":    // fall through
                case "TEXT":        // fall through
                case "MEDIUMTEXT":  // fall through
                case "LONGTEXT": return Quote(reader.GetString(colIndex));
                case "DOUBLE": return reader.GetDouble(colIndex).ToString(CultureInfo.InvariantCulture);
                case "FLOAT": return reader.GetFloat(colIndex).ToString(CultureInfo.InvariantCulture);
                case "TINYINT": return reader.GetInt16(colIndex).ToString(CultureInfo.InvariantCulture);
                case "SMALLINT": return reader.GetInt16(colIndex).ToString(CultureInfo.InvariantCulture);
                case "INT": return reader.GetInt32(colIndex).ToString(CultureInfo.InvariantCulture);
                // TODO: does YEAR handling work?
                case "YEAR": return reader.GetInt32(colIndex).ToString(CultureInfo.InvariantCulture);
                case "MEDIUMINT": return reader.GetInt32(colIndex).ToString(CultureInfo.InvariantCulture);
                case "BIGINT": return reader.GetInt64(colIndex).ToString(CultureInfo.InvariantCulture);
                case "DECIMAL": return reader.GetDecimal(colIndex).ToString(CultureInfo.InvariantCulture);
                case "TINY INT": return reader.GetByte(colIndex).ToString();
                default:
                    // case "BLOB":
                    //case "TINYBLOB":
                    //case "MEDIUMBLOB":
                    //case "LONGBLOB":
                    //case "BINARY":
                    //case "VARBINARY":
                    //case "TIME":
                    //case "SET":
                    //case "ENUM":
                    throw new ArgumentException("Unsupported column type found: " + typeName);
            }
        }

        protected static string Quote(string input)
        {
            // Surround strings with single quotes after
            // replacing single quotes in the 'input' string with
            // two single quotes.
            return $"'{input.Replace("'", "''")}'";
        }
    }
}
