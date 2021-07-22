using System;
using AutomaticallyDefinedFunctions.Extensions;
using AutomaticallyDefinedFunctions.source.ifStatement;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.source.forLoop
{
    public class ForLoopNode<T,U>: FunctionNode<T> where T : IComparable where U : IComparable
    {
        private ValueNode<U> _counter;
        private ValueNode<U> _incremental;
        private ValueNode<U> _bound;
        private NodeComparator<U> _comparator;

        private INode<T> _block;

        private AddFunc<U> _incrementalAdd;
        private AddFunc<T> _resultsAdd;

        public ForLoopNode()
        {
            _incrementalAdd = AddFunctionFactory.CreateAddFunction<U>();
            _resultsAdd = AddFunctionFactory.CreateAddFunction<T>();
        }
        public ForLoopNode(ValueNode<U> incremental, ValueNode<U> bound, NodeComparator<U> comparator, INode<T> block): this()
        {
            _incremental = incremental;
            _bound = bound;
            _comparator = comparator;
            _block = block;
        }
        
        public override T GetValue()
        {
            if (!IsValid()) throw new Exception("For loop function is not valid");

            _incrementalAdd.Refresh(_counter,_incremental);
            
            while (_comparator.PassesCheck(_counter,_bound))
            {

                _resultsAdd.AddChild(new ValueNode<T>(_block.GetValue()));
                _counter = new ValueNode<U>(_incrementalAdd.GetValue());
                _incrementalAdd.Refresh(_counter,_incremental);
            }

            return _resultsAdd.GetValue();
        }

        public override bool IsValid()
        {
            return _counter != null && _incremental != null && _comparator != null
                   && _bound != null && _block != null && _incrementalAdd != null && _resultsAdd != null;
        }

        private ValueNode<U> CreateTNode(double value)
        {
            U val = (U) Convert.ChangeType(value, typeof(U));
            return new(val);
        }
        private ValueNode<double> CreateDoubleValue(INode<U> node)
        {
            var asDouble = (double) Convert.ToDouble( (object)node.GetValue());

            return new ValueNode<double>(asDouble);
        }
        
        public ForLoopNode<T,U> SetCounter(ValueNode<U> counter)
        {
            _counter = counter;
            return this;
        }
        
        public ForLoopNode<T,U> SetCounter(U counter)
        {
            _counter = new ValueNode<U>(counter);
            return this;
        }
        
        public ForLoopNode<T,U> SetIncrement(ValueNode<U> incremental)
        {
            _incremental = incremental;
            return this;
        }
        
        public ForLoopNode<T,U> SetIncrement(U incremental)
        {
            _incremental = new ValueNode<U>(incremental);
            return this;
        }
        
        public ForLoopNode<T,U> SetBounds(ValueNode<U> bound)
        {
            _bound = bound;
            return this;
        }
        
        public ForLoopNode<T,U> SetBounds(U bound)
        {
            _bound = new ValueNode<U>(bound);
            return this;
        }
        
        public ForLoopNode<T,U> SetCodeBlock(ValueNode<T> block)
        {
            _block = block;
            return this;
        }
        
        public ForLoopNode<T,U> SetCodeBlock(T block)
        {
            _block = new ValueNode<T>(block);
            return this;
        }

        public ForLoopNode<T,U> SetComparator(NodeComparator<U> comparator)
        {
            _comparator = comparator;
            return this;
        }
        
        
    }
}