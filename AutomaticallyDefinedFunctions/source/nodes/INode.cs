using System;

namespace AutomaticallyDefinedFunctions.source.nodes
{
    public interface INode<out T> where T : IComparable
    {
        public T GetValue();

        public bool IsValid();

        public int GetNullNodeCount();
    }
}