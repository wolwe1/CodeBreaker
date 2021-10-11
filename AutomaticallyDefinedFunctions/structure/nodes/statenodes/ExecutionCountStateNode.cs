using System;

namespace AutomaticallyDefinedFunctions.structure.nodes.statenodes
{
    //Uses double to behave with the rest of the type system
    public class ExecutionCountStateNode : StateNode<double>
    {
        public ExecutionCountStateNode(){}
        public ExecutionCountStateNode(double value): base(value) { }

        public override INode<double> GetCopy()
        {
            return new ExecutionCountStateNode(Value);
        }
    }
}