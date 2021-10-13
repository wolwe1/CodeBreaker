using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.functions.comparators;
using AutomaticallyDefinedFunctions.structure.functions.ifStatement;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.functionFactories.operators
{
    public class IfFunctionFactory : FunctionFactory
    {
        public IfFunctionFactory() : base(NodeCategory.If) { }
        
        public override FunctionNode<T> CreateFunction<T, TU>(int maxDepth,FunctionCreator parent)
        {
            var sameAuxAsReturn = RandomNumberFactory.TrueOrFalse();

            if (sameAuxAsReturn)
            {
                var ifNode = new IfNode<T, T>();
                var comparator = parent.ChooseComparator<T>(maxDepth - 1);
                var trueBlock = parent.Choose<T>(maxDepth - 1);
                var falseBlock = parent.Choose<T>(maxDepth - 1);

                return ifNode
                    .SetComparisonOperator(comparator)
                    .SetFalseCodeBlock(falseBlock)
                    .SetTrueCodeBlock(trueBlock);
            }
            else
            {
                var ifNode = new IfNode<T, TU>();
                var comparator = parent.ChooseComparator<TU>(maxDepth - 1);
                var trueBlock = parent.Choose<T>(maxDepth - 1);
                var falseBlock = parent.Choose<T>(maxDepth - 1);

                return ifNode
                    .SetComparisonOperator(comparator)
                    .SetFalseCodeBlock(falseBlock)
                    .SetTrueCodeBlock(trueBlock);
            }
        }
        
        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(double) || typeof(T) == typeof(string) || typeof(T) == typeof(bool);
        }

        public override bool CanDispatchAux<T>()
        {
            return CanDispatch<T>();
        }

        protected override INode<T> GenerateFunctionFromId<T,TU>(string id, FunctionCreator functionCreator)
        {
            var comparator = (NodeComparator<TU>)functionCreator.GenerateChildFromId<TU>(ref id);
                //FunctionGenerator.ChooseComparator<TU>(ref id);

            var trueBlock = functionCreator.GenerateChildFromId<T>(ref id);

            var falseBlock = functionCreator.GenerateChildFromId<T>(ref id);
            
            return new IfNode<T,TU>()
                .SetComparisonOperator(comparator)
                .SetTrueCodeBlock(trueBlock)
                .SetFalseCodeBlock(falseBlock);
        }
    }
}