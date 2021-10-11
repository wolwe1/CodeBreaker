namespace AutomaticallyDefinedFunctions.parsing
{
    public class NodeCategory
    {
        private string Value { get; }

        private NodeCategory(string value)
        {
            Value = value;
        }

        public static string If => new NodeCategory("I").Value;
        public static string Loop => new NodeCategory("L").Value;
        public static string Add => new NodeCategory("A").Value;
        
        public static string ValueNode => new NodeCategory("V").Value;
        public static string Subtract => new NodeCategory("S").Value;
        public static string Multiplication => new NodeCategory("M").Value;
        public static string Division => new NodeCategory("D").Value;
        public static string State => new NodeCategory("Z").Value;
        public static string NotNull => new NodeCategory("Y").Value;
        public static string Equal => new NodeCategory("=").Value;
        public static string LessThan => new NodeCategory("<").Value;
        public static string GreaterThan => new NodeCategory(">").Value;
        public static string NotEqual => new NodeCategory(">").Value;
    }
}