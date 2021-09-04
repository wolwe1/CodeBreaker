using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.source.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.factories.valueNodes
{
    public static class ValueNodeFactory
    {
        public static IEnumerable<ValueNode<T>> GetAll<T>() where T : IComparable
        {
            if (typeof(T) == typeof(string))
            {
                return (IEnumerable<ValueNode<T>>) StringValueNodeFactory.GetAll();
            }

            if (typeof(T) == typeof(double))
            {
                return (IEnumerable<ValueNode<T>>) DoubleValueNodeFactory.GetAll();
            }
            
            if (typeof(T) == typeof(bool))
            {
                return (IEnumerable<ValueNode<T>>) BooleanValueNodeFactory.GetAll();
            }
            
            throw new InvalidOperationException($"Unable to generate value nodes of type {typeof(T)}");
        }

        public static ValueNode<T> Get<T>() where T : IComparable
        {
            if (typeof(T) == typeof(string))
            {
                return (ValueNode<T>) (object) StringValueNodeFactory.Get();
            }

            if (typeof(T) == typeof(double))
            {
                return (ValueNode<T>) (object) DoubleValueNodeFactory.Get();
            }
            
            if (typeof(T) == typeof(bool))
            {
                return (ValueNode<T>) (object) BooleanValueNodeFactory.Get();
            }
            
            throw new InvalidOperationException($"Unable to generate value node of type {typeof(T)}");
        }
    }
}