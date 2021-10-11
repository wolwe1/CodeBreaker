using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction
{
    public class AddFunc<T> : ArithmeticFunc<T> where T : IComparable
    {
        private readonly IArithmeticOperator<T> _arithmeticOperator;

        public AddFunc(IEnumerable<INode<T>> nodes, IArithmeticOperator<T> arithmeticOperator) : base(nodes, NodeCategory.Add)
        {
            _arithmeticOperator = arithmeticOperator;
        }

        public AddFunc(IArithmeticOperator<T> arithmeticOperator) : base(NodeCategory.Add)
        {
            _arithmeticOperator = arithmeticOperator;
        }

        public AddFunc(INode<T> firstNode, INode<T> secondNode, IArithmeticOperator<T> arithmeticOperator) : base(firstNode,
            secondNode, NodeCategory.Add)
        {
            _arithmeticOperator = arithmeticOperator;
        }

        public AddFunc<T> Refresh(INode<T> firstNode,INode<T> secondNode)
        {
            Children.Clear();
            Children.Add(firstNode);
            Children.Add(secondNode);
            
            return this;
        }

        public override T GetValue()
        {
            return _arithmeticOperator.GetResult(Children);
        }

        public override INode<T> GetCopy()
        {
            var childCopies = GetChildCopies();

            return new AddFunc<T>(childCopies,_arithmeticOperator);
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionGenerator generator)
        {
            var (left,right) = GetChildrenWithoutNullNodes(maxDepth,generator);

            return new AddFunc<T>(left, right,_arithmeticOperator);
        }
    }
}