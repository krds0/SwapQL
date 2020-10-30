using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Data.Common;
using Shared_Functions_Lib;
using MySQL_Functions_Lib;
using System.Diagnostics;

namespace Testing
{
    class Program
    {
        enum ExitCodes : int
        {
            InvalidConfig
        }

        static void Main(string[] args)
        {
            Console.Title = "SwapQL";
            TestMySQLQuery();
            Console.Read();
        }

        private static void TestMySQLQuery()
        {
            DbProviderFactory test = SharedFunctions.GetProvFact(SharedFunctions.DatabaseKind.Mysql);
            Console.WriteLine("Created Provider Factory");

            Console.WriteLine(MysqlFunctions.ReadMYSQLData("Select * from test_table;"));
            Console.WriteLine("Successfully read from MySQL Database.");
        }

        private static void readConfig()
        {
            #region Read config
            Console.WriteLine("Reading configuration...");
            try
            {
                AccessConfig.ReadConfig();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                AboutToExit();
                Environment.Exit((int)ExitCodes.InvalidConfig);
            }
            Console.WriteLine("Configuration read...");
            #endregion
        }

        [Conditional("DEBUG")]
        private static void AboutToExit()
        {
            Console.Read();
        }
    }
}
