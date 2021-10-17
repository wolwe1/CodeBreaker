using AutomaticallyDefinedFunctions.parsing;

namespace AutomaticallyDefinedFunctions.structure.nodes.statenodes
{
    public class OutputFailedStateNode : StateNode<bool>
    {
        public OutputFailedStateNode() : base(NodeCategory.OutputFailed) { }

        public OutputFailedStateNode(bool failed) : base(failed,NodeCategory.OutputFailed) { }
        
        public override INode<bool> GetCopy()
        {
            return new OutputFailedStateNode(Value);
        }
    }
}