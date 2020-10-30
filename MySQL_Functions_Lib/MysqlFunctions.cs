using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Data.Common;
using Shared_Functions_Lib;

namespace MySQL_Functions_Lib
{
    public class MysqlFunctions
    {
        private static string createConnectionString() //IMPLEMENT dynamic connection string!
        {
            string connectionString = $"SERVER =localhost; DATABASE =sourcetestdb; UID =root; ";
            return connectionString;
        }


        public static string ReadMYSQLData(string query)
        {
            string conStr = createConnectionString();
            MySqlConnection conn = (MySqlConnection)SharedFunctions.createConnection(conStr);
            return SharedFunctions.ReadData(query, conn);
        }
    }
}
