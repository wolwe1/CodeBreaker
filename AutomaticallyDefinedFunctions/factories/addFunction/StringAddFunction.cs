using System;
using System.Text;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.addFunction
{
    public class StringAddFunction : AddFunc<string>
    {
        public StringAddFunction(){}
        public StringAddFunction(INode<string> firstNode,INode<string> secondNode): base(firstNode,secondNode) { }
        public override string GetValue()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var child in Children)
            {
                builder.Append(child.GetValue());
            }

            return builder.ToString();
        }

        public override INode<string> GetCopy()
        {
            return new StringAddFunction(GetChild(0).GetCopy(), GetChild(1).GetCopy());
        }

        public override INode<string> ReplaceNullNodes(int maxDepth, FunctionGenerator generator)
        {
            var newLeftChild = ReplaceNullNodesForComponent(GetChild(0),maxDepth - 1,generator);
            var newRightChild = ReplaceNullNodesForComponent(GetChild(1),maxDepth - 1,generator);

            return new StringAddFunction(newLeftChild, newRightChild);
        }
        
        public override INode<string> ReplaceNode(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth)
        {
            var index = nodeIndexToReplace;
            if (index-- == 0)
                return new StringAddFunction( generator.CreateFunction<string>(maxDepth),Children[1].GetCopy());

            if ((index -= Children[0].GetNodeCount()) <= 0)
                return new StringAddFunction(Children[0].ReplaceNode(index, generator, maxDepth),Children[1].GetCopy());

            if (index-- == 0)
                return new StringAddFunction( Children[0].GetCopy(),generator.CreateFunction<string>(maxDepth));
            
            if ((index -= Children[1].GetNodeCount()) <= 0)
                return new StringAddFunction(Children[1].GetCopy(),Children[1].ReplaceNode(index, generator, maxDepth));
            
            throw new Exception("Could not find desired node to replace");
        }
    }
}