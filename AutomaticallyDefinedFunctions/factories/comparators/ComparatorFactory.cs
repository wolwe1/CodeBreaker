using System;
using AutomaticallyDefinedFunctions.factories.functionFactories;

namespace AutomaticallyDefinedFunctions.factories.comparators
{
    public abstract class ComparatorFactory : FunctionFactory
    {
        protected ComparatorFactory(string symbol) : base(symbol) { }

    }
}