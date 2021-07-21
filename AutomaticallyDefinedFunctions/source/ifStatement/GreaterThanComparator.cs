using System;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.source.ifStatement
{
    public class GreaterThanComparator<T> : NodeComparator<T> where T : IComparable
    {
        public override bool PassesCheck(INode<T> leftPredicate, INode<T> rightPredicate)
        {
            var leftResult = leftPredicate.GetValue();
            var rightResult = rightPredicate.GetValue();

            var result = leftResult.CompareTo(rightResult) > 0;
            
            return handleReturn(result, leftPredicate, rightPredicate);

        }
    }
}