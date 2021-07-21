using System;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.source.ifStatement
{
    public abstract class NodeComparator<T> where T : IComparable
    {
        protected NodeComparator<T> Next;

        public NodeComparator<T> SetAdditionalComparator(NodeComparator<T> next)
        {
            Next = next;
            return this;
            
        }

        protected bool handleReturn(bool result,INode<T> leftPredicate, INode<T> rightPredicate)
        {
            if (result || Next == null)
                return result;
            
            return Next.PassesCheck(leftPredicate, rightPredicate);
        }
        public abstract bool PassesCheck(INode<T> leftPredicate, INode<T> rightPredicate);
    }
}