using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using MySql.Data.MySqlClient;

using SwapQLib;
using SwapQLib.Config;

namespace AddQL
{
    public class MysqlFunctions : ISwapQL
    {
        public override DbProviderFactory database => MySqlClientFactory.Instance;

        public override SwapQLConstraint[] GetConstraints()
        {
            var columns = Connection.GetSchema("Columns", new[] {null, AccessConfig.Source.Databasename, null, null});
            
            return base.GetConstraints(columns);
        }
    }
}