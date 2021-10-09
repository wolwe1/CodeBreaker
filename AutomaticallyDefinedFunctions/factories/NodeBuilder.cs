using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction;
using AutomaticallyDefinedFunctions.structure.functions.forLoop;
using AutomaticallyDefinedFunctions.structure.functions.ifStatement;
using AutomaticallyDefinedFunctions.structure.functions.ifStatement.comparators;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.factories
{
    public class NodeBuilder
    {
        public static AddFunc<double> CreateAdditionFunction(double firstNumber, double secondNumber)
        {
            var nodeOne = new ValueNode<double>(firstNumber);
            var nodeTwo = new ValueNode<double>(secondNumber);
            return CreateAdditionFunction(nodeOne, nodeTwo);
        }
        
        public static AddFunc<double> CreateAdditionFunction(INode<double> firstNode, INode<double> secondNode)
        {
            return new AddFunc<double>(new List<INode<double>>() {firstNode, secondNode}, new NumericAddOperator());
        }

        public static IfNode<T,U> CreateIfStatement<T,U>(INode<U> left,INode<U> right,INode<T> trueBlock,
            INode<T> falseBlock, NodeComparator<U> nodeOperator) where T : IComparable where U : IComparable
        {
            return new IfNode<T,U>()
                .SetLeftPredicate(left)
                .SetRightPredicate(right)
                .SetComparisonOperator(nodeOperator)
                .SetFalseCodeBlock(falseBlock)
                .SetTrueCodeBlock(trueBlock);
        }

        public static ForLoopNode<T, double> CreateSimpleForLoop<T>(double bound,T code) where T : IComparable
        {
            return new ForLoopNode<T, double>()
                .SetCounter(new ValueNode<double>(0d))
                .SetIncrement(new ValueNode<double>(1d))
                .SetBounds(new ValueNode<double>(bound))
                .SetComparator(new LessThanComparator<double>())
                .SetCodeBlock(new ValueNode<T>(code));

        }

        public static ForLoopNode<T, TU> CreateForLoop<T,TU>(INode<TU> counter, INode<TU>  increment, INode<TU>  bounds, NodeComparator<TU> comparator, INode<T> block) where T : IComparable where TU : IComparable
        {
            return new ForLoopNode<T, TU>()
                .SetCounter(counter)
                .SetIncrement(increment)
                .SetBounds(bounds)
                .SetComparator(comparator)
                .SetCodeBlock(block);
        }
    }
}