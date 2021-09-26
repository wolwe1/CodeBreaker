using System;
using System.Text.RegularExpressions;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.ifStatement;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.functionFactories
{
    public class IfFunctionFactory : FunctionFactory
    {
        public IfFunctionFactory() : base(NodeCategory.If)
        {
            
        }
        
        public override FunctionNode<T> Get<T, TU>(int maxDepth,FunctionGenerator parent)
        {
            var sameAuxAsReturn = RandomNumberFactory.TrueOrFalse();

            if (sameAuxAsReturn)
            {
                var ifNode = new IfNode<T, T>();
                var leftPredicate = parent.Choose<T>(maxDepth - 1);
                var rightPredicate = parent.Choose<T>(maxDepth - 1);
                var comparator = parent.ChooseComparator<T>();
                var trueBlock = parent.Choose<T>(maxDepth - 1);
                var falseBlock = parent.Choose<T>(maxDepth - 1);

                return ifNode
                    .SetLeftPredicate(leftPredicate)
                    .SetRightPredicate(rightPredicate)
                    .SetComparisonOperator(comparator)
                    .SetFalseCodeBlock(falseBlock)
                    .SetTrueCodeBlock(trueBlock);
            }
            else
            {
                var ifNode = new IfNode<T, TU>();
                var leftPredicate = parent.Choose<TU>(maxDepth - 1);
                var rightPredicate = parent.Choose<TU>(maxDepth - 1);
                var comparator = parent.ChooseComparator<TU>();
                var trueBlock = parent.Choose<T>(maxDepth - 1);
                var falseBlock = parent.Choose<T>(maxDepth - 1);

                return ifNode
                    .SetLeftPredicate(leftPredicate)
                    .SetRightPredicate(rightPredicate)
                    .SetComparisonOperator(comparator)
                    .SetFalseCodeBlock(falseBlock)
                    .SetTrueCodeBlock(trueBlock);
            }
        }
        
        public override bool CanDispatchFunctionOfType(Type t)
        {
            return t == typeof(string) || t == typeof(double) || t == typeof(bool);
        }

        protected override INode<T> GenerateFunction<T,TU>(string id, FunctionGenerator functionGenerator)
        {
            var leftPred = functionGenerator.GenerateChildFromId<TU>(ref id);
            
            var rightPred = functionGenerator.GenerateChildFromId<TU>(ref id);
 
            var comparator = FunctionGenerator.ChooseComparator<TU>(ref id);

            var trueBlock = functionGenerator.GenerateChildFromId<T>(ref id);

            var falseBlock = functionGenerator.GenerateChildFromId<T>(ref id);
            
            return new IfNode<T,TU>()
                .SetLeftPredicate(leftPred)
                .SetRightPredicate(rightPred)
                .SetComparisonOperator(comparator)
                .SetTrueCodeBlock(trueBlock)
                .SetFalseCodeBlock(falseBlock);
        }
    }
}