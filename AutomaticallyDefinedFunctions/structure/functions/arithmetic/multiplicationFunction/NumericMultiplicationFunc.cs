using System.Collections.Generic;
using AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.arithmetic.multiplicationFunction
{
    public class NumericMultiplicationFunc : IArithmeticOperator<double>
    {
        public double GetResult(List<INode<double>> children)
        {
            return children[0].GetValue() * children[1].GetValue();
        }
    }
}