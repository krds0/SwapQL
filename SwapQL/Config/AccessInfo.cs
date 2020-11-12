using System.Net;

using Salaros.Configuration;

namespace SwapQL.Config
{
    internal class AccessInfo
    {
        public readonly string Kind;
        public readonly IPAddress Host;
        public readonly int Port;
        public readonly string User;
        public readonly string Password;
        public readonly string Databasename;

        public AccessInfo(ConfigSection conf)
        {
            Kind = conf["kind"];
            Host = IPAddress.Parse(conf["host"]);
            Port = int.Parse(conf["port"]);
            User = conf["user"];
            Password = conf["password"];
            Databasename = conf["databasename"];
        }
    }
}
