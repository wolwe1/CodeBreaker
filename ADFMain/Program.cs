using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions;
using AutomaticallyDefinedFunctions.Extensions;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.generators.functionGenerators;
using AutomaticallyDefinedFunctions.source;
using AutomaticallyDefinedFunctions.source.forLoop;
using AutomaticallyDefinedFunctions.source.ifStatement;
using AutomaticallyDefinedFunctions.source.nodes;
using AutomaticallyDefinedFunctions.source.nodes.valueNodes;

namespace ADFMain
{
    class Program
    {

        static FunctionDefinition<string> CreateFunctionDefinitionWithNullNode()
        {
            var loop = CreateForLoop();
            var ifStatement = CreateIfStatement().SetFalseCodeBlock(new NullNode<string>());

            INode<string> newloop = loop.SetCodeBlock(ifStatement);
            
            var definition = FunctionDefinition<string>.Create("ADF0")
                .UseFunction(newloop);

            return definition;
        }
        
        static FunctionDefinition<string> CreateFunctionDefinition()
        {
            var loop = CreateForLoop();
            var ifStatement = CreateIfStatement();

            INode<string> newloop = loop.SetCodeBlock(ifStatement);
            
            var definition = FunctionDefinition<string>.Create("ADF0")
                .UseFunction(newloop);

            return definition;
        }
        
        static ADF<double> CreateSimpleADF()
        {
            var nodeOne = new ValueNode<double>(1);
            var nodeTwo = new ValueNode<double>(2);
            var addNode = new NumericAddFunc(new List<INode<double>>() {nodeOne, nodeTwo});
            
            var func1 = FunctionDefinition<double>.Create("ADF0")
                .UseFunction(new NodeTree<double>(addNode));

            
            var main = new MainProgram<double>( new NodeTree<double>(addNode));
            return new ADF<double>(main,func1);
        }
        
        static void Main(string[] args)
        {
            // var definition = CreateFunctionDefinition();
            // var definitionWithNull = CreateFunctionDefinitionWithNullNode();
            //
            // Console.WriteLine($"Non null: Arity: {definition.GetArity()} - Valid: {definition.IsValid()}");
            // Console.WriteLine($"With null: Arity: {definitionWithNull.GetArity()} - Valid: {definitionWithNull.IsValid()}");
            //
            // var addFunc = new StringAddFunction()
            //     .AddChild(definition)
            //     .AddChild(definitionWithNull);
            //
            // var main = new MainProgram<string>(new NodeTree<string>(addFunc));
            //
            // Console.WriteLine($"Main: Valid {main.IsValid()} - Output ");
            //
            var comparators = new List<NodeComparator<double>>(){new LessThanComparator<double>(),new EqualsComparator<double>(),new GreaterThanComparator<double>()};
            var auxTerminals = new List<ValueNode<double>>()
                {new ValueNode<double>(0), new ValueNode<double>(1), new ValueNode<double>(2)};
            var auxFuncs = new List<FunctionGenerator<double>>()
                {new AddFunctionGenerator<double>(new Random(1),30)};
            
            var generator = new ADFGenerator<string>(1, 30)
                .UseTerminalNode(new ValueNode<string>("a"))
                .UseTerminalNode(new ValueNode<string>("b"))
                .UseTerminalNode(new ValueNode<string>("c"))
                .UseFunctionGenerator(new AddFunctionGenerator<string>(new Random(1),30))
                .UseFunctionGenerator(
                    new IfGenerator<string,double>(new Random(1),30, comparators, auxTerminals,auxFuncs
                    ));
            var adf = generator.Generate();

            Console.Write($"ADF value : {adf.GetValue()}");
        }

        private static ForLoopNode<string,double> CreateForLoop()
        {
            var forLoop = new ForLoopNode<string, double>();

            var codeBlock = new ValueNode<string>("a");

            forLoop = forLoop.SetCounter(0d)
                .SetIncrement(1d)
                .SetBounds(3d)
                .SetComparator(new LessThanComparator<double>())
                .SetCodeBlock(codeBlock);

            return forLoop;
        }
        private static IfNode<string, double> CreateIfStatement()
        {
            var nodeOne = new ValueNode<double>(5);
            var nodeTwo = new ValueNode<double>(5.5);
            var addNode = new NumericAddFunc(new List<INode<double>>() {nodeOne, nodeTwo});

            var nodeThree = new ValueNode<double>(5);
            var nodeFour = new ValueNode<double>(5);
            var addNodeTwo = new NumericAddFunc(new List<INode<double>>() {nodeThree, nodeFour});

            var trueMessage = new ValueNode<string>("The statement was true! :)");
            var falseMessage = new ValueNode<string>("The statement was false! :(");

            var ifStatement = new IfNode<string, double>()
            .SetLeftPredicate(addNode)
            .SetRightPredicate(addNodeTwo)
            .SetComparisonOperator(new LessThanComparator<double>()
                .SetAdditionalComparator(new EqualsComparator<double>()))
            .SetFalseCodeBlock(falseMessage)
            .SetTrueCodeBlock(trueMessage);
            return ifStatement;
        }
    }
}