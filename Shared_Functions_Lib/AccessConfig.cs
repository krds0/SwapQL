using System;
using System.IO;
using Salaros.Configuration;

namespace Shared_Functions_Lib
{
    public static class AccessConfig
    {
        public static AccessInfo Target { get; private set; }
        public static AccessInfo Source { get; private set; }

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

            Target = new AccessInfo(parser["target"]);
            Source = new AccessInfo(parser["source"]);
        }
    }
}
