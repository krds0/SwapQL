using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            ConnectionError = 20,
            SourceConnectionError = 21,
            TargetConnectionError = 22,
        }

        async static Task Main()
        {
            Instace = new Dictionary<string, SwapQLConnection>()
            {
                {"mysql", new MysqlConnection() },
                {"postgres", new PostgresConnection() }
            };

            Console.Title = "SwapQL";

            ReadConfig();

            await Connect2Database();

            await CreateDatabaseStructure();

            await PopulateDatabase();

            await AddCostraints();

            await AddAutoIncrement();

            PanicAndExit("everything works yay", ExitCode.Success);
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

        private async static Task Connect2Database()
        {
            var errors = new List<string>(2);
            Console.WriteLine("Connecting to database...");

            var sourceConnect = Task.Run(() =>
            {
                var cfg = AccessConfig.Source;

                if (Instace.ContainsKey(cfg.Kind))
                    source = Instace[cfg.Kind];
                else
                    PanicAndExit($"Unsupported Database system: {AccessConfig.Source.Kind}", ExitCode.UnsupportedDatabase);

                try
                {
                    source.Connect(cfg.Host, cfg.Port, cfg.Databasename, cfg.User, cfg.Password);
                }
                catch (Exception e)
                {
                    errors.Add($"SOURCE DATABASE: {e.InnerException.Message}");
                }
            });

            var targetConnect = Task.Run(() =>
            {
                var cfg = AccessConfig.Target;

                if (Instace.ContainsKey(cfg.Kind))
                    target = Instace[cfg.Kind];
                else
                    PanicAndExit($"Unsupported Database system: {AccessConfig.Target.Kind}", ExitCode.UnsupportedDatabase);

                try
                {
                    target.Connect(cfg.Host, cfg.Port, cfg.Databasename, cfg.User, cfg.Password);
                }
                catch (Exception e)
                {
                    errors.Add($"TARGET DATABASE: {e.InnerException.Message}");
                }
            });

            await Task.WhenAll(sourceConnect, targetConnect).ContinueWith(ante =>
            {
                if (errors.Any())
                {
                    PanicAndExit(string.Join("\n", errors), ExitCode.ConnectionError);
                }
            });

            Console.WriteLine("Connected to database...\n");
        }

        private async static Task CreateDatabaseStructure()
        {
            Console.WriteLine("Creating database structure...");
            await Task.Delay(500);

            var comm = target.Connection.CreateCommand();
#if DEBUG
            {
                comm.CommandText = "drop table IF EXISTS abteilung;";
                await comm.ExecuteNonQueryAsync();
                comm.CommandText = "drop table IF EXISTS checks;";
                await comm.ExecuteNonQueryAsync();
                comm.CommandText = "drop table IF EXISTS person;";
                await comm.ExecuteNonQueryAsync();
                comm.CommandText = "drop table IF EXISTS foreigners;";
                await comm.ExecuteNonQueryAsync();
                comm.CommandText = "drop table IF EXISTS Persons;";
                await comm.ExecuteNonQueryAsync();
                comm.CommandText = "drop sequence IF EXISTS sequence_Persons_ID;";
                await comm.ExecuteNonQueryAsync();
            }
#endif

            foreach (var item in source.GetDatabaseStructure(target))
            {
                comm.CommandText = item;
                await comm.ExecuteNonQueryAsync();
            }
        }

        private async static Task PopulateDatabase()
        {
            Console.WriteLine("Inserting data...");
            await Task.Delay(500);

            foreach (var tableName in source.GetTableNames())
            {
                // TODO: reading and inserting should happen in lockstep - read one line,
                //       insert one line. Needs small refactoring but nothing major.

                string[] insertStatements = source.GetData(tableName);

                target.SetData(insertStatements);
            }
        }

        private async static Task AddCostraints()
        {
            Console.WriteLine("Integrating constraints...");
            await Task.Delay(500);

            foreach (var constraints in new[] { source.GetConstraints(), source.GetForeignKeyConstraints() })
            {
                var sql = target.SetConstraints(constraints);

                foreach (var item in sql)
                {
                    Console.WriteLine(item);

                    var comm = target.Connection.CreateCommand();
                    comm.CommandText = item;
                    await comm.ExecuteNonQueryAsync();
                }
            }
        }

        private async static Task AddAutoIncrement()
        {
            Console.WriteLine("Integrating auto_increment...");
            await Task.Delay(500);

            var columns_with_autoIncrement = source.GetAtrributeAutoIncrement();
            var sql_statements = target.SetAtrributeAutoIncrement(columns_with_autoIncrement);

            foreach (var sql in sql_statements)
            {
                Console.WriteLine(sql);

                var comm = target.Connection.CreateCommand();
                comm.CommandText = sql;
                await comm.ExecuteNonQueryAsync();
            }
        }


        private static void PanicAndExit(string msg, ExitCode exitCode)
        {
            Console.ForegroundColor = exitCode == ExitCode.Success ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Error.WriteLine($"\n{msg}\n{(exitCode == ExitCode.Success ? "" : "Error: ")}{exitCode}:{(int)exitCode}");
            Environment.Exit((int)exitCode);
        }
    }
}