using System;

namespace AutomaticallyDefinedFunctions.source.nodes.valueNodes
{
    public class ValueNode<T> : INode<T> where T : IComparable
    {
        private readonly T _value;

        public ValueNode(T value)
        {
            _value = value;
        }
        
        protected ValueNode()
        {
            _value = default(T);
        }

        public virtual T GetValue()
        {
            return _value;
        }

        public virtual bool IsValid()
        {
            return _value != null;
        }

        public virtual int GetNullNodeCount()
        {
            return 0;
        }

    }
}