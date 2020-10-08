using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SwapQL.Config
{
    static class Configuration
    {
        public static IPAddress Host { get; private set; }
        public static int Port { get; private set; }
        public static string User { get; private set; }
        public static string Password { get; private set; }


        ///<summary>
        ///Reads the configuration from "%USERPROFILE%/.SwapQL.conf" and parses its content.
        ///</summary>
        ///<exception cref="System.IO.FileNotFoundException"></exception>
        public async static Task ReadConfig()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            if (!File.Exists($"{path}/.SwapQL.conf"))
                throw new FileNotFoundException("Config file not found!", $"{path}/.SwapQL.conf");
            
            var config = await File.ReadAllLinesAsync($"{path}/.SwapQL.conf", Encoding.UTF8);

            try
            {
                ParseConfig(config);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        ///<summary>
        ///Either assigns all options or throws an exception, if anything did go wrong.
        ///</summary>
        ///<exception cref="System.IO.FileNotFoundException"></exception>
        ///<exception cref="System.MissingFieldException"></exception>
        private static void ParseConfig(string[] config)
        {
            foreach (var configuration in config)
            {
                if (configuration.StartsWith('#') || string.IsNullOrWhiteSpace(configuration))
                    continue;

                var option = configuration.Split('=');
                switch (option[0])
                {
                    case "host":
                        Host = IPAddress.Parse(option[1]);
                        break;
                    
                    case "port":
                        Port = int.Parse(option[1]);
                        break;

                    case "user":
                        User = option[1];
                        break;

                    case "password":
                        Password = option[1];
                        break;

                    default:
                        throw new InvalidDataException($"No option is implemented for {option[0]} with {option[1]}!");
                }
            }

            if (Host is null || Port == 0 || string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                throw new MissingFieldException("At least one option is not set in the config!");
        }
    }
}
