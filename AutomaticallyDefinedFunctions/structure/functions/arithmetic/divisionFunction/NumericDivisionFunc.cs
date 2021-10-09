﻿using System.Collections.Generic;
using AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.arithmetic.divisionFunction
{
    public class NumericDivisionFunc : IArithmeticOperator<double>
    {
        public double GetResult(List<INode<double>> children)
        {
            var firstVal = children[0].GetValue();
            var secondVal = children[1].GetValue();

            if (secondVal == 0)
                return 0;

            return firstVal / secondVal;
        }
    }
}