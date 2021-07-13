namespace AutomaticallyDefinedFunctions.Nodes
{
    public class ValueNode<T> : INode<T>
    {
        private readonly T _value;

        public ValueNode(T value)
        {
            _value = value;
        }

        public T GetValue()
        {
            return _value;
        }
    }
}