using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.visitors
{
    public class BranchReplacementVisitor : INodeVisitor
    {
        private readonly FunctionGenerator _generator;
        private readonly int _maxDepth;

        public BranchReplacementVisitor(FunctionGenerator generator, int maxDepth)
        {
            _generator = generator;
            _maxDepth = maxDepth;
        }

        public void Accept(INode valueNode)
        {
            throw new System.NotImplementedException();
        }
    }
}