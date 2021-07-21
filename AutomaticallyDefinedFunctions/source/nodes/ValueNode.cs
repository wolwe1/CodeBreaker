using System;

namespace AutomaticallyDefinedFunctions.source.nodes
{
    public class ValueNode<T> : INode<T> where T : IComparable
    {
        private readonly T _value;

        public ValueNode(T value)
        {
            _value = value;
        }

        public T GetValue()
        {
            return _value;
        }

        public bool IsValid()
        {
            return true;
        }
    }
}