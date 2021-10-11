using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.visitors
{
    public interface INodeVisitor
    {
        public void Accept(INode valueNode);
    }
}