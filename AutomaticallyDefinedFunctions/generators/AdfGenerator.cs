using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.factories.valueNodes;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.generators
{
    public class AdfGenerator<T>  where T : IComparable
    {
        private readonly FunctionDefinitionHolder<T> _definitionHolder;
        private readonly FunctionGenerator _functionGenerator;
        private readonly AdfSettings _settings;
        
        //Helpers
        private readonly FunctionDefinitionGenerator<T> _definitionGenerator;
        private readonly MainGenerator<T> _mainGenerator;

        public AdfGenerator(int seed, AdfSettings settings)
        {
            RandomNumberFactory.SetSeed(seed);
            
            _settings = settings;
            
            _definitionHolder = new FunctionDefinitionHolder<T>();
            _functionGenerator = new FunctionGenerator(settings);
            _definitionGenerator = new FunctionDefinitionGenerator<T>(_functionGenerator, settings);
            _mainGenerator = new MainGenerator<T>(_functionGenerator, settings);
            
            Reset();
        }
        
        public Adf<T> Generate()
        {
            var newAdf = new Adf<T>();

            for (var i = 0; i < _settings.NumberOfFunctions; i++)
            {
                var function = _definitionGenerator.GenerateFunctionDefinition(i);
                _definitionHolder.AddDefinition(function);

                newAdf.UseDefinition(function);
            }
            
            _functionGenerator.UseFactory(_definitionHolder);
            
            for (var i = 0; i < _settings.ArgumentCount; i++) 
            {
                var main = _mainGenerator.GenerateMainFunction();
                
                newAdf.UseMain(main);
            }
            
            Reset();
            return newAdf;
        }

        public INode<T> GenerateSubTree(int maxDepth)
        {
            Reset();
            _functionGenerator.UseNullTerminals(false);
            return _functionGenerator.CreateFunction<T>(maxDepth);
        }

        public Adf<T> GenerateFromId(string originalId)
        {
            //Must reset factory to clear existing definitions
            Reset();
            //Generating from ID requires a value node factory
            _functionGenerator.UseFactory(new ValueNodeFactory());

            var (mainProgramsIdList, functionDefinitionsIdList) = AdfParser.ParseAdfId(originalId);

            var mainPrograms = _mainGenerator.GenerateMainsFromIdList(mainProgramsIdList);
            var definitions = _definitionGenerator.GenerateFunctionsFromIdList(functionDefinitionsIdList);

            return CreateAdfFrom(mainPrograms, definitions);
        }

        private Adf<T> CreateAdfFrom(IEnumerable<MainProgram<T>> mainPrograms, IEnumerable<FunctionDefinition<T>> definitions)
        {
            var adf = new Adf<T>();

            foreach (var definition in definitions)
            {
                adf.UseDefinition(definition);
            }

            foreach (var mainProgram in mainPrograms)
            {
                adf.UseMain(mainProgram);
            }

            return adf;
        }

        private void Reset()
        {
            _definitionHolder.Clear();
            _functionGenerator
                .ClearFactories()
                .UseFactory(new AddFunctionFactory())
                .UseFactory(new IfFunctionFactory())
                .UseFactory(new LoopFunctionFactory());
        }

        public FunctionDefinition<T> GenerateFunctionFromId(string id)
        {
            //Must reset factory to clear existing definitions
            Reset();
            //Generating from ID requires a value node factory
            _functionGenerator.UseFactory(new ValueNodeFactory());
            return _definitionGenerator.GenerateFunctionFromId(id,0);
        }

        public MainProgram<T> GenerateMainFromId(string id)
        {
            //Must reset factory to clear existing definitions
            Reset();
            //Generating from ID requires a value node factory
            _functionGenerator.UseFactory(new ValueNodeFactory());
            return _mainGenerator.GenerateMainFromId(id);
        }

        public FunctionGenerator GetFunctionGenerator()
        {
            return _functionGenerator;
        }
    }
}