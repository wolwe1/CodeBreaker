using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.Extensions;
using AutomaticallyDefinedFunctions.source.ifStatement;
using AutomaticallyDefinedFunctions.source.nodes;
using AutomaticallyDefinedFunctions.source.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.source.forLoop
{
    public class ForLoopNode<T,U>: FunctionNode<T> where T : IComparable where U : IComparable
    {
        private ValueNode<U> _counter;
        private readonly ValueNode<U> _incremental;
        private readonly ValueNode<U> _bound;
        private readonly NodeComparator<U> _comparator;

        private readonly INode<T> _block;

        private readonly AddFunc<U> _incrementalAdd;
        private readonly AddFunc<T> _resultsAdd;

        public ForLoopNode()
        {
            _incrementalAdd = AddFunctionFactory.CreateAddFunction<U>();
            _resultsAdd = AddFunctionFactory.CreateAddFunction<T>();
        }

        private ForLoopNode(ValueNode<U> incremental, ValueNode<U> bound, NodeComparator<U> comparator,
            INode<T> block,ValueNode<U> counter): this()
        {
            _incremental = incremental;
            _bound = bound;
            _comparator = comparator;
            _block = block;
            _counter = counter;

            RegisterChildren( new List<INode<T>>(){_block});
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
                   && _bound != null && _incrementalAdd != null && _resultsAdd != null 
                   && _block != null && _block.IsValid();
        }

        public override int GetNullNodeCount()
        {
            var numberOfNullNodes = 0;
            
            if (_incremental != null)
            {
                numberOfNullNodes += _incremental.GetNullNodeCount();
            }
            
            if (_bound != null)
            {
                numberOfNullNodes += _bound.GetNullNodeCount();
            }
            
            if (_block != null)
            {
                numberOfNullNodes += _block.GetNullNodeCount();
            }
            
            if (_counter != null)
            {
                numberOfNullNodes += _counter.GetNullNodeCount();
            }

            return numberOfNullNodes;
        }

        private ValueNode<U> CreateTNode(double value)
        {
            U val = (U) Convert.ChangeType(value, typeof(U));
            return new(val);
        }
        private ValueNode<double> CreateDoubleValue(INode<U> node)
        {
            var asDouble = Convert.ToDouble( node.GetValue());

            return new ValueNode<double>(asDouble);
        }
        
        public ForLoopNode<T,U> SetCounter(ValueNode<U> counter)
        {
            return new ForLoopNode<T, U>(_incremental,_bound,_comparator,_block,counter);
        }
        
        public ForLoopNode<T,U> SetCounter(U counter)
        {
            var counterNode = new ValueNode<U>(counter);
            return SetCounter(counterNode);
        }
        
        public ForLoopNode<T,U> SetIncrement(ValueNode<U> incremental)
        {
            return new ForLoopNode<T, U>(incremental,_bound,_comparator,_block,_counter);
        }
        
        public ForLoopNode<T,U> SetIncrement(U incremental)
        {
            var incrementalNode = new ValueNode<U>(incremental);
            return SetIncrement(incrementalNode);
        }
        
        public ForLoopNode<T,U> SetBounds(ValueNode<U> bound)
        {
            return new ForLoopNode<T, U>(_incremental,bound,_comparator,_block,_counter);
        }
        
        public ForLoopNode<T,U> SetBounds(U bound)
        {
            var boundNode = new ValueNode<U>(bound);
            return SetBounds(boundNode);
        }
        
        public ForLoopNode<T,U> SetCodeBlock(INode<T> block)
        {
            return new ForLoopNode<T, U>(_incremental,_bound,_comparator,block,_counter);
        }
        
        public ForLoopNode<T,U> SetCodeBlock(T block)
        {
            var blockNode = new ValueNode<T>(block);
            return SetCodeBlock(blockNode);
        }

        public ForLoopNode<T,U> SetComparator(NodeComparator<U> comparator)
        {
            return new ForLoopNode<T, U>(_incremental,_bound,comparator,_block,_counter);

        }
        
        
    }
}