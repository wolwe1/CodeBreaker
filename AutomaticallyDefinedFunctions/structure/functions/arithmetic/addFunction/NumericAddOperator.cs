using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction
{
    public class NumericAddOperator : IArithmeticOperator<double> 
    {
        public double GetResult(List<INode> children)
        {
            return children.Sum( (child) => ((INode<double>)child).GetValue());
        }
    }
}