using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.structure;

namespace AutomaticallyDefinedFunctions.generators
{
    public class FunctionDefinitionGenerator<T> where T : IComparable
    {
        private readonly FunctionGenerator _functionGenerator;
        private readonly AdfSettings _settings;

        public FunctionDefinitionGenerator(FunctionGenerator generator, AdfSettings settings)
        {
            _functionGenerator = generator;
            _settings = settings;
        }

        public FunctionDefinition<T> GenerateFunctionDefinition(int functionCount)
        {
            _functionGenerator.UseNullTerminals(true);
            return FunctionDefinition<T>.Create($"ADF{functionCount}")
                .UseFunction(_functionGenerator.CreateFunction<T>(_settings.MaxFunctionDepth));
        }

        
        public IEnumerable<FunctionDefinition<T>> GenerateFunctionsFromIdList(IEnumerable<string> ids)
        {
            return ids.Select(GenerateFunctionFromId);
        }
        
        public FunctionDefinition<T> GenerateFunctionFromId(string id,int functionCount)
        {
            return FunctionDefinition<T>.Create($"ADF{functionCount}")
                .UseFunction(_functionGenerator.GenerateFunctionFromId<T>(id));
        }
    }
}