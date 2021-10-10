using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories;
using AutomaticallyDefinedFunctions.factories.comparators;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.factories.valueNodes;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure.functions;
using AutomaticallyDefinedFunctions.structure.functions.ifStatement.comparators;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.generators
{
    public class FunctionGenerator
    {
        private readonly List<IFunctionFactory> _factories;
        private readonly bool _useNullTerminals;
        private readonly AdfSettings _settings;
        
        public FunctionGenerator(AdfSettings settings,bool useNullTerminals)
        {
            _factories = new List<IFunctionFactory>();
            _useNullTerminals = useNullTerminals;
            _settings = settings;
        }
        
        public FunctionNode<T> CreateFunction<T>(int maxDepth) where T : IComparable
        {
            var choice = RandomNumberFactory.Next(3);

            return choice switch
            {
                0 => CreateFunction<T, string>(maxDepth),
                1 => CreateFunction<T, bool>(maxDepth),
                2 => CreateFunction<T, double>(maxDepth),
                _ => throw new InvalidOperationException("Cannot dispatch function factory on type " + typeof(T))
            };
        }

        private FunctionNode<T> CreateFunction<T, TU>(int maxDepth) where T : IComparable where TU : IComparable
        {
            var factoriesThatCanDispatch = GetFactoriesThatCanDispatch<T>();
            
            var choice = RandomNumberFactory.Next(factoriesThatCanDispatch.Count());

            return factoriesThatCanDispatch.ElementAt(choice).Get<T, TU>(maxDepth, this);
        }

        private IEnumerable<IFunctionFactory> GetFactoriesThatCanDispatch<T>()
        {
            return _factories.Where(f => f.CanDispatchFunctionOfType(typeof(T)));
        }
    

        public NodeComparator<T> ChooseComparator<T>() where T : IComparable
        {
            var choice = RandomNumberFactory.Next(3);

            return choice switch
            {
                0 => ComparatorFactory.CreateEqualsComparator<T>(),
                1 => ComparatorFactory.CreateLessThanComparator<T>(),
                2 => ComparatorFactory.CreateGreaterThanComparator<T>(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public static NodeComparator<T> ChooseComparator<T>(ref string id) where T : IComparable
        {
            var comparator = ComparatorFactory.Get<T>(id[0]);
            id = id[1..];
            
            return comparator;
        }

        public INode<T> Choose<T>(int maxDepth) where T : IComparable
        {
            if (maxDepth <= 0) return GetTerminal<T>();
            
            var terminalOrFunction = RandomNumberFactory.AboveThreshold(_settings.TerminalChance);

            return terminalOrFunction switch
            {
                false => GetTerminal<T>(),
                true => CreateFunction<T>(maxDepth - 1)
            };
        }

        public INode<T> GetTerminal<T>() where T : IComparable
        {
            return _useNullTerminals ? ValueNodeFactory.GetNull<T>() : ValueNodeFactory.Get<T>();
        }

        public FunctionGenerator UseFactory(IFunctionFactory factory)
        {
            _factories.Add(factory);
            return this;
        }
        
        public FunctionGenerator ClearFactories()
        {
            _factories.Clear();

            return this;
        }

        public INode<T> GenerateFunctionFromId<T>(string id) where T : IComparable
        {
            id = AdfParser.GetIdWithoutDelimiters(id);
            var generator = _factories.FirstOrDefault(f => f.CanMap(id));
            if (generator == null)
                throw new Exception($"Unable to find generator for ID '{id}'");
            
            return generator.GenerateFunction<T>(id,this);
        }
        
        public INode<T> GenerateChildFromId<T>(ref string id) where T : IComparable
        {
            var child = GenerateFunctionFromId<T>(id);
            var childId = child.GetId();
            
            var remainingId = GetIdAfter(id,childId);
            
            id = remainingId;
            
            return child;
        }

        private string GetIdAfter(string id, string afterSubstring)
        {
            var afterSubstringWithoutDelimiters = AdfParser.GetIdWithoutDelimiters(afterSubstring);
            return id[(id.IndexOf(afterSubstringWithoutDelimiters, StringComparison.Ordinal) + afterSubstringWithoutDelimiters.Length)..];
        }
        
    }
}