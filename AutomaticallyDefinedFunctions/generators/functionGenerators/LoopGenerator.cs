using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.source.forLoop;
using AutomaticallyDefinedFunctions.source.ifStatement;
using AutomaticallyDefinedFunctions.source.nodes;
using AutomaticallyDefinedFunctions.source.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.generators.functionGenerators
{
    public class LoopGenerator<T,U> : FunctionGenerator<T> where T : IComparable where U : IComparable
    {
        private readonly List<ValueNode<U>> _auxiliaryValueNodes;
        private readonly List<FunctionGenerator<U>> _auxiliaryFunctionGenerators;
        private readonly List<NodeComparator<U>> _comparators;

        public LoopGenerator(Random generator, int terminalChance, List<ValueNode<U>> auxiliaryValueNodes, List<FunctionGenerator<U>> auxiliaryFunctionGenerators, List<NodeComparator<U>> comparators) : base(generator, terminalChance)
        {
            _auxiliaryValueNodes = auxiliaryValueNodes;
            _auxiliaryFunctionGenerators = auxiliaryFunctionGenerators;
            _comparators = comparators;
        }
        public override FunctionNode<T> Generate()
        {

            var loop = new ForLoopNode<T, U>();
            
            var counter = ChooseTerminalNode(_auxiliaryValueNodes);
            var incremental = ChooseTerminalNode(_auxiliaryValueNodes);
            var bound = ChooseTerminalNode(_auxiliaryValueNodes);
            var comparator = ChooseComparator();

            var block = Choose();

            return loop
                .SetCounter((ValueNode<U>) counter)
                .SetIncrement((ValueNode<U>) incremental)
                .SetBounds((ValueNode<U>) bound)
                .SetComparator(comparator)
                .SetCodeBlock(block);
        }
        
        private NodeComparator<U> ChooseComparator()
        {
            var choice = NumberGenerator.Next(_comparators.Count);
            return _comparators.ElementAt(choice);
        }
    }
}