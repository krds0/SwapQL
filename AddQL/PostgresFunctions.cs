using System.Data.Common;

using Npgsql;

using SwapQLib;

namespace AddQL
{
    public class PostgresFunctions : ISwapQL
    {
        public override DbProviderFactory database => NpgsqlFactory.Instance;
    }
}
