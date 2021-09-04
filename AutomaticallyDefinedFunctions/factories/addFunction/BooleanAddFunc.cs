using System.Linq;

namespace AutomaticallyDefinedFunctions.factories.addFunction
{
    public class BooleanAddFunc: AddFunc<bool>
    {
        public override bool GetValue()
        {
            return Children.Aggregate(true, (total, next) => total && next.GetValue());
        }
    }
}