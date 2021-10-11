using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions
{
    public abstract class FunctionNode<T> : ChildManager, INode<T> where T : IComparable
    {
        protected FunctionNode(int expectedChildrenAmount): base(expectedChildrenAmount){}
        protected FunctionNode(IEnumerable<INode<T>> nodes) : base(nodes) {}

        public override bool IsNullNode() => false;
 
        protected INode ReplaceNullNodesForComponent(INode component,int maxDepth, FunctionGenerator generator)
        {
            if (!component.IsNullNode())
            {
                return component switch
                {
                    INode<string> strComp => strComp.GetNullNodeCount() > 0
                        ? strComp.ReplaceNullNodes(maxDepth - 1, generator)
                        : strComp.GetCopy(),
                    INode<bool> boolComp => boolComp.GetNullNodeCount() > 0
                        ? boolComp.ReplaceNullNodes(maxDepth - 1, generator)
                        : boolComp.GetCopy(),
                    INode<double> doubleComp => doubleComp.GetNullNodeCount() > 0
                        ? doubleComp.ReplaceNullNodes(maxDepth - 1, generator)
                        : doubleComp.GetCopy(),
                    _ => throw new Exception($"Unable to dispatch type ReplaceNullNodesForComponent")
                };
            }

            return DispatchForGenerator(component, generator, maxDepth);
        }

        private static INode DispatchForGenerator(INode node, FunctionGenerator generator, int maxDepth)
        {
            return node switch
            {
                INode<string> => generator.Choose<string>(maxDepth - 1),
                INode<bool> => generator.Choose<bool>(maxDepth - 1),
                INode<double> => generator.Choose<double>(maxDepth - 1),
                _ => throw new Exception($"Unable to dispatch type in ReplaceNullNodesForComponent")
            };
        }

        public new FunctionNode<T> AddChild(INode newNode)
        {
            base.AddChild(newNode);
            return this;
        }
        
        protected string CreateId<TU>(string nodeCategory)
        {
            var header = $"{nodeCategory}<{typeof(T)},{typeof(TU)}>";
            var childIds = Children.Select(child => child.GetId());
            
            var body = $"[{string.Join("",childIds)}]";

            return $"{header}{body}";
        }

        public abstract INode<T> ReplaceNullNodes(int maxDepth, FunctionGenerator generator);

        public abstract T GetValue();

        public abstract INode<T> GetCopy();
        
    }
}