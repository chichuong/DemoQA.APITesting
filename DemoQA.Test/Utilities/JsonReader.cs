using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace DemoQA.Test.Utilities
{
    public static class JsonReader
    {
        public static T? ReadJsonFile<T>(string relativeFilePath)
        {
            string baseDirectory = AppContext.BaseDirectory;
            string absoluteFilePath = Path.GetFullPath(Path.Combine(baseDirectory, relativeFilePath));

            if (File.Exists(absoluteFilePath))
            {
                var jsonText = File.ReadAllText(absoluteFilePath);
                return JsonConvert.DeserializeObject<T>(jsonText);
            }

            string? assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!string.IsNullOrEmpty(assemblyLocation))
            {
                string projectRootGuess = Path.GetFullPath(Path.Combine(assemblyLocation, @"..\..\.."));
                string pathFromProjectRootGuess = Path.GetFullPath(Path.Combine(projectRootGuess, relativeFilePath));
                if (File.Exists(pathFromProjectRootGuess))
                {
                    var jsonText = File.ReadAllText(pathFromProjectRootGuess);
                    return JsonConvert.DeserializeObject<T>(jsonText);
                }
            }
            throw new FileNotFoundException($"JSON file not found: {relativeFilePath}");
        }
    }
}