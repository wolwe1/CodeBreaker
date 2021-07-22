using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions;
using AutomaticallyDefinedFunctions.Extensions;
using AutomaticallyDefinedFunctions.source;
using AutomaticallyDefinedFunctions.source.forLoop;
using AutomaticallyDefinedFunctions.source.ifStatement;
using AutomaticallyDefinedFunctions.source.nodes;

namespace ADFMain
{
    class Program
    {
        static ADF<double> CreateSimpleADF()
        {
            var nodeOne = new ValueNode<double>(1);
            var nodeTwo = new ValueNode<double>(2);
            var addNode = new NumericAddFunc(new List<INode<double>>() {nodeOne, nodeTwo});
            
            var func1 = new FunctionDefinition<double>(new NodeTree<double>(addNode));

            
            var main = new MainProgram<double>( new NodeTree<double>(addNode));
            return new ADF<double>(main,func1);
        }
        
        static void Main(string[] args)
        {
            var forloop = new ForLoopNode<string, double>();

            var codeBlock = new ValueNode<string>("a");

            forloop.SetCounter(0d)
                .SetIncrement(1d)
                .SetBounds(3d)
                .SetComparator(new LessThanComparator<double>())
                .SetCodeBlock(codeBlock);
            
            Console.WriteLine(forloop.GetValue());
        }

        private static IfNode<double, string> CreateIfStatement()
        {
            var nodeOne = new ValueNode<double>(5);
            var nodeTwo = new ValueNode<double>(5.5);
            var addNode = new NumericAddFunc(new List<INode<double>>() {nodeOne, nodeTwo});

            var nodeThree = new ValueNode<double>(5);
            var nodeFour = new ValueNode<double>(5);
            var addNodeTwo = new NumericAddFunc(new List<INode<double>>() {nodeThree, nodeFour});

            var trueMessage = new ValueNode<string>("The statement was true! :)");
            var falseMessage = new ValueNode<string>("The statement was false! :(");

            var ifStatement = new IfNode<double, string>();
            ifStatement.SetLeftPredicate(addNode);
            ifStatement.SetRightPredicate(addNodeTwo);
            ifStatement.SetComparisonOperator(new LessThanComparator<double>()
                .SetAdditionalComparator(new EqualsComparator<double>()));
            ifStatement.SetFalseCodeBlock(falseMessage);
            ifStatement.SetTrueCodeBlock(trueMessage);
            return ifStatement;
        }
    }
}