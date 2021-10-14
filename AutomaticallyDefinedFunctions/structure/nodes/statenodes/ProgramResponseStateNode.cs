using System;
using AutomaticallyDefinedFunctions.parsing;

namespace AutomaticallyDefinedFunctions.structure.nodes.stateNodes
{
    public class ProgramResponseStateNode<T> : StateNode<T> where T : IComparable
    {
        public ProgramResponseStateNode(T value): base(value,NodeCategory.ProgramResponse) { }

        public ProgramResponseStateNode(): base(NodeCategory.ProgramResponse) { }

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