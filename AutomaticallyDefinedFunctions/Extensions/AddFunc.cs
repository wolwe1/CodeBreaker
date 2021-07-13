using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.Nodes;
using AutomaticallyDefinedFunctions.source.Nodes;

namespace AutomaticallyDefinedFunctions.Extensions
{
    public class AddFunc : FunctionNode<int>
    {
        public AddFunc(List<INode<int>> children) : base(children)
        {
        }

        public override int GetValue()
        {
            return Children.Sum(child => child.GetValue());
        }
    }
}