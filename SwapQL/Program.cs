using System;
using System.Collections.Generic;

using AddQL;

using SwapQL.Config;
using SwapQLib;

namespace SwapQL
{
    internal class Program
    {
        static ISwapQL source, target;
        public static Dictionary<string, ISwapQL> Instace;

        enum ExitCode : int
        {
            InvalidConfig = 10,

            SourceConnectionError = 20,
            TargetConnectionError = 21,

            UnsupportedDatabase = 2
        }

        static void Main(string[] args)
        {
            Instace = new Dictionary<string, ISwapQL>()
            {
                {"mysql", new MysqlFunctions() },
                {"postgres", new PostgresFunctions() }
            };

            Console.Title = "SwapQL";

            ReadConfig();

            Connect2Database();

            // ReadMetaData();

            List<string> tableNames = source.GetTableNames();

            foreach (var tableName in tableNames)
            {
                // TODO: reading and inserting should happen in lockstep - read one line,
                //       insert one line. Needs small refactoring but nothing major.

                List<string> insertStatements = source.CreateInserts(tableName);
                foreach (var item in insertStatements)
                {
                    Console.WriteLine(item);
                }

                target.ExecuteInserts(insertStatements);
            }

            Console.ReadLine();
        }

        private static void ReadMetaData()
        {
            PanicAndExit("everything works yay", ExitCode.InvalidConfig);
        }

        private static void Connect2Database()
        {
            Console.WriteLine("Connecting to database...");

            // Source database connection
            try
            {
                var cfg = AccessConfig.Source;
                source = Instace[cfg.Kind];
                source.Connect(cfg.Host, cfg.Port, cfg.Databasename, cfg.User, cfg.Password);
            }
            catch (KeyNotFoundException)
            {
                PanicAndExit($"Unsupported Database system: {AccessConfig.Source.Kind}", ExitCode.UnsupportedDatabase);
            }
            catch (Exception e)
            {
                PanicAndExit($"{e.Message} to source database", ExitCode.SourceConnectionError);
            }


            // Target database connection
            try
            {
                var cfg = AccessConfig.Target;
                target = Instace[cfg.Kind];
                target.Connect(cfg.Host, cfg.Port, cfg.Databasename, cfg.User, cfg.Password);
            }
            catch (KeyNotFoundException)
            {
                PanicAndExit($"Unsupported Database system: {AccessConfig.Target.Kind}", ExitCode.UnsupportedDatabase);
            }
            catch (Exception e)
            {
                PanicAndExit($"{e.Message} to target database", ExitCode.TargetConnectionError);
            }

            Console.WriteLine("Connected to database...");
        }

        private static void ReadConfig()
        {
            Console.WriteLine("Reading configuration...");
            try
            {
                AccessConfig.ReadConfig();
            }
            catch (Exception e)
            {
                PanicAndExit(e.Message, ExitCode.InvalidConfig);
            }
            Console.WriteLine("Configuration read...");
        }

        private static void PanicAndExit(string msg, ExitCode exitCode)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"\n{msg}\nError: {exitCode}:{(int)exitCode}");
            Console.ResetColor();
            Environment.Exit((int)exitCode);
        }
    }
}