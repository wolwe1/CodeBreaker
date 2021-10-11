using System;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.structure.nodes.statenodes
{
    public interface IStateNode{}
    public abstract class StateNode<T> : ValueNode<T>, IStateNode where T : IComparable
    {
        protected StateNode(){}
        protected StateNode(T value) : base(value) {}

        public override string GetId()
        {
            return $"{NodeCategory.State}['{Value}']";
        }

        public void UpdateState(T value)
        {
            Value = value;
        }
        
    }
}