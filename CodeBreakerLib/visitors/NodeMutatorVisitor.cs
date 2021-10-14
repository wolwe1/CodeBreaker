using System;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.visitors;
using CodeBreakerLib.connectors.ga;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.visitors
{
    public class NodeMutatorVisitor<T> : INodeVisitor where T : IComparable
    {
        private int _nodeCount;
        private readonly int _targetNodeCount;

        private readonly FunctionCreator _funcCreator;
        private readonly int _maxModificationDepth;
        private bool _replaced;
        private bool _hasReplacedTree;

        public NodeMutatorVisitor(int targetNodeCount, IPopulationGenerator<T> generator, int maxModificationDepth)
        {
            _nodeCount = 0;
            _targetNodeCount = targetNodeCount;
            _funcCreator = ( (AdfPopulationGenerator<T>) generator).GetFunctionCreator();
            _maxModificationDepth = maxModificationDepth;
            _replaced = false;
        }

        public void Accept(INode node)
        {
            if (_replaced) return;

            if (_nodeCount == _targetNodeCount)
            {
                ReplaceNodeParent(node);
                _replaced = true;
                _hasReplacedTree = true;
            }

            _nodeCount++;
        }

        public bool WantsToVisit()
        {
            return !_hasReplacedTree;
        }

        private void ReplaceNodeParent(INode node)
        {
            //Parent should always be a function node, since a value node cannot have children
            var parent = (ChildManager)node.Parent;
            var newChild = CreateNewSubtree(node);
            parent.SetChild(node, newChild);

        }

        private INode CreateNewSubtree(INode node)
        {

            if (node is INode<string>)
            {
                return _funcCreator.Choose<string>(_maxModificationDepth);
            }
            
            if (node is INode<double>)
            {
                return _funcCreator.Choose<double>(_maxModificationDepth);
            }
            
            if (node is INode<bool>)
            {
                return _funcCreator.Choose<bool>(_maxModificationDepth);
            }

            throw new Exception($"Could not create mutated tree for type {node.GetType()}");
        }
    }
}