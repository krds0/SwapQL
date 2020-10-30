using System;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using SwapQL.Config;
using Shared_DB_Functions;
using MySql.Data.MySqlClient;
using MYSQL_DB_Functions;

namespace SwapQL
{
    class Program
    {
        enum ExitCodes:int
        {
            InvalidConfig
        }

        static void Main(string[] args)
        {
            Console.Title = "SwapQL";
            /*
            #region Read config
            
            Console.WriteLine("Reading configuration...");
            try
            {
                Configuration.ReadConfig();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                AboutToExit();
                Environment.Exit((int)ExitCodes.InvalidConfig);
            }
            Console.WriteLine("Configuration read...");
            #endregion
            */

            // TODO: connenct to database
            DbProviderFactory test = SharedFunctions.GetProvFact(SharedFunctions.DatabaseKind.Mysql);
            Console.WriteLine("Created Provider Factory");

            Console.WriteLine(MYSQLReader.ReadMYSQLData("Select * from test_table;"));
            Console.WriteLine("Successfully read from MySQL Database.");

            Console.Read();
        }

        [Conditional("DEBUG")]
        private static void AboutToExit()
        {
            Console.Read();
        }
    }
}
