using System;

namespace AutomaticallyDefinedFunctions.structure.nodes.statenodes
{
    public class ProgramResponseStateNode<T> : StateNode<T> where T : IComparable
    {
        public ProgramResponseStateNode(T value): base(value) { }

        public ProgramResponseStateNode() { }

        public override bool IsValid()
        {
            return true;
        }
        public override INode<T> GetCopy()
        {
            return new ProgramResponseStateNode<T>(Value);
        }
    }
}