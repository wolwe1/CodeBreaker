using System;

namespace AutomaticallyDefinedFunctions.structure.nodes.statenodes
{
    public class ProgramOutputStateNode<T> : StateNode<T> where T : IComparable
    {
        public ProgramOutputStateNode(T value): base(value) { }

        public ProgramOutputStateNode()
        {
            
        }

        public override INode<T> GetCopy()
        {
            return new ProgramOutputStateNode<T>(Value);
        }
    }
}