using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.generators.functionGenerators;
using AutomaticallyDefinedFunctions.source.nodes;
using AutomaticallyDefinedFunctions.source.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.generators
{
    public abstract class Generator<T,TGenerator> where T : IComparable where TGenerator : Generator<T,TGenerator>
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
        
        public TGenerator SetTerminalChance(int chance)
        {
            TerminalChance = chance;
            return (TGenerator)this;
        }

        public TGenerator UseTerminalNodes(IEnumerable<ValueNode<T>> terminalNodes)
        {
            ValueNodes = terminalNodes.ToList();
            return (TGenerator)this;
        }
        
        public TGenerator UseTerminalNode(ValueNode<T> terminalNode)
        {
            ValueNodes.Add(terminalNode);
            return (TGenerator)this;
        }
        
        public TGenerator UseFunctionGenerators(List<FunctionGenerator<T>> functionGenerators)
        {
            FunctionGenerators = functionGenerators;
            return (TGenerator)this;
        }
        
        public TGenerator UseFunctionGenerator(FunctionGenerator<T> functionGenerator)
        {
            FunctionGenerators.Add(functionGenerator);
            return (TGenerator)this;
        }

        protected INode<T> Choose()
        {
            return Choose(ValueNodes, FunctionGenerators);
        }
        
        protected INode<TX> Choose<TX>(List<ValueNode<TX>> terminalNodes,List<FunctionGenerator<TX>> generators) where TX : IComparable
        {
            var choice = NumberGenerator.Next(100);

            return choice < TerminalChance ? ChooseTerminalNode(terminalNodes) : GetFunctionNode(generators);
        }

        private INode<X> GetFunctionNode<X>(List<FunctionGenerator<X>> generators) where X : IComparable
        {
            var choice = NumberGenerator.Next(generators.Count);

            var generator = generators.ElementAt(choice);

            return generator.Generate();
        }

        protected INode<X> ChooseTerminalNode<X>(List<ValueNode<X>> terminalNodes) where X : IComparable
        {
            var choice = NumberGenerator.Next(terminalNodes.Count);

            return terminalNodes.ElementAt(choice);
        }
        
    }
}