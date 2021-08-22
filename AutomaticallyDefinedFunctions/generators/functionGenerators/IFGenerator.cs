using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.source.ifStatement;
using AutomaticallyDefinedFunctions.source.nodes;
using AutomaticallyDefinedFunctions.source.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.generators.functionGenerators
{
    public class IfGenerator<T,U> : FunctionGenerator<T> where T : IComparable where U : IComparable
    {
        private readonly List<NodeComparator<U>> _comparators;
        private readonly List<ValueNode<U>> _auxilaryValueNodes;
        private readonly List<FunctionGenerator<U>> _auxilaryFunctionGenerators;

        public IfGenerator(Random generator, int terminalChance,List<NodeComparator<U>> comparators, List<ValueNode<U>> auxilaryValueNodes, List<FunctionGenerator<U>> auxilaryFunctionGenerators) : base(generator, terminalChance)
        {
            _comparators = comparators;
            _auxilaryValueNodes = auxilaryValueNodes;
            _auxilaryFunctionGenerators = auxilaryFunctionGenerators;
        }

        public override FunctionNode<T> Generate()
        {
            var ifStatement = new IfNode<T,U>();
            var leftPredicate = Choose(_auxilaryValueNodes,_auxilaryFunctionGenerators);
            var rightPredicate = Choose(_auxilaryValueNodes,_auxilaryFunctionGenerators);
            var comparator = ChooseComparator();
            var trueBlock = Choose();
            var falseBlock = Choose();

            return ifStatement
            .SetLeftPredicate(leftPredicate)
            .SetRightPredicate(rightPredicate)
            .SetComparisonOperator(comparator)
            .SetFalseCodeBlock(falseBlock)
            .SetTrueCodeBlock(falseBlock);
        }

        private NodeComparator<U> ChooseComparator()
        {
            var choice = NumberGenerator.Next(_comparators.Count);
            return _comparators.ElementAt(choice);
        }
    }
}