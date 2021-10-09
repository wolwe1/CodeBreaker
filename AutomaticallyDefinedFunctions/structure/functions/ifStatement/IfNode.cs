using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.exceptions;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions.ifStatement.comparators;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure.functions.ifStatement
{
    public class IfNode<T,TU> : FunctionNode<T> where T : IComparable where TU : IComparable
    {
        private readonly INode<TU> _leftPredicate;
        private readonly INode<TU> _rightPredicate;
        private readonly NodeComparator<TU> _comparator;
        private readonly INode<T> _trueBlock;
        private readonly INode<T> _falseBlock;

        private IfNode(INode<TU> leftPredicate, INode<TU> rightPredicate, NodeComparator<TU> comparator, INode<T> trueBlock, INode<T> falseBlock) : base()
        {
            _leftPredicate = leftPredicate;
            _rightPredicate = rightPredicate;
            _comparator = comparator;
            _trueBlock = trueBlock;
            _falseBlock = falseBlock;

            RegisterChildren(new List<INode<T>>() {_trueBlock, falseBlock});
        }

        public IfNode()
        {
            
        }
        
        public IfNode<T,TU> SetLeftPredicate(INode<TU> predicateRoot)
        {
            return new IfNode<T, TU>(predicateRoot, _rightPredicate, _comparator, _trueBlock, _falseBlock);
        }
        
        public IfNode<T,TU> SetRightPredicate(INode<TU> predicateRoot)
        {
            return new IfNode<T, TU>(_leftPredicate, predicateRoot, _comparator, _trueBlock, _falseBlock);
        }
        
        public IfNode<T,TU> SetComparisonOperator(NodeComparator<TU> comparator)
        {
            return new IfNode<T, TU>(_leftPredicate, _rightPredicate, comparator, _trueBlock, _falseBlock);
        }
        
        public IfNode<T,TU> SetTrueCodeBlock(INode<T> root)
        {
            return new IfNode<T, TU>(_leftPredicate, _rightPredicate, _comparator, root, _falseBlock);
        }
        
        public IfNode<T,TU> SetFalseCodeBlock(INode<T> root)
        {
            return new IfNode<T, TU>(_leftPredicate, _rightPredicate, _comparator, _trueBlock, root);
        }
        
        public override string GetId()
        {
            return
                $"{NodeCategory.If}<{typeof(T)},{typeof(TU)}>[{_leftPredicate.GetId()}{_rightPredicate.GetId()}{_comparator.GetSymbol()}{_trueBlock.GetId()}{_falseBlock.GetId()}]";

        }

        public override int GetNodeCount()
        {
            return _leftPredicate.GetNodeCount() +
                   _rightPredicate.GetNodeCount() +
                   _trueBlock.GetNodeCount() +
                   _falseBlock.GetNodeCount();
        }

        public override INode<T> ReplaceNode(int nodeIndexToReplace, FunctionGenerator generator, int maxDepth)
        {
            var index = nodeIndexToReplace;
            if (index - 1 == 0)
                return SetLeftPredicate(generator.CreateFunction<TU>(maxDepth));

            if (index - _leftPredicate.GetNodeCount() <= 0)
                return SetLeftPredicate(_leftPredicate.ReplaceNode(--index, generator, maxDepth));
            
            index -= _leftPredicate.GetNodeCount();
            
            if (index - 1 == 0)
                return SetRightPredicate(generator.CreateFunction<TU>(maxDepth));
            
            if (index - _rightPredicate.GetNodeCount() <= 0)
                return SetRightPredicate(_rightPredicate.ReplaceNode(--index, generator, maxDepth));
            
            index -= _rightPredicate.GetNodeCount();
            
            if (index - 1 == 0)
                return SetTrueCodeBlock(generator.CreateFunction<T>(maxDepth));
            
            if (index - _trueBlock.GetNodeCount() <= 0)
                return SetTrueCodeBlock(_trueBlock.ReplaceNode(--index, generator, maxDepth));
            
            index -= _trueBlock.GetNodeCount();
            
            if (index - 1 == 0)
                return SetFalseCodeBlock(generator.CreateFunction<T>(maxDepth));
            
            if (index - _falseBlock.GetNodeCount() <= 0)
                return SetFalseCodeBlock(_falseBlock.ReplaceNode(--index, generator, maxDepth));

            throw new Exception("Could not find desired node to replace");
        }
        
        public override INode<T> GetSubTree(int nodeIndexToGet)
        {
            var index = nodeIndexToGet;

            //First child
            if (index-- == 0)
                return _trueBlock;
            
            if (index - _trueBlock.GetNodeCount() <= 0)
                return _trueBlock.GetSubTree(index);
            
            index -= _trueBlock.GetNodeCount();
            
            //Fourth child
            if (index-- == 0)
                return _falseBlock;

            if ( index - _falseBlock.GetNodeCount() <= 0)
                return _falseBlock.GetSubTree(index);

            throw new Exception("Sub tree could not be found");
        }

        public override T GetValue()
        {
            if (!IsValid()) throw new InvalidStructureException("If statement is invalid");
            
            return _comparator.PassesCheck(_leftPredicate, _rightPredicate) ? 
                _trueBlock.GetValue() : _falseBlock.GetValue();
        }

        public override bool IsValid()
        {
            return _leftPredicate != null && _leftPredicate.IsValid() &&
                   _rightPredicate != null && _rightPredicate.IsValid() &&
                   _comparator != null &&
                   _trueBlock != null && _trueBlock.IsValid() &&
                   _falseBlock != null && _falseBlock.IsValid() ;
        }

        public override int GetNullNodeCount()
        {
            var numberOfNullNodes = 0;

            numberOfNullNodes += GetNullNodeCountForComponent(_leftPredicate);
            numberOfNullNodes += GetNullNodeCountForComponent(_rightPredicate);
            numberOfNullNodes += GetNullNodeCountForComponent(_trueBlock);
            numberOfNullNodes += GetNullNodeCountForComponent(_falseBlock);
            
            return numberOfNullNodes;
        }

        public override INode<T> GetCopy()
        {
            return new IfNode<T, TU>(_leftPredicate.GetCopy(),_rightPredicate.GetCopy(),_comparator.GetCopy(),_trueBlock.GetCopy(),_falseBlock.GetCopy());
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionGenerator generator)
        {
            var newLeftPred = ReplaceNullNodesForComponent(_leftPredicate,maxDepth,generator);
            var newRightPred = ReplaceNullNodesForComponent(_rightPredicate,maxDepth,generator);
            var newTrueBlock = ReplaceNullNodesForComponent(_trueBlock,maxDepth,generator);
            var newFalseBlock = ReplaceNullNodesForComponent(_falseBlock,maxDepth,generator);

            return new IfNode<T, TU>(newLeftPred,newRightPred,_comparator.GetCopy(),newTrueBlock,newFalseBlock);
        }
    }
}