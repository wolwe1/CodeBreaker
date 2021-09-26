using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.structure;

namespace AutomaticallyDefinedFunctions.generators
{
    public class MainGenerator<T> where T : IComparable
    {
        private readonly FunctionGenerator _functionGenerator;
        private readonly AdfSettings _settings;

        public MainGenerator(FunctionGenerator generator, AdfSettings settings)
        {
            _functionGenerator = generator;
            _settings = settings;
        }

                
        public MainProgram<T> GenerateMainFunction()
        {
            _functionGenerator.UseNullTerminals(false);
            var mainTree = _functionGenerator.CreateFunction<T>(_settings.MaxMainDepth);
            var main = new MainProgram<T>(mainTree);
            
            return main;
        }
        
        public MainProgram<T> GenerateMainFromId(string id)
        {
            var functionId = id["Main".Length..];
            var newFunction = _functionGenerator.GenerateFunctionFromId<T>(functionId);

            return new MainProgram<T>(newFunction);
        }
        
        public IEnumerable<MainProgram<T>> GenerateMainsFromIdList(IEnumerable<string> ids)
        {
            return ids.Select(GenerateMainFromId);
        }
        
    }
}