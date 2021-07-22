using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.Extensions;
using AutomaticallyDefinedFunctions.source.ifStatement;
using AutomaticallyDefinedFunctions.source.nodes;

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

        public static IfNode<T,U> CreateIfStatement<T,U>(INode<T> left,INode<T> right,INode<U> trueBlock,
            INode<U> falseBlock, NodeComparator<T> nodeOperator) where T : IComparable where U : IComparable
        {
            var ifStatement = new IfNode<T,U>();
            ifStatement.SetLeftPredicate(left);
            ifStatement.SetRightPredicate(right);
            ifStatement.SetComparisonOperator(nodeOperator);
            ifStatement.SetFalseCodeBlock(falseBlock);
            ifStatement.SetTrueCodeBlock(trueBlock);

            return ifStatement;
        }
    }
}