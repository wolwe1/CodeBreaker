using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic;
using AutomaticallyDefinedFunctions.factories.functionFactories.operators;
using AutomaticallyDefinedFunctions.factories.valueNodes;
using AutomaticallyDefinedFunctions.structure;

namespace AutomaticallyDefinedFunctions.generators
{
    public class MainGenerator<T> where T : IComparable
    {
        private readonly FunctionGenerator _functionGenerator;
        private readonly AdfSettings _settings;

        public MainGenerator( AdfSettings settings)
        {
            _functionGenerator = new FunctionGenerator(settings,false);
            _settings = settings;
            SetFactories();
        }

        public void AddDefinitions(FunctionDefinitionHolder<T> definitionHolder)
        {
            _functionGenerator.UseFactory(definitionHolder);
        }
                
        public MainProgram<T> GenerateMainFunction()
        {
            var mainTree = _functionGenerator.CreateFunction<T>(_settings.MaxMainDepth);
            var main = new MainProgram<T>(mainTree);
            
            return main;
        }
        
        public MainProgram<T> GenerateMainFromId(string id)
        {
            _functionGenerator.UseFactory(new ValueNodeFactory());
            var functionId = id["Main".Length..];
            var newFunction = _functionGenerator.GenerateFunctionFromId<T>(functionId);

            return new MainProgram<T>(newFunction);
        }

        private MainProgram<T> GenerateMainFromIdNoAdd(string id)
        {
            var functionId = id["Main".Length..];
            var newFunction = _functionGenerator.GenerateFunctionFromId<T>(functionId);

            return new MainProgram<T>(newFunction);
        }
        
        public IEnumerable<MainProgram<T>> GenerateMainsFromIdList(IEnumerable<string> ids)
        {
            _functionGenerator.UseFactory(new ValueNodeFactory());
            return ids.Select(GenerateMainFromIdNoAdd);
        }
        
        public void Reset()
        {
            _functionGenerator.ClearFactories();
            SetFactories();
        }
        
        private void SetFactories()
        {
            _functionGenerator
                .UseFactory(new AddFunctionFactory())
                .UseFactory(new SubtractFunctionFactory())
                .UseFactory(new MultiplicationFunctionFactory())
                .UseFactory(new DivisionFunctionFactory())
                .UseFactory(new IfFunctionFactory())
                .UseFactory(new LoopFunctionFactory());
        }

        public FunctionGenerator GetGeneratorCopy()
        {
            return new FunctionGenerator(_settings, false)
                .UseFactory(new AddFunctionFactory())
                .UseFactory(new SubtractFunctionFactory())
                .UseFactory(new MultiplicationFunctionFactory())
                .UseFactory(new DivisionFunctionFactory())
                .UseFactory(new IfFunctionFactory())
                .UseFactory(new LoopFunctionFactory());
        }
    }
}