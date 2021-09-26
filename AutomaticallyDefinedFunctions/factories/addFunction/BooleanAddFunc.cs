using System;
using System.Linq;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.addFunction
{
    public class BooleanAddFunc: AddFunc<bool>
    {
        public BooleanAddFunc(){}
        public BooleanAddFunc(INode<bool> firstChild,INode<bool> secondChild) : base(firstChild,secondChild){}

        public override bool GetValue()
        {
            return Children.Aggregate(true, (total, next) => total && next.GetValue());
        }

        public override INode<bool> GetCopy()
        {
            return new BooleanAddFunc(GetChild(0).GetCopy(),GetChild(1).GetCopy());
        }

        public override INode<bool> ReplaceNullNodes(int maxDepth, FunctionGenerator generator)
        {
            var newLeftChild = ReplaceNullNodesForComponent(GetChild(0),maxDepth - 1,generator);
            var newRightChild = ReplaceNullNodesForComponent(GetChild(1),maxDepth - 1,generator);

            return new BooleanAddFunc(newLeftChild,newRightChild);
        }
        
        public override INode<bool> ReplaceNode(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth)
        {
            var index = nodeIndexToReplace;
            if (index-- == 0)
                return new BooleanAddFunc( generator.CreateFunction<bool>(maxDepth),Children[1].GetCopy());

            if (index - Children[0].GetNodeCount() <= 0)
                return new BooleanAddFunc(Children[0].ReplaceNode(index, generator, maxDepth),Children[1].GetCopy());

            index -= Children[0].GetNodeCount();
            //Second child
            if (index-- == 0)
                return new BooleanAddFunc( Children[0].GetCopy(),generator.CreateFunction<bool>(maxDepth));
            
            if (index - Children[1].GetNodeCount() <= 0)
                return new BooleanAddFunc(Children[1].GetCopy(),Children[1].ReplaceNode(index, generator, maxDepth));
            
            throw new Exception("Could not find desired node to replace");
        }
    }
}