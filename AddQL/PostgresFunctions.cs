using System.Data.Common;

using Npgsql;

using SwapQLib;

namespace AddQL
{
    public class PostgresFunctions : ISwapQL
    {
        public override DbProviderFactory database => NpgsqlFactory.Instance;

        protected override string GetInsertValue(DbDataReader reader, int colIndex)
        {
            throw new System.NotImplementedException();
        }
    }

}
