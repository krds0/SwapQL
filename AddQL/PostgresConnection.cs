using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Net;
using System.Linq;

using SwapQLib.Config;
using System.Globalization;

using Npgsql;

using SwapQLib;

namespace AddQL
{
    public class PostgresConnection : SwapQLConnection
    {
        protected override DbProviderFactory database => NpgsqlFactory.Instance;

        public override string[] SetConstraints(SwapQLConstraint[] constraints)
        {
            var sql_statement = new List<string>();

            foreach (var constraint in constraints)
            {
                if (constraint is SwapQLPrimaryKeyConstraint primary_key_constraint)
                {
                    sql_statement.Add($"ALTER TABLE {constraint.table} ADD PRIMARY KEY ({constraint.column})");
                }
                else if (constraint is SwapQLUniqueConstraint unique_constraint)
                {
                    sql_statement.Add($"ALTER TABLE {constraint.table} ADD UNIQUE ({constraint.column})");
                }
                else if (constraint is SwapQLNullConstraint null_constraint)
                {
                    sql_statement.Add($"ALTER TABLE {constraint.table} ALTER {constraint.column} SET NOT NULL");
                }
                else if (constraint is SwapQLCheckConstraint check_constraint)
                {
                    sql_statement.Add($"ALTER TABLE {check_constraint.table} ADD CHECK (char_length(zipcode) = 5);");
                }
                else if (constraint is SwapQLForeignKeyConstraint foreign_constraint)
                {
                    sql_statement.Add($"ALTER TABLE {foreign_constraint.targetTable} ADD CONSTRAINT {foreign_constraint.constraintName} FOREIGN KEY ({foreign_constraint.targetColumn}) REFERENCES {foreign_constraint.sourceTable} ({foreign_constraint.sourceColumn});");
                }
            }

            return sql_statement.ToArray();
        }

        public override string[] SetAtrributeAutoIncrement(SwapQLAutoIncrement[] autoIncrements)
        {
            var sql_statement = new List<string>();

            foreach (var autoIncrement in autoIncrements)
            {
                sql_statement.Add($"CREATE SEQUENCE sequence_{autoIncrement.table}_{autoIncrement.column};");
                sql_statement.Add($"ALTER TABLE {autoIncrement.table} ALTER COLUMN {autoIncrement.column} SET DEFAULT nextval('sequence_{autoIncrement.table}_{autoIncrement.column}');");
            }

            return sql_statement.ToArray();
        }

        protected override string GetTDataTypeName(string sTypeName) //Find Postgresql Name of Datatype for Create Statements
        {
            sTypeName = sTypeName.ToUpper();
            switch (sTypeName)
            {
                case "DATETIME":
                case "TIMESTAMP": return "TIMESTAMP";
                case "CHAR":        // fall through
                case "NCHAR":       // fall through
                case "VARCHAR": return AddSize(sTypeName);
                case "NVARCHAR": return AddSize("VARCHAR");
                case "TINYTEXT":    // fall through
                case "MEDIUMTEXT":  // fall through
                case "LONGTEXT":    // fall through
                case "TEXT": return "TEXT"; //Postgresql only has the "Text" Datatype
                case "DOUBLE": return "DOUBLE PRECISION";
                case "FLOAT": return sTypeName;
                case "DECIMAL": return sTypeName;
                case "NUMERIC": return sTypeName;
                case "TINYINT":
                case "TINY INT":
                case "SMALLINT": return "SMALLINT";
                case "MEDIUMINT":
                case "INT": return "INT"; //PostgreSQL supports SmallINT, INT, and BIGINT
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
    }

}
