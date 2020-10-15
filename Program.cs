﻿using System;
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

        static async Task Main(string[] args)
        {
            Console.Title = "SwapQL";
            /*
            #region Read config
            
            Console.WriteLine("Reading configuration...");
            try
            {
                await Configuration.ReadConfig();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                AboutToExit();
                Environment.Exit((int)ExitCodes.InvalidConfig);
            }
            Console.WriteLine("Configuration read...");
            Console.WriteLine($"Using options:\n\thost = {Configuration.Host}:{Configuration.Port}\n\tuser = {Configuration.User}\n\tpassword = {Configuration.Password}");
            #endregion
            */

            // TODO: connenct to database
            MySqlClientFactory test = (MySqlClientFactory)SharedFunctions.GetProvFact(SharedFunctions.DatabaseKind.Mysql);
            Console.WriteLine("Created Mysql Provider Factory");

            Console.WriteLine(MYSQLReader.ReadData("Select * from test_table;"));
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
