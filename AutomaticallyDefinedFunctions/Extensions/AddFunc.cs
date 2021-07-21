using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.Extensions
{
    public class AddFunc : FunctionNode<double> 
    {
        public AddFunc(IEnumerable<INode<double>> nodes) : base()
        {
            
            Children.AddRange(nodes);
        }

        public override double GetValue()
        {
            return Children.Sum( (child) => child.GetValue());
        }

        public override bool IsValid()
        {
            return Children.Count == 2;
        }
    }
}