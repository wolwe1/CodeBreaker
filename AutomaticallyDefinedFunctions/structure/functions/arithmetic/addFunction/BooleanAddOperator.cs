﻿using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction
{
    public class BooleanAddOperator: IArithmeticOperator<bool>
    {
        public bool GetResult(List<INode> children)
        {
            return children.Aggregate(true, (total, next) => total && ((INode<bool>)next).GetValue());
        }
    }
}