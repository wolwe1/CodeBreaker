using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.generators;
using CodeBreakerLib.connectors;
using CodeBreakerLib.coverage.calculators;
using CodeBreakerLib.fitnessFunctions;
using CodeBreakerLib.testHandler;
using CodeBreakerLib.testHandler.setup;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;

namespace CodeBreaker
{
    class Program
    {
        private static void Test()
        {
            var testHandler = new TestHandler(new TestFileDescriptorStrategy());
            testHandler.Setup();
            
            List<List<object>> parameters = new List<List<object>>();

            var adfGenerator = new ADFGenerator<string>(9, 65, 3, 5);
            var numAdfGenerator = new ADFGenerator<double>(2, 65, 3, 5);
            
            var adf = adfGenerator.Generate();
            var adf2 = adfGenerator.Generate();
            var numAdf = numAdfGenerator.Generate();
            
            var output1 = adf.GetValue();
            var output2 = adf2.GetValue();
            var numOutput1 = (int)numAdf.GetValue();

            parameters.Add(new List<object>(){output1});
            parameters.Add(new List<object>(){output2});
            parameters.Add(new List<object>(){numOutput1});

            // var results = testHandler.RunAllTests(parameters);
            // Console.WriteLine($"Sent {output1} - received {results[0]}");
            // Console.WriteLine($"Sent {output2} - received {results[1]}");
            // Console.WriteLine($"Sent {numOutput1} - received {results[2]}");
        }

        private static IPopulationMember<T> CreateAdf<T>(int seed) where T : IComparable
        {
            var populationGenerator = new ADFPopulationGenerator<T>(seed,  65,  3,  7);

            return populationGenerator.GenerateNewMember();
        }

        private static void TestString()
        {
            var stringAdf1 = CreateAdf<string>(6);
            var stringAdf2 = CreateAdf<string>(5);
            
            Console.WriteLine(stringAdf1.GetResult());
            Console.WriteLine(stringAdf2.GetResult());
            
            TestHandler handler = new TestHandler(new TestFileDescriptorStrategy());
            handler.Setup();
            
            var test = handler.GetNextTest();
            var fitnessFunction =
                new CompositeFitnessFunction().AddEvaluation(new CodeCoverageFitnessFunction(test, new StatementCoverageCalculator()), 1);
            
            fitnessFunction.Evaluate(stringAdf1);
            test = handler.GetNextTest();
            fitnessFunction =
                new CompositeFitnessFunction().AddEvaluation(new CodeCoverageFitnessFunction(test, new StatementCoverageCalculator()), 1);

            fitnessFunction.Evaluate(stringAdf2);
        }
        static void Main(string[] args)
        {
            //TestString();
            
            var testHandler = new TestHandler(new TestFileDescriptorStrategy());
            testHandler.Setup();
            testHandler.RunAllTests();
        }
    }
}