using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.nodes.statenodes;

namespace AutomaticallyDefinedFunctions.factories.valueNodes.state
{
    public class ExecutionCountStateFactory : StateNodeFactory
    {
        public ExecutionCountStateFactory() : base(NodeCategory.ExecutionCount) { }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(double);
        }

        public override INode<T> Get<T>()
        {
            //T needs to be a double
            return (INode<T>) new ExecutionCountStateNode();
        }

        protected override INode<T> Get<T>(string id)
        {
            var numberValue = int.Parse(id);

            return (INode<T>) new ExecutionCountStateNode(numberValue);
        }
    }
}