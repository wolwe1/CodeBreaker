using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.nodes.statenodes;

namespace AutomaticallyDefinedFunctions.factories.valueNodes.state
{
    public class OutputFailureStateFactory : StateNodeFactory
    {
        public OutputFailureStateFactory() : base(NodeCategory.OutputFailed) { }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(bool);
        }

        public override INode<T> Get<T>()
        {
            return (INode<T>) new OutputFailedStateNode();
        }

        protected override INode<T> Get<T>(string id)
        {
            return (INode<T>) new OutputFailedStateNode(bool.Parse(id));
        }
    }
}