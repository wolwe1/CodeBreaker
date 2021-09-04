using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.generators.functionGenerators;
using AutomaticallyDefinedFunctions.source;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.generators
{
    public class ADFGenerator<T> : Generator<T,ADFGenerator<T>> where T : IComparable
    {
        private readonly FunctionDefinitionHolder<T> _definitionHolder;

        public ADFGenerator(int seed, int terminalChance) : base(new Random(seed), terminalChance)
        {
            _definitionHolder = new FunctionDefinitionHolder<T>(NumberGenerator);
            UseFunctionGenerator(_definitionHolder);
        }
        
        public ADF<T> Generate()
        {
            var function = GenerateFunctionDefinition();
            _definitionHolder.AddDefinition(function);

            var main = GenerateMainFunction();
            return new ADF<T>()
                .UseDefinition(function)
                .UseMain(main);
        }

        private FunctionDefinition<T> GenerateFunctionDefinition()
        {
            var emptyDef = FunctionDefinition<T>.Create("ADF0");

            var function = GenerateNodeTree();
            var definition = emptyDef.UseFunction(function);
            
            return definition;
        }
        
        private MainProgram<T> GenerateMainFunction()
        {
            var mainTree = GenerateNodeTree();
            var main = new MainProgram<T>(mainTree);
            
            return main;
        }

        private NodeTree<T> GenerateNodeTree()
        {

            var root = Choose();
            var tree = new NodeTree<T>(root);

            return tree;
        }
        

    }
}