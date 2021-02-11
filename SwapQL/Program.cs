using System;
using System.Collections.Generic;

using AddQL;

using SwapQLib;
using SwapQLib.Config;

namespace SwapQL
{
    class Program
    {
        static SwapQLConnection source, target;
        public static Dictionary<string, SwapQLConnection> Instace;

        enum ExitCode : int
        {
            Success = 0,
            UnsupportedDatabase = 2,

            InvalidConfig = 10,

            SourceConnectionError = 20,
            TargetConnectionError = 21,
        }

        static void Main()
        {
            Instace = new Dictionary<string, SwapQLConnection>()
            {
                {"mysql", new MysqlConnection() },
                {"postgres", new PostgresConnection() }
            };

            Console.Title = "SwapQL";

            ReadConfig();

            Connect2Database();

            ReadMetaData();

            CreateDatabaseStructure();

            PopulateDatabase();

            AlterAddCostraints();

            AlterAddAutoIncrement();

            PanicAndExit("everything works yay", ExitCode.Success);
        }

        private static void AlterAddAutoIncrement()
        {
            Console.WriteLine("Integrating auto_increment...");
            System.Threading.Thread.Sleep(500);

            var columns_with_autoIncrement = source.GetAtrributeAutoIncrement();
            var sql_statements = target.SetAtrributeAutoIncrement(columns_with_autoIncrement);

            foreach (var sql in sql_statements)
            {
                Console.WriteLine(sql);

                var comm = target.Connection.CreateCommand();
                comm.CommandText = sql;
                comm.ExecuteNonQuery();
            }
        }

        private static void AlterAddCostraints()
        {
            Console.WriteLine("Integrating constraints...");
            System.Threading.Thread.Sleep(500);

            foreach (var constraints in new[] { source.GetConstraints(), source.GetForeignKeyConstraints() })
            {
                var sql = target.SetConstraints(constraints);

                foreach (var item in sql)
                {
                    Console.WriteLine(item);

                    var comm = target.Connection.CreateCommand();
                    comm.CommandText = item;
                    comm.ExecuteNonQuery();
                }
            }
        }

        private static void PopulateDatabase()
        {
            Console.WriteLine("Inserting data...");
            System.Threading.Thread.Sleep(500);

            string[] tableNames = source.GetTableNames();
            foreach (var tableName in tableNames)
            {
                // TODO: reading and inserting should happen in lockstep - read one line,
                //       insert one line. Needs small refactoring but nothing major.

                string[] insertStatements = source.GetData(tableName);

                target.SetData(insertStatements);
            }
        }

        private static void CreateDatabaseStructure()
        {
            Console.WriteLine("Creating database structure...");
            System.Threading.Thread.Sleep(500);

            var comm = target.Connection.CreateCommand();

            comm.CommandText = "drop table IF EXISTS abteilung;";
            comm.ExecuteNonQuery();
            comm.CommandText = "drop table IF EXISTS checks;";
            comm.ExecuteNonQuery();
            comm.CommandText = "drop table IF EXISTS person;";
            comm.ExecuteNonQuery();
            comm.CommandText = "drop table IF EXISTS foreigners;";
            comm.ExecuteNonQuery();
            comm.CommandText = "drop table IF EXISTS Persons;";
            comm.ExecuteNonQuery();
            comm.CommandText = "drop sequence IF EXISTS sequence_Persons_ID;";
            comm.ExecuteNonQuery();

            var create_statements = source.GetDatabaseStructure(target);

            foreach (var item in create_statements)
            {
                comm.CommandText = item;
                comm.ExecuteNonQuery();
            }
        }

        private static void ReadMetaData()
        {
            Console.WriteLine("Reading Metadata...");
            System.Threading.Thread.Sleep(500);
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

            Console.WriteLine("Connected to database...\n");
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
            Console.WriteLine("Configuration read...\n");
        }

        private static void PanicAndExit(string msg, ExitCode exitCode)
        {
            Console.ForegroundColor = exitCode == ExitCode.Success ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Error.WriteLine($"\n{msg}\n{(exitCode == ExitCode.Success ? "" : "Error: ")}{exitCode}:{(int)exitCode}");
            Console.ResetColor();
            Environment.Exit((int)exitCode);
        }
    }
}