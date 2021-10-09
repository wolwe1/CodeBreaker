using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.arithmetic
{
    public interface IArithmeticOperator<T> where T : IComparable
    {
        T GetResult(List<INode<T>> children) ;
    }
}