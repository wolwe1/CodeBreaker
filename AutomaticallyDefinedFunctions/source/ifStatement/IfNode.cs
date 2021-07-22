using System;
using AutomaticallyDefinedFunctions.source.nodes;

namespace AutomaticallyDefinedFunctions.source.ifStatement
{
    public class IfNode<T,U> : FunctionNode<U> where T : IComparable where U : IComparable
    {
        private INode<T> _leftPredicate;
        private INode<T> _rightPredicate;
        private NodeComparator<T> _comparator;
        private INode<U> _trueBlock;
        private INode<U> _falseBlock;

        public IfNode() : base()
        {
            
        }

        public void SetLeftPredicate(INode<T> predicateRoot)
        {
            _leftPredicate = predicateRoot;
        }
        
        public void SetRightPredicate(INode<T> predicateRoot)
        {
            _rightPredicate = predicateRoot;
        }
        
        public void SetComparisonOperator(NodeComparator<T> comparator)
        {
            _comparator = comparator;
        }
        
        public void SetTrueCodeBlock(INode<U> root)
        {
            _trueBlock = root;
        }
        
        public void SetFalseCodeBlock(INode<U> root)
        {
            _falseBlock = root;
        }

        public override U GetValue()
        {
            return _comparator.PassesCheck(_leftPredicate, _rightPredicate) ? 
                _trueBlock.GetValue() : _falseBlock.GetValue();
        }

        public override bool IsValid()
        {
            return _leftPredicate != null && _rightPredicate != null && _comparator != null &&
                   _trueBlock != null && _falseBlock != null;
        }
    }
}