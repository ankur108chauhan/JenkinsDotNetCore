using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace JenkinsDotNetCore.Utils
{
    public class ConfigUtil
    {
        private static IConfiguration configuration;
        public static string GetAppSettings(string label)
        {
            string path = GetProjectRootDirectory() + "appsettings.json";
            configuration = new ConfigurationBuilder().AddJsonFile(path).Build();
            return configuration[label];
        }

        public static string GetDataManagerTestData(string fileName)
        {
            string path = Path.Combine(GetProjectRootDirectory(), "Testdata", "DataManagers");
            string jsonPath = path + "\\" + fileName + ".json";
            return File.ReadAllText(jsonPath);
        }

        public static string GetTestData(string fileName)
        {
            string path = Path.Combine(GetProjectRootDirectory(), "Testdata");
            string jsonPath = path + "\\" + fileName + ".json";
            return File.ReadAllText(jsonPath);
        }

        public static string GetProjectRootDirectory()
        {
            string currentDirectory = Environment.CurrentDirectory;
            return currentDirectory.Split("bin")[0];
        }
    }
}
