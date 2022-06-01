using System.Collections.Generic;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.nodes.statenodes;
using AutomaticallyDefinedFunctions.structure.visitors;

namespace AutomaticallyDefinedFunctions.visitors
{
    public class StateNodeVisitor : INodeVisitor
    {
        public List<IStateNode> StateNodes { get; }

        public StateNodeVisitor()
        {
            StateNodes = new List<IStateNode>();
        }

        public void Accept(INode node)
        {
            if (node is IStateNode stateNode)
            {
                StateNodes.Add(stateNode);
            }
        }

        public bool WantsToVisit()
        {
            return true;
        }
    }
}