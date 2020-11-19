using System.Data.Common;

using MySql.Data.MySqlClient;

using SwapQLib;

namespace AddQL
{
    public class MysqlFunctions : ISwapQL
    {
        public override DbProviderFactory database => MySqlClientFactory.Instance;
    }
}