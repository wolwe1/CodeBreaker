using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutomaticallyDefinedFunctions.generators.adf;
using AutomaticallyDefinedFunctions.structure.state;
using CodeBreakerLib.settings;
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
            var runNumber = testHistory.GetRunNumber();
            var data = runNumber + "$" + numberOfInputs + "$" + inputType.FullName + "$" + responseType.FullName + "$";
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

        public static List<IStateBasedAdf> ReadFromFile(string mandelbrotGet)
        {
            var filePath = GetFilePath(mandelbrotGet);

            var data = File.ReadAllText(filePath);

            return ParseFileData(data);
        }

        private static List<IStateBasedAdf> ParseFileData(string data)
        {

            var items = data.Split("$");

            var runNumber = int.Parse(items[0]);
            var numberOfInputs = int.Parse(items[1]);
            var inputType = Type.GetType(items[2]);
            var responseType = Type.GetType(items[3]);

            var adfIds = items.Skip(4);
            
            return GenerateAdfs(runNumber,numberOfInputs,inputType,responseType,adfIds);

        }

        private static List<IStateBasedAdf> GenerateAdfs(int runNumber, int numberOfInputs, Type inputType, Type responseType,
            IEnumerable<string> adfIds)
        {
            if (inputType == typeof(string))
            {
                return GenerateAdfs<string>(runNumber,numberOfInputs,responseType,adfIds);
            }
            
            if (inputType == typeof(double))
            {
                return GenerateAdfs<double>(runNumber,numberOfInputs,responseType,adfIds);
            }
            
            if (inputType == typeof(bool))
            {
                return GenerateAdfs<bool>(runNumber,numberOfInputs,responseType,adfIds);
            }
        }
        
        private static List<IStateBasedAdf> GenerateAdfs<T>(int runNumber, int numberOfInputs, Type responseType,
            IEnumerable<string> adfIds) where T : IComparable
        {
            if (responseType == typeof(string))
            {
                return GenerateAdfs<T,string>(runNumber,numberOfInputs,adfIds);
            }
            
            if (responseType == typeof(double))
            {
                return GenerateAdfs<T,double>(runNumber,numberOfInputs,adfIds);
            }
            
            if (responseType == typeof(bool))
            {
                return GenerateAdfs<T,bool>(runNumber,numberOfInputs,adfIds);
            }
        }
        
        private static List<IStateBasedAdf> GenerateAdfs<T, TU>(int runNumber, int numberOfInputs, IEnumerable<string> adfIds) where TU : IComparable where T : IComparable
        {
            var settings = new StateAdfSettings<T, TU>(
                GlobalSettings.MaxFunctionDepth,
                GlobalSettings.MaxMainDepth,
                numberOfInputs,
                GlobalSettings.TerminalChance
            );

            var generator = new StateAdfGenerator<T, TU>(runNumber, settings);

            return adfIds.Select(id => (IStateBasedAdf) generator.GenerateFromId(id)).ToList();
        }
    }
}