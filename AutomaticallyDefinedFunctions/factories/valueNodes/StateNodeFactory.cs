using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.nodes.statenodes;

namespace AutomaticallyDefinedFunctions.factories.valueNodes
{
    public class StateNodeFactory<TAdf,TProgResponse> : IValueNodeFactory
    {
        public INode<T> Get<T>() where T : IComparable
        {
            var listOfApplicableNodes = new List<StateNode<T>>();

            //The desired type of the node is the same as the output of the adf
            if (typeof(T) == typeof(TAdf))
            {
                listOfApplicableNodes.Add(new LastOutputStateNode<T>(default));
            }
            
            //The desired type of the node is the same as the output of the adf
            if (typeof(T) == typeof(TProgResponse))
            {
                listOfApplicableNodes.Add(new ProgramResponseStateNode<T>(default));
            }
            
            if (typeof(T) == typeof(double))
            {
                return (INode<T>) new ExecutionCountStateNode();
            }

            if(listOfApplicableNodes.Count == 0)
                throw new Exception($"State node factory could not dispatch type {typeof(T)}");
            
            var choice = RandomNumberFactory.Next(listOfApplicableNodes.Count);

            return listOfApplicableNodes[choice];

        }

        public bool CanDispatch<T>()
        {
            return typeof(T) == typeof(double) || typeof(T) == typeof(TAdf) || typeof(T) == typeof(TProgResponse);
        }

        public bool CanDispatchAux<T>()
        {
            return true;
        }
    }
}