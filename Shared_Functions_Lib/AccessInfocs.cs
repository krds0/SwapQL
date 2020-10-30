using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Salaros.Configuration;

namespace Shared_Functions_Lib
{
    public struct AccessInfo
    {
        public readonly IPAddress Host;
        public readonly int Port;
        public readonly string User;
        public readonly string Password;
        public readonly string Databasename;

        public AccessInfo(ConfigSection conf)
        {
            Host = IPAddress.Parse(conf["host"]);
            Port = int.Parse(conf["port"]);
            User = conf["user"];
            Password = conf["password"];
            Databasename = conf["databasename"];
        }
    }
}
