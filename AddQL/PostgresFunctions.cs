using System;
using System.Collections.Generic;
using System.Data.Common;

using Npgsql;

using SwapQLib;

namespace AddQL
{
    public class PostgresFunctions : ISwapQL
    {
        public override DbProviderFactory database => NpgsqlFactory.Instance;

        public override SwapQLConstraint[] GetConstraints()
        {
            throw new NotImplementedException();
        }

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
            }

            return sql_statement.ToArray();
        }

        protected override string GetInsertValue(DbDataReader reader, int colIndex)
        {
            throw new System.NotImplementedException();
        }
    }

}
