using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.factories.functionFactories.arithmetic;
using AutomaticallyDefinedFunctions.factories.functionFactories.operators;
using AutomaticallyDefinedFunctions.factories.valueNodes;
using AutomaticallyDefinedFunctions.structure;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.generators
{
    public class FunctionDefinitionGenerator<T> where T : IComparable
    {
        private readonly FunctionGenerator _functionGenerator;
        private readonly AdfSettings _settings;

        public FunctionDefinitionGenerator(AdfSettings settings)
        {
            _functionGenerator = new FunctionGenerator(settings,true);
            _settings = settings;
            SetFactories();
        }

        public FunctionDefinition<T> GenerateFunctionDefinition(int functionCount)
        {
            return FunctionDefinition<T>.Create($"ADF{functionCount}")
                .UseFunction(_functionGenerator.CreateFunction<T>(_settings.MaxFunctionDepth));
        }

        
        public IEnumerable<FunctionDefinition<T>> GenerateFunctionsFromIdList(IEnumerable<string> ids)
        {
            _functionGenerator.UseFactory(new ValueNodeFactory());
            return ids.Select(GenerateFunctionFromIdNoAdd);
        }

        //Prevent needless addition of value node factories
        private FunctionDefinition<T> GenerateFunctionFromIdNoAdd(string id,int functionCount)
        {
            return FunctionDefinition<T>.Create($"ADF{functionCount}")
                .UseFunction(_functionGenerator.GenerateFunctionFromId<T>(id));
        }
        public FunctionDefinition<T> GenerateFunctionFromId(string id,int functionCount)
        {
            _functionGenerator.UseFactory(new ValueNodeFactory());
            return GenerateFunctionFromIdNoAdd(id, functionCount);
        }

        public INode<T> CreateFunction(int maxDepth)
        {
            return _functionGenerator.CreateFunction<T>(maxDepth);
        }

        public FunctionGenerator GetGenerator()
        {
            return _functionGenerator;
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
    }
}