using System.Collections.Generic;
using System;
using System.Data.Common;
using System.Globalization;
using MySql.Data.MySqlClient;

using SwapQLib;
using SwapQLib.Config;
using System.Data;

namespace AddQL
{
    public class MysqlConnection : SwapQLConnection
    {
        protected override DbProviderFactory database => MySqlClientFactory.Instance;

        public override string[] GetDatabaseStructure(SwapQLConnection tConnection)
        {
            var createStatements = new List<string>();
            var tables = Connection.GetSchema("Tables", new[] {null, AccessConfig.Source.Databasename });
            
            foreach (DataRow table in tables.Rows)
            {
                var tableName = table[2] as string;
                var dt = Connection.GetSchema("Columns", new[] {null, AccessConfig.Source.Databasename, tableName });

                string statement = $"CREATE TABLE {tableName} ({GetDatabaseStructure(dt, tConnection)});";
                createStatements.Add(statement);
            }

            return createStatements.ToArray();
        }
        
        public override SwapQLConstraint[] GetConstraints()
        {
            var columns = Connection.GetSchema("Columns", new[] {null, AccessConfig.Source.Databasename });

            return GetConstraints(columns);
        }
        public override SwapQLConstraint[] GetForeignKeyConstraints()
        {
            var columns = Connection.GetSchema("Foreign Key Columns", new[] { null, AccessConfig.Source.Databasename });

            return GetForeignKeyConstraints(columns);
        }

        public override string[] GetTableNames()
        {
            var tables = Connection.GetSchema("Tables", new[] {null, Connection.Database });

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