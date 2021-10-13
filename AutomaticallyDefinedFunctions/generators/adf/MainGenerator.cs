using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.factories.valueNodes;
using AutomaticallyDefinedFunctions.structure;

namespace AutomaticallyDefinedFunctions.generators.adf
{
    public class MainGenerator<T> : FunctionGenerator where T : IComparable
    {
        public MainGenerator(AdfSettings settings) : base(settings,false) { }

        public void AddDefinitions(FunctionDefinitionHolder<T> definitionHolder)
        {
            FunctionCreator.UseFactory(definitionHolder);
        }
                
        public MainProgram<T> GenerateMainFunction()
        {
            var mainTree = FunctionCreator.CreateFunction<T>(Settings.MaxMainDepth);
            var main = new MainProgram<T>(mainTree);
            
            return main;
        }
        
        public MainProgram<T> GenerateMainFromId(string id)
        {
            FunctionCreator.UseFactory(new ValueNodeFactory());
            var functionId = id["Main".Length..];
            var newFunction = FunctionCreator.GenerateFunctionFromId<T>(functionId);

            return new MainProgram<T>(newFunction);
        }

        private MainProgram<T> GenerateMainFromIdNoAdd(string id)
        {
            var functionId = id["Main".Length..];
            var newFunction = FunctionCreator.GenerateFunctionFromId<T>(functionId);

            return new MainProgram<T>(newFunction);
        }
        
        public IEnumerable<MainProgram<T>> GenerateMainsFromIdList(IEnumerable<string> ids)
        {
            FunctionCreator.UseFactory(new ValueNodeFactory());
            return ids.Select(GenerateMainFromIdNoAdd);
        }

        public FunctionCreator GetGeneratorCopy()
        {
            var newCreator = new FunctionCreator(Settings, false);
            Factories.ForEach(f => newCreator.UseFactory(f));
            Comparators.ForEach(c => newCreator.UseComparator(c));

            return newCreator;
        }
    }
}