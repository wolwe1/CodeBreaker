using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.functions.comparators;
using AutomaticallyDefinedFunctions.structure.functions.forLoop;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.factories.functionFactories.operators
{
    public class LoopFunctionFactory : FunctionFactory
    {
        public LoopFunctionFactory() : base(NodeCategory.Loop) { }
        
        public override FunctionNode<T> CreateFunction<T, TU>(int maxDepth, FunctionCreator parent)
        {
            var sameAuxAsReturn = RandomNumberFactory.TrueOrFalse();

            if (sameAuxAsReturn)
            {
                var loop = new ForLoopNode<T, T>();

                var incremental = parent.GetTerminal<T>();
                var comparator = parent.ChooseComparator<T>(maxDepth - 1);
                var block = parent.Choose<T>(maxDepth - 1);

                return loop
                    .SetIncrement((ValueNode<T>) incremental)
                    .SetComparator(comparator)
                    .SetCodeBlock(block);
            }
            else
            {
                var loop = new ForLoopNode<T, TU>();

                var incremental = parent.GetTerminal<TU>();
                var comparator = parent.ChooseComparator<TU>(maxDepth - 1);

                var block = parent.Choose<T>(maxDepth - 1);

                return loop
                    .SetIncrement((ValueNode<TU>) incremental)
                    .SetComparator(comparator)
                    .SetCodeBlock(block);
            }
        }
        
        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(string) || typeof(T) == typeof(double) || typeof(T) == typeof(bool);
        }

        public override bool CanDispatchAux<T>()
        {
            return CanDispatch<T>();
        }

        protected override INode<T> GenerateFunctionFromId<T,TU>(string id, FunctionCreator functionCreator)
        {
            var incremental = functionCreator.GenerateChildFromId<TU>(ref id);
 
            var comparator = (NodeComparator<TU>)functionCreator.GenerateChildFromId<TU>(ref id);
                //FunctionGenerator.ChooseComparator<TU>(ref id);

            var block = functionCreator.GenerateChildFromId<T>(ref id);
            
            return new ForLoopNode<T,TU>()
                .SetIncrement(incremental)
                .SetComparator(comparator)
                .SetCodeBlock(block);
        }

        
    }
}