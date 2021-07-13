namespace AutomaticallyDefinedFunctions.Nodes
{
    public interface INode<out T>
    {
        public T GetValue();
    }
}