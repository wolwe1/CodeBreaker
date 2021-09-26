using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.addFunction
{
    public class NumericAddFunc : AddFunc<double>
    {
        public NumericAddFunc(): base(){}
        public NumericAddFunc(IEnumerable<INode<double>> nodes) : base(nodes){}

        public NumericAddFunc(INode<double> firstNode,INode<double> secondNode): base(firstNode,secondNode) { }
        public override double GetValue()
        {
            return Children.Sum( (child) => child.GetValue());
        }

        public override INode<double> GetCopy()
        {
            return new NumericAddFunc()
                .AddChild(GetChild(0).GetCopy())
                .AddChild(GetChild(1).GetCopy());
        }

        public override INode<double> ReplaceNullNodes(int maxDepth, FunctionGenerator generator)
        {
            var newLeftChild = ReplaceNullNodesForComponent(GetChild(0),maxDepth - 1,generator);
            var newRightChild = ReplaceNullNodesForComponent(GetChild(1),maxDepth - 1,generator);

            return new NumericAddFunc()
                .AddChild(newLeftChild)
                .AddChild(newRightChild);
        }
        
        public override INode<double> ReplaceNode(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth)
        {
            var index = nodeIndexToReplace;
            if (index-- == 0)
                return new NumericAddFunc( generator.CreateFunction<double>(maxDepth),Children[1].GetCopy());

            if ((index -= Children[0].GetNodeCount()) <= 0)
                return new NumericAddFunc(Children[0].ReplaceNode(index, generator, maxDepth),Children[1].GetCopy());

            if (index-- == 0)
                return new NumericAddFunc( Children[0].GetCopy(),generator.CreateFunction<double>(maxDepth));
            
            if ((index -= Children[1].GetNodeCount()) <= 0)
                return new NumericAddFunc(Children[1].GetCopy(),Children[1].ReplaceNode(index, generator, maxDepth));
            
            throw new Exception("Could not find desired node to replace");
        }
    }
}