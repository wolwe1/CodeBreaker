using AutomaticallyDefinedFunctions.Nodes;

namespace AutomaticallyDefinedFunctions
{
    public class MainProgram<T>
    {
        private readonly NodeTree<T> _nodeTree;

        public MainProgram(NodeTree<T> nodeTree)
        {
            _nodeTree = nodeTree;
        }

        public T GetValue()
        {
            return _nodeTree.GetValue();
        }
    }
}