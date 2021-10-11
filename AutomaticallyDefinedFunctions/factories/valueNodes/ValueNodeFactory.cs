using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.nodes;
using AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace AutomaticallyDefinedFunctions.factories.valueNodes
{
    public class ValueNodeFactory : FunctionFactory
    {
        public ValueNodeFactory(): base(NodeCategory.ValueNode){}
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
        
        private static INode<T> Get<T>(string id) where T : IComparable
        {
            if (id.StartsWith("Null"))
                return new NullNode<T>();
            
            if (typeof(T) == typeof(string))
            {
                return (ValueNode<T>) (object) StringValueNodeFactory.Get(id);
            }

            if (typeof(T) == typeof(double))
            {
                return (ValueNode<T>) (object) DoubleValueNodeFactory.Get(id);
            }
            
            if (typeof(T) == typeof(bool))
            {
                return (ValueNode<T>) (object) BooleanValueNodeFactory.Get(id);
            }
            
            throw new InvalidOperationException($"Unable to generate value node of type {typeof(T)}");
        }

        public static INode<T> GetNull<T>() where T : IComparable
        {
            return new NullNode<T>();
        }

        public override FunctionNode<T> CreateFunction<T, TU>(int maxDepth, FunctionGenerator parent)
        {
            throw new NotImplementedException();
        }

        public override bool CanDispatchFunctionOfType(Type t)
        {
            return false;
        }

        protected override INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionGenerator functionGenerator)
        {
            return Get<T>(AdfParser.GetValueFromQuotes(id));
        }

        
    }
}