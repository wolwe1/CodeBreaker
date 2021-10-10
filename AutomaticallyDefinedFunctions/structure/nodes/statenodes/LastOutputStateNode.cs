using System;

namespace AutomaticallyDefinedFunctions.structure.nodes.statenodes
{
    public class LastOutputStateNode<T> : StateNode<T> where T : IComparable
    {
        public LastOutputStateNode(T value): base(value) { }

        public override INode<T> GetCopy()
        {
            return new LastOutputStateNode<T>(Value);
        }
    }
}