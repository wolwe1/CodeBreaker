using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions;
using AutomaticallyDefinedFunctions.Extensions;
using AutomaticallyDefinedFunctions.source;
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
            var addNode = new AddFunc(new List<INode<double>>() {nodeOne, nodeTwo});
            
            var func1 = new FunctionDefinition<double>(new NodeTree<double>(addNode));

            
            var main = new MainProgram<double>( new NodeTree<double>(addNode));
            return new ADF<double>(main,func1);
        }
        
        static void Main(string[] args)
        {

            var nodeOne = new ValueNode<double>(5);
            var nodeTwo = new ValueNode<double>(5.5);
            var addNode = new AddFunc(new List<INode<double>>() {nodeOne, nodeTwo});
            
            var nodeThree = new ValueNode<double>(5);
            var nodeFour = new ValueNode<double>(5);
            var addNodeTwo = new AddFunc(new List<INode<double>>() {nodeThree, nodeFour});

            var trueMessage = new ValueNode<string>("The statement was true! :)");
            var falseMessage = new ValueNode<string>("The statement was false! :(");

            var ifStatement = new IfNode<double,string>();
            ifStatement.AddLeftPredicate(addNode);
            ifStatement.AddRightPredicate(addNodeTwo);
            ifStatement.AddComparisonOperator(new LessThanComparator<double>()
                .SetAdditionalComparator(new EqualsComparator<double>()));
            ifStatement.AddFalseCodeBlock(falseMessage);
            ifStatement.AddTrueCodeBlock(trueMessage);
            
            
            Console.WriteLine(ifStatement.GetValue());
        }
    }
}