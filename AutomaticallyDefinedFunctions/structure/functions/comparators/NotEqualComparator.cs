using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.comparators
{
    public class NotEqualComparator<T> : NodeComparator<T> where T : IComparable
    {
        private INode<T> LeftPredicate => GetChildAs<T>(0);
        private INode<T> RightPredicate => GetChildAs<T>(1);
        public NotEqualComparator() : base(2) { }

        public NotEqualComparator(INode<T> leftPredicate, INode<T> rightPredicate) : this()
        {
            RegisterChildren(new List<INode>(){leftPredicate,rightPredicate});
        }

        public override string GetId()
        {
            return CreateId<T>(NodeCategory.NotEqual);
        }

        public override INode<T> GetCopy()
        {
            return new NotEqualComparator<T>(GetChildCopyAs<T>(0),GetChildCopyAs<T>(1));
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            var leftWithoutNulls = (INode<T>) ReplaceNullNodesForComponent(LeftPredicate, maxDepth, creator);
            var rightWithoutNulls = (INode<T>) ReplaceNullNodesForComponent(RightPredicate, maxDepth, creator);

            return new NotEqualComparator<T>(leftWithoutNulls, rightWithoutNulls);
        }

        public override T GetValue()
        {
            throw new NotImplementedException();
        }

        public override bool PassesCheck()
        {
            var leftVal = LeftPredicate.GetValue();
            var rightVal = RightPredicate.GetValue();
            var result = leftVal.CompareTo(rightVal) != 0;

            return result;
            //return LeftPredicate.GetValue().CompareTo(RightPredicate.GetValue()) != 0;
        }
    }
}