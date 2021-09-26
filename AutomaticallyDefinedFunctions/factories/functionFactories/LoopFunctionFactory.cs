using System;
using System.Text.RegularExpressions;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.forLoop;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.factories.functionFactories
{
    public class LoopFunctionFactory : FunctionFactory
    {
        public LoopFunctionFactory() : base(NodeCategory.Loop)
        {
            
        }
        
        public override FunctionNode<T> Get<T, TU>(int maxDepth, FunctionGenerator parent)
        {
            var sameAuxAsReturn = RandomNumberFactory.TrueOrFalse();

            if (sameAuxAsReturn)
            {
                var loop = new ForLoopNode<T, T>();

                var counter = parent.GetTerminal<T>();
                var incremental = parent.GetTerminal<T>();
                var bound = parent.GetTerminal<T>();
                var comparator = parent.ChooseComparator<T>();

                var block = parent.Choose<T>(maxDepth - 1);

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

                var counter = parent.GetTerminal<TU>();
                var incremental = parent.GetTerminal<TU>();
                var bound = parent.GetTerminal<TU>();
                var comparator = parent.ChooseComparator<TU>();

                var block = parent.Choose<T>(maxDepth - 1);

                return loop
                    .SetCounter((ValueNode<TU>) counter)
                    .SetIncrement((ValueNode<TU>) incremental)
                    .SetBounds((ValueNode<TU>) bound)
                    .SetComparator(comparator)
                    .SetCodeBlock(block);
            }
        }
        
        public override bool CanDispatchFunctionOfType(Type t)
        {
            return t == typeof(string) || t == typeof(double) || t == typeof(bool);
        }

        protected override INode<T> GenerateFunction<T,TU>(string id, FunctionGenerator functionGenerator)
        {
            var counter = functionGenerator.GenerateChildFromId<TU>(ref id);
            
            var incremental = functionGenerator.GenerateChildFromId<TU>(ref id);
 
            var comparator = FunctionGenerator.ChooseComparator<TU>(ref id);

            var bounds = functionGenerator.GenerateChildFromId<TU>(ref id);

            var block = functionGenerator.GenerateChildFromId<T>(ref id);
            
            return new ForLoopNode<T,TU>()
                .SetCounter(counter)
                .SetIncrement(incremental)
                .SetBounds(bounds)
                .SetComparator(comparator)
                .SetCodeBlock(block);
        }

        
    }
}