namespace AutomaticallyDefinedFunctions.parsing
{
    public class NodeCategory
    {
        public string Value { get; }

        private NodeCategory(string value)
        {
            Value = value;
        }

        public static string If => new NodeCategory("I").Value;
        public static string Loop => new NodeCategory("L").Value;
        public static string Add => new NodeCategory("A").Value;
        
        public static string ValueNode => new NodeCategory("V").Value;
    }
}