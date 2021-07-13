using AutomaticallyDefinedFunctions.Nodes;

namespace AutomaticallyDefinedFunctions
{
    public class FunctionDefinition<T>
    {
        private readonly NodeTree<T> _nodeTree;

        public FunctionDefinition(NodeTree<T> nodeTree)
        {
            _nodeTree = nodeTree;
        }
    }
}