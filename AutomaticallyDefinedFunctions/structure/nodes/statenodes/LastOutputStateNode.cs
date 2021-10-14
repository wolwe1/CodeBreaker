using System;
using AutomaticallyDefinedFunctions.parsing;

namespace AutomaticallyDefinedFunctions.structure.nodes.stateNodes
{
    public class LastOutputStateNode<T> : StateNode<T> where T : IComparable
    {
        public LastOutputStateNode(T value): base(value,NodeCategory.LastOutput) { }

        public LastOutputStateNode() : base(NodeCategory.LastOutput) { }

        public override INode<T> GetCopy()
        {
            return new LastOutputStateNode<T>(Value);
        }
    }
}