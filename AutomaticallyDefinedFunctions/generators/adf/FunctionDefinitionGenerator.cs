using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories.valueNodes;
using AutomaticallyDefinedFunctions.parsing;
using AutomaticallyDefinedFunctions.structure;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.generators.adf
{
    public class FunctionDefinitionGenerator<T> : FunctionGenerator where T : IComparable
    {
        public FunctionDefinitionGenerator(AdfSettings settings): base(settings,true) { }

        public FunctionDefinition<T> GenerateFunctionDefinition(int functionCount)
        {
            return FunctionDefinition<T>.Create($"ADF{functionCount}")
                .UseFunction(FunctionCreator.CreateFunction<T>(Settings.MaxFunctionDepth));
        }
        
        public List<FunctionDefinition<T>> GenerateFunctionsFromIdList(IEnumerable<string> ids)
        {
            FunctionCreator.UseFactory(new ValueNodeFactory());
            return ids.Select(GenerateFunctionFromIdNoAdd).ToList();
        }

        //Prevent needless addition of value node factories
        protected FunctionDefinition<T> GenerateFunctionFromIdNoAdd(string id,int functionCount)
        {
            var idWithoutLead = id;
            if (!id.StartsWith(NodeCategory.FunctionDefinition))
                return FunctionDefinition<T>.Create($"ADF{functionCount}")
                    .UseFunction(FunctionCreator.GenerateFunctionFromId<T>(idWithoutLead));
            
            var endOfDefinitionTypeInfo = id.IndexOf(">") + 2;
            idWithoutLead = id[endOfDefinitionTypeInfo..^1];

            return FunctionDefinition<T>.Create($"ADF{functionCount}")
                .UseFunction(FunctionCreator.GenerateFunctionFromId<T>(idWithoutLead));
        }
        public FunctionDefinition<T> GenerateFunctionFromId(string id,int functionCount)
        {
            FunctionCreator.UseFactory(new ValueNodeFactory());
            return GenerateFunctionFromIdNoAdd(id, functionCount);
        }

        public INode<T> CreateFunction(int maxDepth)
        {
            return FunctionCreator.CreateFunction<T>(maxDepth);
        }

        public FunctionCreator GetGenerator()
        {
            return FunctionCreator;
        }
        
    }
}