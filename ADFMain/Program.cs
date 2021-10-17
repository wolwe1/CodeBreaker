using System;
using System.Linq;
using AutomaticallyDefinedFunctions.factories.valueNodes.standard;
using AutomaticallyDefinedFunctions.generators.adf;
using AutomaticallyDefinedFunctions.structure.adf;
using AutomaticallyDefinedFunctions.structure.functions.comparators;
using AutomaticallyDefinedFunctions.structure.functions.forLoop;
using AutomaticallyDefinedFunctions.structure.functions.ifStatement;
using AutomaticallyDefinedFunctions.structure.nodes.statenodes;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;
using AutomaticallyDefinedFunctions.structure.state;

namespace ADFMain
{
    class Program
    {
        static void Main(string[] args)
        {

            var main = new IfNode<string, bool>()
                .SetComparisonOperator(new EqualsComparator<bool>(
                    new OutputFailedStateNode(),
                    new ValueNode<bool>(true)
                ))
                .SetFalseCodeBlock(new ForLoopNode<string, double>()
                    .SetComparator(new LessThanComparator<double>(
                        new ExecutionCountStateNode(),
                        new ValueNode<double>(253)))
                    .SetIncrement(new ValueNode<double>(1))
                    .SetCodeBlock(new ValueNode<string>("a"))
                )
                .SetTrueCodeBlock(new RandomStateNode<string>());

            var adf = new StateBasedAdf<string, double>();
            adf.UseMain(new MainProgram<string>(main));

            Simulate(adf,10);

            var history = adf.GetHistory();

            foreach (var output in history.OutputsWithResponse)
            {
                Console.WriteLine("History:");
                var adfOutput = output.Key;
                var outputs = adfOutput.GetOutputs();
                foreach (var outputOfAdf in outputs)
                {
                    Console.WriteLine($"Generated: {outputOfAdf.Value} - Failed: {outputOfAdf.Failed}");
                }
               
            }

        }

        private static void Simulate<T, TU>(StateBasedAdf<T, TU> adf,int numberOfRuns) where TU : IComparable where T : IComparable
        {
            for (var i = 0; i < numberOfRuns; i++)
            {
                var output = adf.GetValues();
                
                adf.Update(output,ValueNodeFactory.GetT<TU>().GetValue());
            }
        }


    }
}