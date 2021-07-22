using System.Linq;

namespace AutomaticallyDefinedFunctions.Extensions
{
    public class BooleanAddFunc: AddFunc<bool>
    {
        public override bool GetValue()
        {
            return Children.Aggregate(true, (total, next) => total && next.GetValue());
        }
    }
}