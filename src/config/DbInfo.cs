using System.Net;
using Salaros.Configuration;

namespace SwapQL.Config
{
    struct DbInfo
    {
        public readonly IPAddress Host;
        public readonly int Port;
        public readonly string User;
        public readonly string Password;
        public readonly string Databasename;

        public DbInfo(ConfigSection conf)
        {
            Host = IPAddress.Parse(conf["host"]);
            Port = int.Parse(conf["port"]);
            User = conf["user"];
            Password = conf["password"];
            Databasename = conf["databasename"];
        }
    }
}