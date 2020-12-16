using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Net;

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
    }
}
