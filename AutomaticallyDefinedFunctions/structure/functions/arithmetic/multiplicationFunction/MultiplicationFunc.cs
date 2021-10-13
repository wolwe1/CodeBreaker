using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.arithmetic.multiplicationFunction
{
    public class MultiplicationFunc<T> : ArithmeticFunc<T> where T : IComparable
    {
        private readonly IArithmeticOperator<T> _multiplicationOperator;
        public MultiplicationFunc(IEnumerable<INode<T>> nodes, IArithmeticOperator<T> multiplicationOperator) : base(nodes,NodeCategory.Multiplication)
        {
            _multiplicationOperator = multiplicationOperator;
        }

        public MultiplicationFunc(IArithmeticOperator<T> multiplicationOperator): base(NodeCategory.Multiplication)
        {
            _multiplicationOperator = multiplicationOperator;
        }

        public MultiplicationFunc(INode<T> firstNode, INode<T> secondNode, IArithmeticOperator<T> multiplicationOperator) : base(firstNode,secondNode,NodeCategory.Multiplication)
        {
            _multiplicationOperator = multiplicationOperator;
        }
        
        public override T GetValue()
        {
            return _multiplicationOperator.GetResult(Children);
        }
        
        public override INode<T> GetCopy()
        {
            var childCopies = GetChildCopies();

            return new MultiplicationFunc<T>(childCopies,_multiplicationOperator);
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            var (left,right) = GetChildrenWithoutNullNodes(maxDepth,creator);

            return new MultiplicationFunc<T>(left, right,_multiplicationOperator);
        }
        
    }
}