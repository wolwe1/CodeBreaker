using System;
using AutomaticallyDefinedFunctions.factories.addFunction;
using AutomaticallyDefinedFunctions.factories.valueNodes;
using AutomaticallyDefinedFunctions.source.forLoop;
using AutomaticallyDefinedFunctions.source.ifStatement;
using AutomaticallyDefinedFunctions.source.nodes;
using AutomaticallyDefinedFunctions.source.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.factories.functionFactories
{
    public static class FunctionFactory
    {

        public static FunctionNode<T> GetFunction<T>() where T : IComparable
        {
            var choice = RandomNumberFactory.Next(3);

            return choice switch
            {
                0 => GetFunction<T, string>(),
                1 => GetFunction<T, bool>(),
                2 => GetFunction<T, double>(),
                _ => throw new InvalidOperationException("Cannot dispatch function factory on type " + typeof(T))
            };
        }
        
        private static FunctionNode<T> GetFunction<T,TU>() where T : IComparable where TU : IComparable
        {
            var choice = RandomNumberFactory.Next(3);

            return choice switch
            {
                0 => CreateIfNode<T, TU>(),
                1 => CreateLoopNode<T, TU>(),
                2 => CreateAddNode<T, TU>(),
                _ => throw new ArgumentOutOfRangeException()
            };

        }

        private static FunctionNode<T> CreateAddNode<T, TU>() where T : IComparable where TU : IComparable
        {
            var addFunc = AddFunctionFactory.CreateAddFunction<T>();

            var firstChild = Choose<T>();
            var secondChild = Choose<T>();

            return addFunc.AddChild(firstChild).AddChild(secondChild);
        }

        private static FunctionNode<T> CreateLoopNode<T, TU>() where T : IComparable where TU : IComparable
        {
            var sameAuxAsReturn = RandomNumberFactory.TrueOrFalse();

            if (sameAuxAsReturn)
            {
                var loop = new ForLoopNode<T, T>();

                var counter = GetTerminal<T>();
                var incremental = GetTerminal<T>();
                var bound = GetTerminal<T>();
                var comparator = ChooseComparator<T>();

                var block = Choose<T>();

                return loop
                    .SetCounter((ValueNode<T>) counter)
                    .SetIncrement((ValueNode<T>) incremental)
                    .SetBounds((ValueNode<T>) bound)
                    .SetComparator(comparator)
                    .SetCodeBlock(block);
            }
            else
            {
                var loop = new ForLoopNode<T, TU>();

                var counter = GetTerminal<TU>();
                var incremental = GetTerminal<TU>();
                var bound = GetTerminal<TU>();
                var comparator = ChooseComparator<TU>();

                var block = Choose<T>();

                return loop
                    .SetCounter((ValueNode<TU>) counter)
                    .SetIncrement((ValueNode<TU>) incremental)
                    .SetBounds((ValueNode<TU>) bound)
                    .SetComparator(comparator)
                    .SetCodeBlock(block);
            }

            
        }

        private static FunctionNode<T> CreateIfNode<T,TU>() where T : IComparable where TU : IComparable
        {
            var sameAuxAsReturn = RandomNumberFactory.TrueOrFalse();

            if (sameAuxAsReturn)
            {
                var ifNode = new IfNode<T, T>();
                var leftPredicate = Choose<T>();
                var rightPredicate = Choose<T>();
                var comparator = ChooseComparator<T>();
                var trueBlock = Choose<T>();
                var falseBlock = Choose<T>();

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
                var leftPredicate = Choose<TU>();
                var rightPredicate = Choose<TU>();
                var comparator = ChooseComparator<TU>();
                var trueBlock = Choose<T>();
                var falseBlock = Choose<T>();

                return ifNode
                    .SetLeftPredicate(leftPredicate)
                    .SetRightPredicate(rightPredicate)
                    .SetComparisonOperator(comparator)
                    .SetFalseCodeBlock(falseBlock)
                    .SetTrueCodeBlock(trueBlock);
            }
            
        }

        private static NodeComparator<T> ChooseComparator<T>() where T : IComparable
        {
            var choice = RandomNumberFactory.Next(3);

            return choice switch
            {
                0 => ComparatorFactory.CreateEqualsComparator<T>(),
                1 => ComparatorFactory.CreateLessThanComparator<T>(),
                2 => ComparatorFactory.CreateGreaterThanComparator<T>(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static INode<T> Choose<T>() where T : IComparable
        {
            var terminalOrFunction = RandomNumberFactory.TrueOrFalse();

            return terminalOrFunction switch
            {
                true => GetTerminal<T>(),
                false => GetFunction<T>()
            };
        }

        private static INode<T> GetTerminal<T>() where T : IComparable
        {
            return ValueNodeFactory.Get<T>();
        }
    }
}