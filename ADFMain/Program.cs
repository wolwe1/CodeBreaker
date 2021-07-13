using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions;
using AutomaticallyDefinedFunctions.Extensions;
using AutomaticallyDefinedFunctions.Nodes;

namespace ADFMain
{
    class Program
    {
        static void Main(string[] args)
        {

            var nodeOne = new ValueNode<int>(1);
            var nodeTwo = new ValueNode<int>(2);
            var addNode = new AddFunc(new List<INode<int>>() {nodeOne, nodeTwo});
            
            var func1 = new FunctionDefinition<int>(new NodeTree<int>(addNode));

            
            var main = new MainProgram<int>( new NodeTree<int>(addNode));
            var adf = new ADF<int>(main,func1);
            
            Console.WriteLine(adf.GetValue());
        }
    }
}