using System;
using System.Linq;
using AutomaticallyDefinedFunctions.exceptions;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.state;
using AutomaticallyDefinedFunctions.structure;
using AutomaticallyDefinedFunctions.structure.functions.boolean;
using AutomaticallyDefinedFunctions.structure.functions.comparators;
using AutomaticallyDefinedFunctions.structure.functions.forLoop;
using AutomaticallyDefinedFunctions.structure.functions.ifStatement;
using AutomaticallyDefinedFunctions.structure.nodes.statenodes;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace ADFMain
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunADFGeneration<double>(3);
            TestStateNodes();
        }

        public static void TestStateNodes()
        {
            var adf = new StateBasedAdf<string,string>();
            adf.UseMain(CreateMainWithStateNodes());

            for (int i = 0; i < 5; i++)
            {
                var results = adf.GetValues();
                adf.Update(results.ElementAt(0),"Nice");
                
                Console.WriteLine(results.ElementAt(0));
            }
        }

        private static MainProgram<string> CreateMainWithStateNodes()
        {
            var codeBlock = new IfNode<string, bool>()
                .SetComparisonOperator(new NotNullComparator<string>(new ProgramOutputStateNode<string>()))
                .SetFalseCodeBlock(new ValueNode<string>("No last value"))
                .SetTrueCodeBlock(new ProgramOutputStateNode<string>());

            var func = new ForLoopNode<string, double>()
                .SetComparator(new LessThanComparator<double>(new ValueNode<double>(0),new ExecutionCountStateNode()))
                .SetIncrement(new ValueNode<double>(1))
                .SetCodeBlock(new ValueNode<string>("a"));
            
            
            return new MainProgram<string>(func);
        }

        public static void RunADFGeneration<T>(int runs) where T : IComparable
        {
            var settings = new AdfSettings(2, 3, 3, 65);
            settings.NumberOfFunctions = 5;

            var adfGenerator = new AdfGenerator<T>(0, settings);
            var adf = adfGenerator.Generate();
            for (int i = 0; i < runs; i++)
            {
                try
                {
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
                Console.WriteLine("======================");
                
            }
        }
       
    }
}