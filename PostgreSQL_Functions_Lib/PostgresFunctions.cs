using MySql.Data.MySqlClient;
using Npgsql;

using SwapQLib;

using System;
using System.Data.Common;
//using Shared_Functions_Lib;

namespace PostgreSQL_Functions_Lib
{
    public class PostgresFunctions : ISwapQL
    {
        public override DbProviderFactory database => throw new NotImplementedException();
    }
}
