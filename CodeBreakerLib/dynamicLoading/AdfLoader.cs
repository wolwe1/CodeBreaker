using System;
using System.Collections.Generic;
using System.IO;
using CodeBreakerLib.testHandler;

namespace CodeBreakerLib.dynamicLoading
{
    public class AdfLoader
    {
        public static void SaveToFile(List<TestHistory> testHistories)
        {
            foreach (var testHistory in testHistories)
            {
                WriteBestPerformingAdfsToFile(testHistory);
            }
        }

        private static void WriteBestPerformingAdfsToFile(TestHistory testHistory)
        {
            var testName = testHistory.GetTestName();
            var bestAdfs = testHistory.GetBestAdfs();
            var inputType = testHistory.GetInputType();
            var responseType = testHistory.GetResponseType();
            var numberOfInputs = testHistory.GetNumberOfInputs();
            
            var data = numberOfInputs + "$" + inputType.FullName + "$" + responseType.FullName + "$";
            data += string.Join("$", bestAdfs);

            TryWriteOutputToFile(data, testName);
        }

        private static bool TryWriteOutputToFile(string data,string fileName)
        {
            try
            {
                var filePath = GetFilePath(fileName);
                File.WriteAllText(filePath, data);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to write output to file");
                return false;
            }
            
        }

        private static string GetFilePath(string fileName)
        {
            var projectDir = AppDomain.CurrentDomain.BaseDirectory.Split("bin")[0];
            projectDir = Path.Combine(projectDir, "output");
            projectDir = Path.Combine(projectDir, "adf");
            
            return Path.Combine(projectDir, fileName);
        }
    }
}