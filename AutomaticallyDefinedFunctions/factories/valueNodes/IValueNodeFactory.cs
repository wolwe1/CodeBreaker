using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.valueNodes
{
    public interface IValueNodeFactory : IDispatcher
    {
        INode<T> Get<T>() where T : IComparable;
        INode<T> GenerateFunctionFromId<T>(string id, FunctionCreator functionCreator) where T : IComparable;
    }
}