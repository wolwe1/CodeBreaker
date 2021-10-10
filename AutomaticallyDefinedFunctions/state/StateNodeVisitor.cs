using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.nodes.statenodes;

namespace AutomaticallyDefinedFunctions.state
{
    public class StateNodeVisitor : INodeVisitor
    {
        public List<IStateNode> StateNodes { get; }

        public StateNodeVisitor()
        {
            StateNodes = new List<IStateNode>();
        }

        public void Accept<T>(INode<T> node) where T : IComparable
        {
            if (node is StateNode<T> stateNode)
            {
                StateNodes.Add(stateNode);
            }
        }
    }
}