using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

using SwapQLib;
using SwapQLib.Config;

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
    }
}