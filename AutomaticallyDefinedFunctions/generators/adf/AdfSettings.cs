using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories.comparators;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic;
using AutomaticallyDefinedFunctions.factories.functionFactories.operators;
using AutomaticallyDefinedFunctions.factories.valueNodes;
using AutomaticallyDefinedFunctions.factories.valueNodes.standard;

namespace AutomaticallyDefinedFunctions.generators.adf
{
    public class AdfSettings
    {
        public readonly int MaxFunctionDepth;
        public readonly int MaxMainDepth;
        public readonly int ArgumentCount;
        public readonly int TerminalChance;

        public List<IFunctionFactory> Factories { get; set; }
        public List<ComparatorFactory> Comparators { get; set; }
        public List<IValueNodeFactory> ValueNodeFactories { get; set; }
        
        //Optional
        public readonly int NumberOfFunctions;

        public AdfSettings(int maxFunctionDepth, int maxMainDepth, int argumentCount, int terminalChance)
        {
            MaxFunctionDepth = maxFunctionDepth;
            MaxMainDepth = maxMainDepth;
            ArgumentCount = argumentCount;
            TerminalChance = terminalChance;

            NumberOfFunctions = 1;
            
            SetFactories();
            SetComparators();
        }

        private void SetFactories()
        {
            Factories = new List<IFunctionFactory>()
            {
                new AddFunctionFactory(),
                new SubtractFunctionFactory(),
                new MultiplicationFunctionFactory(),
                new DivisionFunctionFactory(),
                new IfFunctionFactory(),
                new LoopFunctionFactory(),
                new LengthOfFunctionFactory()
            };

            ValueNodeFactories = new List<IValueNodeFactory>()
            {
                new ValueNodeFactory()
            };
        }
        
        private void SetComparators()
        {
            Comparators = new List<ComparatorFactory>()
                {
                    new LessThanComparatorFactory(),
                    new GreaterThanComparatorFactory(),
                    new EqualComparatorFactory(),
                    new NotEqualComparatorFactory(),
                };
        }
    }
}