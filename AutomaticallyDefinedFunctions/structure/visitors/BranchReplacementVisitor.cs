using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.visitors
{
    public class BranchReplacementVisitor : INodeVisitor
    {
        private readonly FunctionCreator _creator;
        private readonly int _maxDepth;

        public BranchReplacementVisitor(FunctionCreator creator, int maxDepth)
        {
            _creator = creator;
            _maxDepth = maxDepth;
        }

        public void Accept(INode node)
        {
            throw new System.NotImplementedException();
        }
    }
}