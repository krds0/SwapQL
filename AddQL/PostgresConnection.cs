﻿using System;
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
using System.Text.RegularExpressions;

namespace AddQL
{
    public class PostgresConnection : SwapQLConnection
    {
        protected override DbProviderFactory database => NpgsqlFactory.Instance;

        public override string[] SetConstraints(SwapQLConstraint[] constraints)
        {
            var index_to_delete = new List<int>();
            var table_has_primary_key = new Dictionary<string, int>();
            var sql_statement = new List<string>();

            for (var i = 0; i < constraints.Length; i++)
            {
                var constraint = constraints[i];

                if (constraint is SwapQLPrimaryKeyConstraint primary_key_constraint)
                {
                    if (!table_has_primary_key.ContainsKey(primary_key_constraint.table))
                    {
                        table_has_primary_key.Add(primary_key_constraint.table, i);
                        sql_statement.Add($"ALTER TABLE {constraint.table} ADD PRIMARY KEY ({constraint.column})");
                    }
                    else
                    {
                        var primary_key_alter = sql_statement[table_has_primary_key[constraint.table]];
                        var primary_key_columns = Regex.Match(primary_key_alter, @"\((.*)\)").Groups[1];

                        sql_statement.Add($"ALTER TABLE {constraint.table} ADD PRIMARY KEY ({primary_key_columns},{constraint.column})");
                        index_to_delete.Add(table_has_primary_key[constraint.table]);
                        table_has_primary_key[constraint.table] = sql_statement.Count - 1;
                    }
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
                    sql_statement.Add($"ALTER TABLE {check_constraint.table} ADD CONSTRAINT {check_constraint.table}_{check_constraint.column}_check CHECK {check_constraint.check};");
                }
                else if (constraint is SwapQLForeignKeyConstraint foreign_constraint)
                {
                    sql_statement.Add($"ALTER TABLE {foreign_constraint.targetTable} ADD CONSTRAINT {foreign_constraint.constraintName} FOREIGN KEY ({foreign_constraint.targetColumn}) REFERENCES {foreign_constraint.sourceTable} ({foreign_constraint.sourceColumn});");
                }
            }

            for (int i = index_to_delete.Count - 1; i >= 0 ; i--)
                sql_statement.RemoveAt(index_to_delete[i]);

            return sql_statement.ToArray();
        }

        public override string[] SetAtrributeAutoIncrement(SwapQLAutoIncrement[] autoIncrements)
        {
            var sql_statement = new List<string>();

            foreach (var autoIncrement in autoIncrements)
            {
                sql_statement.Add($"CREATE SEQUENCE sequence_{autoIncrement.table}_{autoIncrement.column} START WITH {autoIncrement.startValue};");
                sql_statement.Add($"ALTER TABLE {autoIncrement.table} ALTER COLUMN {autoIncrement.column} SET DEFAULT nextval('sequence_{autoIncrement.table}_{autoIncrement.column}');");
            }

            return sql_statement.ToArray();
        }

        protected override string GetTDataTypeName(string sTypeName) //Find Postgresql Name of Datatype for Create Statements
        {
            Regex varchar = new Regex(@"(CHARACTER VARYING)|((N?VAR)|NCHAR).+");
            Regex character = new Regex(@"CHAR(ACTOR)?.+");
            Regex integer = new Regex(@"INT.+");
            Regex dec = new Regex(@"DECIMAL.+");

            sTypeName = sTypeName.ToUpper();
            switch (sTypeName)
            {
                case "DATETIME": return sTypeName;
                case "DATE":    return sTypeName;
                case "TIMESTAMP": return sTypeName;
                case "INTERVAL": return sTypeName;
                case "TIME": return sTypeName;
                case "YEAR": return sTypeName;
                /*case "CHARACTER": return AddSize(sTypeName);
                case "CHAR": return AddSize(sTypeName);
                case "NCHAR":   //fall through: regular varchar stores utf8
                case "NVARCHAR":
                case "VARCHAR": return AddSize("VARCHAR");
                case "CHARACTER VARYING": return AddSize(sTypeName);*/
                case "TINYTEXT":    // fall through: Only Text type
                case "MEDIUMTEXT": 
                case "LONGTEXT":    
                case "TEXT": return "TEXT";
                case "DOUBLE": return "DOUBLE PRECISION";
                case "FLOAT": return sTypeName;
                case "REAL": return sTypeName;
                case "DECIMAL": return sTypeName;
                case "NUMERIC": return sTypeName;
                case "UNSIGNED INT": return "BIGINT";
                case "INT2": return sTypeName;
                case "INT4": return sTypeName;
                case "INT8": return sTypeName;
                case "TINYINT":     // fall through: Only SMALLINT, INT & BIGINT 
                case "TINY INT":
                case "SMALLINT": return "SMALLINT";
                case "MEDIUMINT":
                case "INT": return "INT"; //PostgreSQL supports SmallINT, INT, and BIGINT
                case "BIGINT": return sTypeName;
                default:
                    string size = Regex.Match(sTypeName, @"\d+").Value;
                    if (integer.IsMatch(sTypeName))
                    {
                        return $"INT";
                    }
                    if (varchar.IsMatch(sTypeName))
                    {                     
                        return $"VARCHAR({size})";
                    }
                    else if (character.IsMatch(sTypeName))
                    {
                        return $"CHAR({size})";
                    }
                    else if (dec.IsMatch(sTypeName))
                    {
                        return $"DECIMAL({size})";
                    }
                    else
                    {
                        try
                        {
                            throw new ArgumentException("Unsupported column type found: " + sTypeName);
                        }
                        catch (Exception e)
                        {
                            return "INT";
                        }
                        
                    }
                    
            }
        }
    }

}
