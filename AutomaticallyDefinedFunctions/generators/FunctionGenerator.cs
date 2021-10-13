using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories.comparators;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.generators.adf;

namespace AutomaticallyDefinedFunctions.generators
{
    public abstract class FunctionGenerator
    {
        protected readonly FunctionCreator FunctionCreator;
        protected readonly AdfSettings Settings;

        protected readonly List<IFunctionFactory> Factories;
        protected readonly List<ComparatorFactory> Comparators;

        protected FunctionGenerator(AdfSettings settings,bool useNullTerminals)
        {
            FunctionCreator = new FunctionCreator(settings,useNullTerminals);
            Settings = settings;
            Factories = settings.Factories;
            Comparators = settings.Comparators;
            SetFactories();
        }
        
        protected void SetFactories()
        {
            FunctionCreator.ClearFactories();
            Factories.ForEach(f => FunctionCreator.UseFactory(f));
            Comparators.ForEach(c => FunctionCreator.UseComparator(c));
        }
        
        public void Reset()
        {
            FunctionCreator.ClearFactories();
            SetFactories();
        }
        
    }
}