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
        public IfFunctionFactory() : base(NodeCategory.If)
        {
            
        }
        
        public override FunctionNode<T> CreateFunction<T, TU>(int maxDepth,FunctionGenerator parent)
        {
            var sameAuxAsReturn = RandomNumberFactory.TrueOrFalse();

            if (sameAuxAsReturn)
            {
                var ifNode = new IfNode<T, T>();
                var leftPredicate = parent.Choose<T>(maxDepth - 1);
                var rightPredicate = parent.Choose<T>(maxDepth - 1);
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
        
        public override bool CanDispatchFunctionOfType(Type t)
        {
            return t == typeof(string) || t == typeof(double) || t == typeof(bool);
        }

        protected override INode<T> GenerateFunctionFromId<T,TU>(string id, FunctionGenerator functionGenerator)
        {
            var comparator = (NodeComparator<TU>)functionGenerator.GenerateChildFromId<TU>(ref id);
                //FunctionGenerator.ChooseComparator<TU>(ref id);

            var trueBlock = functionGenerator.GenerateChildFromId<T>(ref id);

            var falseBlock = functionGenerator.GenerateChildFromId<T>(ref id);
            
            return new IfNode<T,TU>()
                .SetComparisonOperator(comparator)
                .SetTrueCodeBlock(trueBlock)
                .SetFalseCodeBlock(falseBlock);
        }
    }
}