using System;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.valueNodes
{
    public interface IValueNodeFactory : IDispatcher
    {
        INode<T> Get<T>() where T : IComparable;
    }
}