using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.arithmetic.subtractFunction
{
    public class SubtractFunc<T> : ArithmeticFunc<T> where T : IComparable
    {
        private readonly IArithmeticOperator<T> _subtractOperator;

        public SubtractFunc(IEnumerable<INode<T>> nodes, IArithmeticOperator<T> subtractOperator) : base(nodes,NodeCategory.Subtract)
        {
            _subtractOperator = subtractOperator;
        }

        public SubtractFunc(IArithmeticOperator<T> subtractOperator): base(NodeCategory.Subtract)
        {
            _subtractOperator = subtractOperator;
        }

        public SubtractFunc(INode<T> firstNode, INode<T> secondNode, IArithmeticOperator<T> subtractOperator) : base(firstNode,secondNode,NodeCategory.Subtract)
        {
            _subtractOperator = subtractOperator;
        }
        
        public override T GetValue()
        {
            return _subtractOperator.GetResult(Children);
        }
        
        public override INode<T> GetCopy()
        {
            var childCopies = GetChildCopies();

            return new SubtractFunc<T>(childCopies,_subtractOperator);
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionGenerator generator)
        {
            var (left,right) = GetChildrenWithoutNullNodes(maxDepth,generator);

            return new SubtractFunc<T>(left, right,_subtractOperator);
        }
        
    }
}