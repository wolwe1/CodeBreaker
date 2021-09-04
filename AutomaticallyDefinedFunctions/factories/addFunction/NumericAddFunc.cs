using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.factories.addFunction
{
    public class NumericAddFunc : AddFunc<double>
    {
        public NumericAddFunc(): base(){}
        public NumericAddFunc(IEnumerable<INode<double>> nodes) : base(nodes){}

        public NumericAddFunc(INode<double> firstNode,INode<double> secondNode): base(firstNode,secondNode) { }
        public override double GetValue()
        {
            return Children.Sum( (child) => child.GetValue());
        }
    }
}