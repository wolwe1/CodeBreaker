using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.functionFactories
{
    public interface IFunctionFactory: IDispatcher
    {
        public FunctionNode<T> CreateFunction<T, TU>(int maxDepth, FunctionCreator parent)
            where T : IComparable where TU : IComparable;
        
        bool CanMap(string id);
        INode<T> GenerateFunctionFromId<T>(string id, FunctionCreator functionCreator) where T : IComparable;
    }
}