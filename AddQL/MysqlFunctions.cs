using System.Collections.Generic;
﻿using System;
using System.Data.Common;
using System.Globalization;
using MySql.Data.MySqlClient;

using SwapQLib;
using SwapQLib.Config;
using System.Data;

namespace AddQL
{
    public class MysqlFunctions : ISwapQL
    {
        public override DbProviderFactory database => MySqlClientFactory.Instance;

        public override string[] CreateTableStatement()
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
            var tables = Connection.GetSchema("Tables", new[] {null, AccessConfig.Source.Databasename});

            foreach (DataRow table in tables.Rows)
            {
                var table_name = table[2] as string;

                string statement = $"CREATE TABLE {table_name} (";

                DataTable dt = Connection.GetSchema("Columns", new[] {null, AccessConfig.Source.Databasename, table_name});   

                var i = 0;
                var colName = string.Empty;
                var colType = string.Empty;
                for (; i < dt.Rows.Count - 1; i++)
                {
                    colName = dt.Rows[i].Field<string>("column_name");
                    colType = dt.Rows[i].Field<string>("data_type");
                    statement += $"{colName} {colType}";
                    
                    statement += ", ";
                }

                colName = dt.Rows[i].Field<string>("column_name");
                colType = dt.Rows[i].Field<string>("data_type");
                statement += $"{colName} {colType});";     
            }

            return statements.ToArray();

        }

        public override SwapQLConstraint[] GetConstraints()
        {
            var columns = Connection.GetSchema("Columns", new[] {null, AccessConfig.Source.Databasename, null, null});
            
            return base.GetConstraints(columns);
        }
        
        protected override string GetInsertValue(DbDataReader reader, int colIndex)
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

        private static string Quote(string input)
        {
            // Surround strings with single quotes after
            // replacing single quotes in the 'input' string with
            // two single quotes.
            return $"'{input.Replace("'", "''")}'";
        }
    }
}