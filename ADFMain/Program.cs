using System;
using System.Linq;
using AutomaticallyDefinedFunctions.exceptions;
using AutomaticallyDefinedFunctions.generators;

namespace ADFMain
{
    class Program
    {
        static void Main(string[] args)
        {
            RunADFGeneration<double>(1);
        }
        
        public static void RunADFGeneration<T>(int runs) where T : IComparable
        {
            var settings = new AdfSettings(2, 3, 5, 65);
            settings.NumberOfFunctions = 5;

            var adfGenerator = new AdfGenerator<T>(0, settings);
            for (var i = 0; i < runs; i++)
            {
                try
                {
                    var adf = adfGenerator.Generate();

                    var results = adf.GetValues().ToList();

                    foreach (var result in results)
                    {
                        Console.WriteLine("Result: " + result);
                    }
                }
                catch (ProgramLoopException e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
                
            }
        }
       
    }
}