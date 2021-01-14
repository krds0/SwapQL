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

        public override string[] GetTableNames()
        {
            var tables = Connection.GetSchema("Tables", new[] {null, Connection.Database });

            return GetTableNames(tables);
        }

        protected override string GetTDataTypeName(string sDatatTypeName)
        {
            throw new NotImplementedException();
        }
    }
}