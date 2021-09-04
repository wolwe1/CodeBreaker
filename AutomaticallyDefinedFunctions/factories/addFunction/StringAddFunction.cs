using System.Text;

namespace AutomaticallyDefinedFunctions.factories.addFunction
{
    public class StringAddFunction : AddFunc<string>
    {
        public override string GetValue()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var child in Children)
            {
                builder.Append(child.GetValue());
            }

            return builder.ToString();
        }
    }
}