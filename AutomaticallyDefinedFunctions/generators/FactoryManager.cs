using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories.comparators;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.factories.valueNodes;
using AutomaticallyDefinedFunctions.generators.adf;

namespace AutomaticallyDefinedFunctions.generators
{
    public abstract class FactoryManager
    {
        protected readonly List<IFunctionFactory> Factories;
        protected readonly List<ComparatorFactory> Comparators;
        protected readonly List<IValueNodeFactory> ValueNodeFactories;

        protected FactoryManager(AdfSettings settings)
        {
            Factories = settings.Factories;
            Comparators = settings.Comparators;
            ValueNodeFactories = settings.ValueNodeFactories;
        }
        
        public FactoryManager UseFactory(IFunctionFactory factory)
        {
            Factories.Add(factory);
            return this;
        }
        
        public FactoryManager UseComparator(ComparatorFactory factory)
        {
            Comparators.Add(factory);
            return this;
        }
        
        public FactoryManager UseValueNodeFactory(IValueNodeFactory factory)
        {
            ValueNodeFactories.Add(factory);
            return this;
        }
        
        public FactoryManager ClearFactories()
        {
            Factories.Clear();
            Comparators.Clear();
            ValueNodeFactories.Clear();
            return this;
        }
    }
}