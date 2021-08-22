using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.generators.functionGenerators;
using AutomaticallyDefinedFunctions.source.nodes;
using AutomaticallyDefinedFunctions.source.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.generators
{
    public abstract class Generator<T,U> where T : IComparable where U : Generator<T,U>
    {
        protected readonly Random NumberGenerator;
        protected int TerminalChance;

        protected List<ValueNode<T>> ValueNodes;
        protected List<FunctionGenerator<T>> FunctionGenerators;

        protected Generator(Random generator,int terminalChance)
        {
            NumberGenerator = generator;
            TerminalChance = terminalChance;

            ValueNodes = new List<ValueNode<T>>();
            FunctionGenerators = new List<FunctionGenerator<T>>();
        }
        
        public U SetTerminalChance(int chance)
        {
            TerminalChance = chance;
            return (U)this;
        }

        public U UseTerminalNodes(List<ValueNode<T>> terminalNodes)
        {
            ValueNodes = terminalNodes;
            return (U)this;
        }
        
        public U UseTerminalNode(ValueNode<T> terminalNode)
        {
            ValueNodes.Add(terminalNode);
            return (U)this;
        }
        
        public U UseFunctionGenerators(List<FunctionGenerator<T>> functionGenerators)
        {
            FunctionGenerators = functionGenerators;
            return (U)this;
        }
        
        public U UseFunctionGenerator(FunctionGenerator<T> functionGenerator)
        {
            FunctionGenerators.Add(functionGenerator);
            return (U)this;
        }

        protected INode<T> Choose()
        {
            return Choose(ValueNodes, FunctionGenerators);
        }
        
        protected INode<U> Choose<U>(List<ValueNode<U>> terminalNodes,List<FunctionGenerator<U>> generators) where U : IComparable
        {
            var choice = NumberGenerator.Next(100);

            return choice < TerminalChance ? ChooseTerminalNode(terminalNodes) : GetFunctionNode(generators);
        }

        private INode<U> GetFunctionNode<U>(List<FunctionGenerator<U>> generators) where U : IComparable
        {
            var choice = NumberGenerator.Next(generators.Count);

            var generator = generators.ElementAt(choice);

            return generator.Generate();
        }

        protected INode<U> ChooseTerminalNode<U>(List<ValueNode<U>> terminalNodes) where U : IComparable
        {
            var choice = NumberGenerator.Next(terminalNodes.Count);

            return terminalNodes.ElementAt(choice);
        }
        
    }
}