using System.Collections.Generic;
using System;
using System.Data.Common;
using System.Globalization;
using MySql.Data.MySqlClient;

using SwapQLib;
using SwapQLib.Config;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;

namespace AddQL
{
    public class MysqlConnection : SwapQLConnection
    {
        protected override DbProviderFactory database => MySqlClientFactory.Instance;

        public override string[] GetDatabaseStructure(SwapQLConnection tConnection)
        {
            var createStatements = new List<string>();
            var tables = Connection.GetSchema("Tables", new[] { null, AccessConfig.Source.Databasename });

            foreach (DataRow table in tables.Rows)
            {
                var tableName = table[2] as string;
                var dt = Connection.GetSchema("Columns", new[] { null, AccessConfig.Source.Databasename, tableName });

                string statement = $"CREATE TABLE {tableName} ({GetDatabaseStructure(dt, tConnection)});";
                createStatements.Add(statement);
            }

            return createStatements.ToArray();
        }

        public override SwapQLConstraint[] GetConstraints()
        {
            var check_constraints = new List<SwapQLCheckConstraint>();
            foreach (var table in GetTableNames())
            {
                var comm = Connection.CreateCommand();
                comm.CommandText = $"show create table {table};";

                var create_table_sql = string.Empty;
                var create_table_sql_reader = comm.ExecuteReader();

                if (create_table_sql_reader.Read())
                {
                    create_table_sql = create_table_sql_reader.GetString(1);

                    var regex_check_contraints = Regex.Match(create_table_sql, "CHECK \\((.*)\\)");
                    var column = Regex.Match(regex_check_contraints.Value, "`(.*)`").Groups[1].Value;

                    check_constraints.Add(new SwapQLCheckConstraint(table, column, regex_check_contraints.Groups[1].Value.Replace('`', ' ')));
                }

                create_table_sql_reader.Close();
            }

            var columns = Connection.GetSchema("Columns", new[] { null, AccessConfig.Source.Databasename });

            return check_constraints.Concat(GetConstraints(columns)).ToArray();
        }
        public override SwapQLConstraint[] GetForeignKeyConstraints()
        {
            var columns = Connection.GetSchema("Foreign Key Columns", new[] { null, AccessConfig.Source.Databasename });

            return GetForeignKeyConstraints(columns);
        }

        public override SwapQLAutoIncrement[] GetAtrributeAutoIncrement()
        {
            var tables = Connection.GetSchema("Columns", new[] { null, Connection.Database });

            return GetAtrributeAutoIncrement(tables);
        }

        public override string[] GetTableNames()
        {
            var tables = Connection.GetSchema("Tables", new[] { null, Connection.Database });

            return GetTableNames(tables);
        }

        protected override string GetTDataTypeName(string sTypeName) //Find MySQL Name for SQL Datatype
        {
            sTypeName = sTypeName.ToUpper();
            switch (sTypeName)
            {
                case "DATE": return sTypeName;
                case "DATETIME": return sTypeName;
                case "TIMESTAMP": return sTypeName;
                case "INTERVAL":    // fall through: timespans saved as time
                case "TIME": return "TIME";
                case "YEAR": return sTypeName;

                //CHARACTER TYPES
                case "CHARACTER": return AddSize(sTypeName);
                case "CHAR": return AddSize(sTypeName);
                case "NCHAR": return AddSize(sTypeName);
                case "NVARCHAR": return AddSize(sTypeName);
                case "CHARACTER VARYING": return AddSize(sTypeName);
                case "VARCHAR": return AddSize(sTypeName);
                case "TINYTEXT": return AddSize(sTypeName);
                case "MEDIUMTEXT": return AddSize(sTypeName);
                case "LONGTEXT": return AddSize(sTypeName);
                case "TEXT": return AddSize(sTypeName);
                //NUMERIC TYPES
                case "DOUBLE": return sTypeName;
                case "FLOAT": return sTypeName;
                case "REAL": return sTypeName;
                case "DOUBLE PRECISION": return sTypeName;
                case "DECIMAL": return sTypeName;
                case "NUMERIC": return sTypeName;
                case "INT2": return sTypeName;
                case "INT4": return sTypeName;
                case "INT8": return sTypeName;
                case "TINYINT": return sTypeName;
                case "TINY INT": return sTypeName;
                case "SMALLINT": return sTypeName;
                case "MEDIUMINT": return sTypeName;
                case "INT": return sTypeName;
                case "BIGINT": return sTypeName;
                default:
                    throw new ArgumentException("Unsupported column type found: " + sTypeName);
            }
        }

        protected string AddSize(string typename)
        {
            //TODO: ADD ACTUAL SIZE
            return $"{typename}(20)";
        }


        //TODO: AddPrecision Function
    }
}