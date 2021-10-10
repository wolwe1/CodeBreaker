using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.exceptions;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.state;
using AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction;
using AutomaticallyDefinedFunctions.structure.functions.ifStatement.comparators;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.structure.functions.forLoop
{
    public class ForLoopNode<T,TU>: FunctionNode<T> where T : IComparable where TU : IComparable
    {
        private readonly INode<TU> _counter;
        private readonly INode<TU> _incremental;
        private readonly INode<TU> _bound;
        private readonly NodeComparator<TU> _comparator;

        private readonly INode<T> _block;

        private readonly AddFunc<TU> _incrementalAdd;
        private readonly AddFunc<T> _resultsAdd;

        public ForLoopNode()
        {
            _incrementalAdd = AddFunctionFactory.CreateAddFunction<TU>();
            _resultsAdd =  AddFunctionFactory.CreateAddFunction<T>();
        }

        private ForLoopNode(INode<TU> incremental, INode<TU> bound, NodeComparator<TU> comparator,
            INode<T> block,INode<TU> counter): this()
        {
            _incremental = incremental;
            _bound = bound;
            _comparator = comparator;
            _block = block;
            _counter = counter;

            RegisterChildren( new List<INode<T>> {_block});
        }
        
        public override string GetId()
        {
            return $"{NodeCategory.Loop}<{typeof(T)},{typeof(TU)}>[{_counter.GetId()}{_incremental.GetId()}{_comparator.GetSymbol()}{_bound.GetId()}{_block.GetId()}]";
        }

        public override int GetNodeCount()
        {
            //TODO: Investigate
            return _counter.GetNodeCount() +
                   _incremental.GetNodeCount() +
                   _bound.GetNodeCount() +
                   _block.GetNodeCount();
        }

        public override INode<T> ReplaceNode(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth)
        {
            var index = nodeIndexToReplace;
            
            //First child
            if (index - 1 == 0)
                return SetCounter(generator.CreateFunction<TU>(maxDepth));

            if (index - _counter.GetNodeCount() <= 0)
                return SetCounter(_counter.ReplaceNode(--index, generator, maxDepth));
            
            index -= _counter.GetNodeCount();
            
            //Second child
            if (index - 1 == 0)
                return SetIncrement(generator.CreateFunction<TU>(maxDepth));
            
            if (index - _incremental.GetNodeCount() <= 0)
                return SetIncrement(_incremental.ReplaceNode(--index, generator, maxDepth));
            
            index -= _incremental.GetNodeCount();
            
            //Third child
            if (index - 1 == 0)
                return SetBounds(generator.CreateFunction<TU>(maxDepth));
            
            if (index - _bound.GetNodeCount() <= 0)
                return SetBounds(_bound.ReplaceNode(--index, generator, maxDepth));
            
            index -= _bound.GetNodeCount();
            
            //Fourth child
            if (index - 1 == 0)
                return SetCodeBlock(generator.CreateFunction<T>(maxDepth));
            
            if (index - _block.GetNodeCount() <= 0)
                return SetCodeBlock(_block.ReplaceNode(--index, generator, maxDepth));

            throw new Exception("Could not find desired node to replace");
            
        }
        
        public override INode<T> GetSubTree(int nodeIndexToGet)
        {
            var index = nodeIndexToGet;

            //First child
            if (index-- == 0)
                return _block;
            
            if (index - _block.GetNodeCount() <= 0)
                return _block.GetSubTree(--index);
            
            throw new Exception("Sub tree could not be found");
        }
        
        public override T GetValue()
        {
            if (!IsValid()) throw new InvalidStructureException("For loop function is invalid");

            var count = _counter.GetCopy();
            _incrementalAdd.Refresh(_counter,_incremental);

            int safetyBreak = 0;
            while (_comparator.PassesCheck(count,_bound))
            {
                _resultsAdd.AddChild(new ValueNode<T>(_block.GetValue()));
                count = new ValueNode<TU>(_incrementalAdd.GetValue());
                _incrementalAdd.Refresh(count,_incremental);

                safetyBreak++;

                if (safetyBreak == 1000)
                    throw new ProgramLoopException(_counter.GetId(), _comparator.GetSymbol(), _bound.GetId());
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

            numberOfNullNodes += GetNullNodeCountForComponent(_incremental);
            numberOfNullNodes += GetNullNodeCountForComponent(_bound);
            numberOfNullNodes += GetNullNodeCountForComponent(_block);
            numberOfNullNodes += GetNullNodeCountForComponent(_counter);
            
            return numberOfNullNodes;
        }

        public override INode<T> GetCopy()
        {
            return new ForLoopNode<T, TU>(_incremental.GetCopy(),_bound.GetCopy(),_comparator.GetCopy(),_block.GetCopy(),_counter.GetCopy());
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionGenerator generator)
        {
            var newIncremental = ReplaceNullNodesForComponent(_incremental,maxDepth,generator);
            var newBound = ReplaceNullNodesForComponent(_bound,maxDepth,generator);
            var newBlock = ReplaceNullNodesForComponent(_block,maxDepth,generator);
            var newCounter = ReplaceNullNodesForComponent(_counter,maxDepth,generator);

            return new ForLoopNode<T, TU>(newIncremental,newBound,_comparator.GetCopy(),newBlock,newCounter);

        }
        public ForLoopNode<T,TU> SetCounter(INode<TU> counter)
        {
            return new ForLoopNode<T, TU>(_incremental,_bound,_comparator,_block,counter);
        }

        public ForLoopNode<T,TU> SetIncrement(INode<TU> incremental)
        {
            return new ForLoopNode<T, TU>(incremental,_bound,_comparator,_block,_counter);
        }

        public ForLoopNode<T,TU> SetBounds(INode<TU> bound)
        {
            return new ForLoopNode<T, TU>(_incremental,bound,_comparator,_block,_counter);
        }

        public ForLoopNode<T,TU> SetCodeBlock(INode<T> block)
        {
            return new ForLoopNode<T, TU>(_incremental,_bound,_comparator,block,_counter);
        }

        public ForLoopNode<T,TU> SetComparator(NodeComparator<TU> comparator)
        {
            return new ForLoopNode<T, TU>(_incremental,_bound,comparator,_block,_counter);

        }

        public override void Visit(INodeVisitor visitor)
        {
            visitor.Accept(this);
            
            _incremental.Visit(visitor);
            _counter.Visit(visitor);
            _block.Visit(visitor);
            _bound.Visit(visitor);
        }
    }
}