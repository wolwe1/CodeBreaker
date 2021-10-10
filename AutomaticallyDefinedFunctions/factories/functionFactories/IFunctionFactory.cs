using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.functionFactories
{
    public interface IFunctionFactory
    {
        public FunctionNode<T> Get<T, TU>(int maxDepth, FunctionGenerator parent)
            where T : IComparable where TU : IComparable;

        public bool CanDispatchFunctionOfType(Type t);
        bool CanMap(string id);
        INode<T> GenerateFunction<T>(string id, FunctionGenerator functionGenerator) where T : IComparable;
    }
}