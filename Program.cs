using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using SwapQL.Config;

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

            // TODO: connenct to database

            Console.Read();
        }

        [Conditional("DEBUG")]
        private static void AboutToExit()
        {
            Console.Read();
        }
    }
}
