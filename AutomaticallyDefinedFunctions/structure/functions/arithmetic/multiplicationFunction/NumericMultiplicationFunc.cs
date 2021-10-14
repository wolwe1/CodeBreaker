using System.Collections.Generic;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.arithmetic.multiplicationFunction
{
    public class NumericMultiplicationFunc : IArithmeticOperator<double>
    {
        public double GetResult(List<INode> children)
        {
            return ((INode<double>)children[0]).GetValue() * ((INode<double>)children[1]).GetValue();
        }
    }
}