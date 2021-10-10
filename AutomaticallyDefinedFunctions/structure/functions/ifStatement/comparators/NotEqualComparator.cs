﻿using System;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.ifStatement.comparators
{
    public class NotEqualComparator<T> : NodeComparator<T> where T: IComparable
    {
        public override bool PassesCheck(INode<T> leftPredicate, INode<T> rightPredicate)
        {
            var leftResult = leftPredicate.GetValue();
            var rightResult = rightPredicate.GetValue();

            var result = leftResult.CompareTo(rightResult) != 0;

            return handleReturn(result, leftPredicate, rightPredicate);
        }

        public override NodeComparator<T> GetCopy()
        {
            var newComp = new NotEqualComparator<T>();

            if (Next != null)
                newComp.Next = Next.GetCopy();

            return newComp;
        }

        public override string GetSymbol()
        {
            return "=";
        }
    }
}