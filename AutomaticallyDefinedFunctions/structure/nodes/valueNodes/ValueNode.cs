using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.state;

namespace AutomaticallyDefinedFunctions.structure.nodes.valueNodes
{
    public class ValueNode<T> : INode<T> where T : IComparable
    {
        protected T Value;

        public ValueNode(T value)
        {
            Value = value;
        }
        
        protected ValueNode()
        {
            Value = default(T);
        }

        public virtual T GetValue()
        {
            return Value;
        }

        public virtual bool IsValid()
        {
            return Value != null;
        }

        public virtual int GetNullNodeCount()
        {
            return 0;
        }

        public virtual INode<T> GetCopy()
        {
            return new ValueNode<T>(Value);
        }

        public INode<T> ReplaceNullNodes(int maxDepth, FunctionGenerator generator)
        {
            return GetCopy();
        }

        public virtual bool IsNullNode()
        {
            return false;
        }
        
        public virtual string GetId()
        {
            return $"{NodeCategory.ValueNode}['{Value}']";
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
        
        public virtual void Visit(INodeVisitor visitor)
        {
            visitor.Accept(this);
            
        }
    }
}