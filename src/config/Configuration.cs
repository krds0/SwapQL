using System;
using System.IO;
using Salaros.Configuration;

namespace SwapQL.Config
{
    static class Configuration
    {
        public static DbInfo Target { get; private set; }
        public static DbInfo Source { get; private set; }


        ///<summary>
        ///Reads the configuration from "%USERPROFILE%/.SwapQL.conf" and parses its content.
        ///</summary>
        ///<exception cref="System.IO.FileNotFoundException"></exception>
        public static void ReadConfig()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            if (!File.Exists($"{path}/.SwapQL.conf"))
                throw new FileNotFoundException("Config file not found!", $"{path}/.SwapQL.conf");
            
            var parser = new ConfigParser($"{path}/.SwapQL.conf",
                                          new ConfigParserSettings()
                                          {
                                              MultiLineValues = MultiLineValues.Simple | MultiLineValues.AllowEmptyTopSection 
                                          });
            
            Target = new DbInfo(parser["target"]);
            Source = new DbInfo(parser["source"]);
        }
    }
}
