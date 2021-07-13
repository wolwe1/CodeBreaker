namespace AutomaticallyDefinedFunctions.Nodes
{
    public class NodeTree<T>
    {
        private readonly INode<T> _root;

        public NodeTree(INode<T> root)
        {
            _root = root;
        }

        public T GetValue()
        {
            return _root.GetValue();
        }
    }
}