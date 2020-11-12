using MySql.Data.MySqlClient;
using Npgsql;

using SwapQLib;

using System;
using System.Data.Common;

namespace MySQL_Functions_Lib
{
    public class MysqlFunctions : ISwapQL
    {
        //private static string createConnectionString() //IMPLEMENT dynamic connection string!
        //{
        //    string connectionString = $"SERVER =localhost; DATABASE =sourcetestdb; UID =root; ";
        //    return connectionString;
        //}


        //public static string ReadMYSQLData(string query)
        //{
        //    string conStr = createConnectionString();
        //    //MySqlConnection conn = (MySqlConnection)SharedFunctions.createConnection(conStr);
        //    //return SharedFunctions.ReadData(query, conn);
        //    return null;
        //}
        public override DbProviderFactory database => MySqlClientFactory.Instance;

        public MysqlFunctions()
        {
            //Instace.Add("mysql", this);
        }
    }
}
