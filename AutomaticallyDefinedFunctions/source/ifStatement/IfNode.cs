using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.source.ifStatement
{
    public class IfNode<T,U> : FunctionNode<T> where T : IComparable where U : IComparable
    {
        private readonly INode<U> _leftPredicate;
        private readonly INode<U> _rightPredicate;
        private readonly NodeComparator<U> _comparator;
        private readonly INode<T> _trueBlock;
        private readonly INode<T> _falseBlock;

        private IfNode(INode<U> leftPredicate, INode<U> rightPredicate, NodeComparator<U> comparator, INode<T> trueBlock, INode<T> falseBlock) : base()
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
        
        public IfNode<T,U> SetLeftPredicate(INode<U> predicateRoot)
        {
            return new IfNode<T, U>(predicateRoot, _rightPredicate, _comparator, _trueBlock, _falseBlock);
        }
        
        public IfNode<T,U> SetRightPredicate(INode<U> predicateRoot)
        {
            return new IfNode<T, U>(_leftPredicate, predicateRoot, _comparator, _trueBlock, _falseBlock);
        }
        
        public IfNode<T,U> SetComparisonOperator(NodeComparator<U> comparator)
        {
            return new IfNode<T, U>(_leftPredicate, _rightPredicate, comparator, _trueBlock, _falseBlock);
        }
        
        public IfNode<T,U> SetTrueCodeBlock(INode<T> root)
        {
            return new IfNode<T, U>(_leftPredicate, _rightPredicate, _comparator, root, _falseBlock);
        }
        
        public IfNode<T,U> SetFalseCodeBlock(INode<T> root)
        {
            return new IfNode<T, U>(_leftPredicate, _rightPredicate, _comparator, _trueBlock, root);
        }

        public override T GetValue()
        {
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
            
            if (_leftPredicate != null)
            {
                numberOfNullNodes += _leftPredicate.GetNullNodeCount();
            }
            
            if (_rightPredicate != null)
            {
                numberOfNullNodes += _rightPredicate.GetNullNodeCount();
            }
            
            if (_trueBlock != null)
            {
                numberOfNullNodes += _trueBlock.GetNullNodeCount();
            }
            
            if (_falseBlock != null)
            {
                numberOfNullNodes += _falseBlock.GetNullNodeCount();
            }

            return numberOfNullNodes;
  
        }
    }
}