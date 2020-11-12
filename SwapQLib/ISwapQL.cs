using System.Collections.Generic;
using System.Data.Common;
using System.Net;

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
    }
}
