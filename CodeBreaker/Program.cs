using System.Collections.Generic;
using AutomaticallyDefinedFunctions.structure.adf;
using AutomaticallyDefinedFunctions.structure.functions.comparators;
using AutomaticallyDefinedFunctions.structure.functions.ifStatement;
using AutomaticallyDefinedFunctions.structure.functions.other;
using AutomaticallyDefinedFunctions.structure.nodes.statenodes;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;
using CodeBreakerLib.connectors.operators.implementation;
using CodeBreakerLib.quickbuild;
using CodeBreakerLib.testHandler;
using CodeBreakerLib.testHandler.integration;
using CodeBreakerLib.testHandler.setup;

namespace CodeBreaker
{
    class Program
    {
        static void Main(string[] args)
        {

            var testHandler = new TestHandler(new TestFileDescriptorStrategy(),new GeneticAlgorithmBuilder());
            testHandler.Setup();
            testHandler.RunAllTests();
            
           // TestMutation();
        }
        
        public static void TestMutation()
        {
            var adfGenerator = QuickBuild.CreateStatePopulationGenerator<string, double>();

            var adf = adfGenerator.GenerateNewMember();
            var adf2 = adfGenerator.GenerateNewMember();

            var crossoverOperator = new CrossoverOperator<string,double>(100);

            crossoverOperator.CreateModifiedChildren(new List<string>() {adf.GetId(),adf2.GetId()},adfGenerator );
        }

        private static MainProgram<string> CreateMainWithStringNodes()
        {
            var comp = new GreaterThanComparator<double>(
                new LengthOfNode<double>(new ProgramResponseStateNode<double>()),
                new ValueNode<double>(3));

            var func = new IfNode<string, double>()
                .SetComparisonOperator(comp)
                .SetFalseCodeBlock(new ValueNode<string>("Program output is boring"))
                .SetTrueCodeBlock(new ValueNode<string>("Program output is interesting!"));
            
            return new MainProgram<string>(func);
        }
    }
}