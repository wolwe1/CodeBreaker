using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories;
using AutomaticallyDefinedFunctions.structure;
using AutomaticallyDefinedFunctions.structure.functions.comparators;
using AutomaticallyDefinedFunctions.structure.functions.ifStatement;
using AutomaticallyDefinedFunctions.structure.functions.other;
using AutomaticallyDefinedFunctions.structure.nodes.statenodes;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;
using AutomaticallyDefinedFunctions.structure.state;
using CodeBreakerLib;
using CodeBreakerLib.connectors.ga;
using CodeBreakerLib.connectors.ga.state;
using CodeBreakerLib.connectors.operators.implementation;
using CodeBreakerLib.quickBuild;
using CodeBreakerLib.testHandler;
using CodeBreakerLib.testHandler.integration;
using CodeBreakerLib.testHandler.setup;

namespace CodeBreaker
{
    class Program
    {
        static void Main(string[] args)
        {

            // var testHandler = new TestHandler(new TestFileDescriptorStrategy(),new GeneticAlgorithmBuilder());
            // testHandler.Setup();
            // testHandler.RunAllTests();
            
            TestMutation();
        }
        
        public static void TestMutation()
        {
            var adf = new StateBasedAdf<string,double>();
            adf.UseMain(CreateMainWithStringNodes());

            var mutator = new MutationOperator<string>(100,3);

            var adfPopGenerator = QuickBuild.CreateStatePopulationGenerator<string,double>();
            
            mutator.CreateModifiedChildren(new List<string>() {adf.GetId()},adfPopGenerator );
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