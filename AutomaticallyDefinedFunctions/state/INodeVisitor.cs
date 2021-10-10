using System;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.state
{
    public interface INodeVisitor
    {
        void Accept<T>(INode<T> node) where T : IComparable;
    }
}