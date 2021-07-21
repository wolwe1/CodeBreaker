using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.Extensions;
using AutomaticallyDefinedFunctions.source.ifStatement;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.builders
{
    public class NodeBuilder
    {
        public static AddFunc CreateAdditionFunction(double firstNumber, double secondNumber)
        {
            var nodeOne = new ValueNode<double>(firstNumber);
            var nodeTwo = new ValueNode<double>(secondNumber);
            return new AddFunc(new List<INode<double>>() {nodeOne, nodeTwo});
        }
        
        public static AddFunc CreateAdditionFunction(INode<double> firstNode, INode<double> secondNode)
        {
            return new AddFunc(new List<INode<double>>() {firstNode, secondNode});
        }

        public static IfNode<T,U> CreateIfStatement<T,U>(INode<T> left,INode<T> right,INode<U> trueBlock,
            INode<U> falseBlock, NodeComparator<T> nodeOperator) where T : IComparable where U : IComparable
        {
            var ifStatement = new IfNode<T,U>();
            ifStatement.AddLeftPredicate(left);
            ifStatement.AddRightPredicate(right);
            ifStatement.AddComparisonOperator(nodeOperator);
            ifStatement.AddFalseCodeBlock(falseBlock);
            ifStatement.AddTrueCodeBlock(trueBlock);

            return ifStatement;
        }
    }
}