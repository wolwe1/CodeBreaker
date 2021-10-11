using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.exceptions;
using AutomaticallyDefinedFunctions.factories.functionFactories;
using AutomaticallyDefinedFunctions.generators;
using AutomaticallyDefinedFunctions.structure.nodes;

namespace AutomaticallyDefinedFunctions.structure
{
    public class Adf<T> where T : IComparable
    {
        protected readonly List<MainProgram<T>> MainPrograms;
        protected readonly List<FunctionDefinition<T>> FunctionDefinitions;
        protected readonly AdfValidator Validator;

        public Adf()
        {
            MainPrograms = new List<MainProgram<T>>();
            FunctionDefinitions = new List<FunctionDefinition<T>>();
            Validator = new AdfValidator();
        }

        protected Adf(List<MainProgram<T>> mainPrograms, List<FunctionDefinition<T>> functionDefinitions)
        {
            MainPrograms = mainPrograms;
            FunctionDefinitions = functionDefinitions;
        }
        
        public List<T> GetValues()
        {
            return MainPrograms.Select(main =>
            {
                try
                {
                    return main.GetValue();
                }
                catch (ProgramLoopException)
                {
                    return default(T);
                }
            }).ToList();
        }

        public Adf<T> UseDefinition(FunctionDefinition<T> definition)
        {
            FunctionDefinitions.Add(definition);
            return this;
        }
        
        public Adf<T> UseMain(MainProgram<T> main)
        {
            MainPrograms.Add(main);
            return this;
        }

        public bool IsValid()
        {
            return Validator.IsValid(this);
        }

        public IEnumerable<FunctionDefinition<T>> GetDefinitions()
        {
            return FunctionDefinitions.Select(function => (FunctionDefinition<T>)function.GetCopy());
        }

        public IEnumerable<MainProgram<T>> GetMainPrograms()
        {
            return MainPrograms.Select(main => main.GetCopy());
        }

        public string GetId()
        {
            var mainIds = string.Join(";", GetMainProgramIds());
            var functionIds = string.Join(";", GetFunctionIds());
            
            return $"ADF({mainIds}-{functionIds})";
        }
        
        protected IEnumerable<string> GetMainProgramIds()
        {
            return MainPrograms.Select(main => main.GetId());
        }

        protected IEnumerable<string> GetFunctionIds()
        {
            return FunctionDefinitions.Select(func => func.GetId());
        }

        public int GetMainNodeCount(int mainIndex)
        {
            return MainPrograms.ElementAt(mainIndex).GetNodeCount();
        }

        public Adf<T> GetCopy()
        {
            var mainProgramCopies = MainPrograms.Select(main => main.GetCopy());
            var functionDefinitionCopies = FunctionDefinitions.Select(func => (FunctionDefinition<T>)func.GetCopy());
            return new Adf<T>(mainProgramCopies.ToList(), functionDefinitionCopies.ToList());
        }

        public int GetNumberOfMainPrograms()
        {
            return MainPrograms.Count;
        }

        public Adf<T> ReplaceNodeInMain(int mainIndex, int nodeIndexToReplace,FunctionGenerator generator,int maxDepth)
        {
            MainPrograms[mainIndex] = MainPrograms[mainIndex].ReplaceNode(nodeIndexToReplace, generator,maxDepth);
            return this;
        }
        
        public int GetNumberOfDefinitions()
        {
            return FunctionDefinitions.Count;
        }
        
        public FunctionDefinition<T> GetFunctionDefinition(int index)
        {
            return (FunctionDefinition<T>) FunctionDefinitions[index].GetCopy();
        }

        public Adf<T> SetFunctionDefinition(int definitionIndex, FunctionDefinition<T> newDefinition)
        {
            var copy = GetCopy();
            copy.FunctionDefinitions[definitionIndex] = newDefinition;

            return copy;
        }
    }
}