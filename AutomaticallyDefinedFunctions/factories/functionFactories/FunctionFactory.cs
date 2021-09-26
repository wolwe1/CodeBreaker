using System;
using System.Text.RegularExpressions;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.factories.functionFactories
{
    public abstract class FunctionFactory : IFunctionFactory
    {
        private readonly string _symbol;

        protected FunctionFactory(string symbol)
        {
            _symbol = symbol;
        }

        public abstract FunctionNode<T> Get<T, TU>(int maxDepth, FunctionGenerator parent)
            where T : IComparable where TU : IComparable;

        public abstract bool CanDispatchFunctionOfType(Type t);

        public bool CanMap(string id)
        {
            return Equals(id[0].ToString(), _symbol);
        }
        
        public INode<T> GenerateFunction<T>(string id, FunctionGenerator functionGenerator) where T : IComparable
        {
            if(!CanMap(id)) throw new Exception($"Cannot generate statement from ID beginning with {AdfParser.GetTypeInfo(id,_symbol)}");
            
            var typeInfo = AdfParser.GetTypeInfo(id,_symbol);
            
            if(typeInfo == "")
                return GenerateFunction<T,T>(id[1..],functionGenerator);
            
            var auxType = AdfParser.GetAuxType(typeInfo);
            
            var idForChildren = id[typeInfo.Length..];
            
            if(auxType == typeof(string))
                return GenerateFunction<T, string>(idForChildren,functionGenerator);
            if(auxType == typeof(double))
                return GenerateFunction<T, double>(idForChildren,functionGenerator);
            if(auxType == typeof(bool))
                return GenerateFunction<T, bool>(idForChildren,functionGenerator);
            
            throw new Exception("Type information for function not found");
            
        }

        protected abstract INode<T> GenerateFunction<T, TU>(string id, FunctionGenerator functionGenerator)
            where T : IComparable where TU : IComparable;
        
    }
}