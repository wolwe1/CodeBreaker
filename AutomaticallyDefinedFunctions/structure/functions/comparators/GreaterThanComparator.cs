using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.comparators
{
    public class GreaterThanComparator<T> : NodeComparator<T> where T : IComparable
    {
        private INode<T> LeftPredicate => GetChildAs<T>(0);
        private INode<T> RightPredicate => GetChildAs<T>(1);
        public GreaterThanComparator(INode<T> leftPredicate, INode<T> rightPredicate) : base(2)
        {
            RegisterChildren(new List<INode>(){leftPredicate,rightPredicate});
        }
        public override bool PassesCheck()
        {
            var leftResult = LeftPredicate.GetValue();
            var rightResult = RightPredicate.GetValue();

            var result = leftResult.CompareTo(rightResult) > 0;
            
            return HandleReturn(result);
        }
        
        public override T GetValue()
        {
            throw new NotImplementedException();
        }

        public override INode<T> GetCopy()
        {
            var newComp = new GreaterThanComparator<T>(LeftPredicate.GetCopy(),RightPredicate.GetCopy());

            if (Next != null)
                newComp.Next = (NodeComparator<T>)Next.GetCopy();

            return newComp;
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            var leftWithoutNulls = (INode<T>) ReplaceNullNodesForComponent(LeftPredicate, maxDepth, creator);
            var rightWithoutNulls = (INode<T>) ReplaceNullNodesForComponent(RightPredicate, maxDepth, creator);

            return new EqualsComparator<T>(leftWithoutNulls, rightWithoutNulls);
        }

        public override string GetId()
        {
            return CreateId<T>(NodeCategory.GreaterThan);
        }
        
    }
}