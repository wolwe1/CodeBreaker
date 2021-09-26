using System;
using AutomaticallyDefinedFunctions.factories.functionFactories;

namespace AutomaticallyDefinedFunctions.structure.nodes.valueNodes
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

        public virtual INode<T> GetCopy()
        {
            return new ValueNode<T>(_value);
        }

        public INode<T> ReplaceNullNodes(int maxDepth, FunctionGenerator generator)
        {
            return GetCopy();
        }

        public virtual bool IsNullNode()
        {
            return false;
        }

        public string GetStructure()
        {
            return $"Value node({_value})";
        }

        public virtual string GetId()
        {
            return $"V['{_value}']";
        }

        public int GetNodeCount()
        {
            return 1;
        }

        public virtual INode<T> ReplaceNode(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth)
        {
            return generator.CreateFunction<T>(maxDepth);
        }

        public virtual INode<T> GetSubTree(int nodeIndexToGet)
        {
            return this;
        }
    }
}