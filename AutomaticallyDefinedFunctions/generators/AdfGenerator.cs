using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.factories;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.generators
{
    public class AdfGenerator<T>  where T : IComparable
    {
        private readonly FunctionDefinitionHolder<T> _definitionHolder;
        private readonly AdfSettings _settings;
        
        //Helpers
        private readonly FunctionDefinitionGenerator<T> _definitionGenerator;
        private readonly MainGenerator<T> _mainGenerator;
        //For external use
        private readonly FunctionGenerator _functionGenerator;

        public AdfGenerator(int seed, AdfSettings settings)
        {
            RandomNumberFactory.SetSeed(seed);
            
            _settings = settings;
            
            _definitionHolder = new FunctionDefinitionHolder<T>();
            _definitionGenerator = new FunctionDefinitionGenerator<T>(settings);
            _mainGenerator = new MainGenerator<T>(settings);

            _functionGenerator = _mainGenerator.GetGeneratorCopy();
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
            
            _mainGenerator.AddDefinitions(_definitionHolder);
            
            for (var i = 0; i < _settings.ArgumentCount; i++) 
            {
                var main = _mainGenerator.GenerateMainFunction();
                
                newAdf.UseMain(main);
            }

            _mainGenerator.Reset();
            return newAdf;
        }

        public INode<T> GenerateSubTree(int maxDepth)
        {
            return _definitionGenerator.CreateFunction(maxDepth);
        }

        public Adf<T> GenerateFromId(string originalId)
        {
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
        
        public FunctionDefinition<T> GenerateFunctionFromId(string id)
        {
            return _definitionGenerator.GenerateFunctionFromId(id,0);
        }

        public MainProgram<T> GenerateMainFromId(string id)
        {
            return _mainGenerator.GenerateMainFromId(id);
        }

        public FunctionGenerator GetFunctionGenerator()
        {
            return _functionGenerator;
        }
    }
}