using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.Extensions;
using AutomaticallyDefinedFunctions.source.forLoop;
using AutomaticallyDefinedFunctions.source.ifStatement;
using AutomaticallyDefinedFunctions.source.nodes;
using AutomaticallyDefinedFunctions.source.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.builders
{
    public class NodeBuilder
    {
        public static AddFunc<double> CreateAdditionFunction(double firstNumber, double secondNumber)
        {
            var nodeOne = new ValueNode<double>(firstNumber);
            var nodeTwo = new ValueNode<double>(secondNumber);
            return new NumericAddFunc(new List<INode<double>>() {nodeOne, nodeTwo});
        }
        
        public static AddFunc<double> CreateAdditionFunction(INode<double> firstNode, INode<double> secondNode)
        {
            return new NumericAddFunc(new List<INode<double>>() {firstNode, secondNode});
        }

        public static IfNode<T,U> CreateIfStatement<T,U>(INode<U> left,INode<U> right,INode<T> trueBlock,
            INode<T> falseBlock, NodeComparator<U> nodeOperator) where T : IComparable where U : IComparable
        {
            var ifStatement = new IfNode<T,U>();
            ifStatement.SetLeftPredicate(left);
            ifStatement.SetRightPredicate(right);
            ifStatement.SetComparisonOperator(nodeOperator);
            ifStatement.SetFalseCodeBlock(falseBlock);
            ifStatement.SetTrueCodeBlock(trueBlock);

            return ifStatement;
        }

        public static ForLoopNode<T, double> CreateSimpleForLoop<T>(double bound,T code) where T : IComparable
        {
            var forloop = new ForLoopNode<T, double>();
            forloop.SetCounter(new ValueNode<double>(0d))
                .SetIncrement(new ValueNode<double>(1d))
                .SetBounds(new ValueNode<double>(bound))
                .SetComparator(new LessThanComparator<double>())
                .SetCodeBlock(new ValueNode<T>(code));

            return forloop;
        }
    }
}