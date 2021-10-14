using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.functionFactories
{
    public interface IFunctionFactory: IDispatcher
    {
        FunctionNode<T> CreateFunction<T>(int maxDepth, FunctionCreator parent) where T : IComparable;
        bool CanMap(string id);
        INode<T> GenerateFunctionFromId<T>(string id, FunctionCreator functionCreator) where T : IComparable;
    }
}