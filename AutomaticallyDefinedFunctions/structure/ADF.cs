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
        private readonly List<MainProgram<T>> _mainPrograms;
        private readonly List<FunctionDefinition<T>> _functionDefinitions;
        private readonly AdfValidator _validator;

        public Adf()
        {
            _mainPrograms = new List<MainProgram<T>>();
            _functionDefinitions = new List<FunctionDefinition<T>>();
            _validator = new AdfValidator();
        }

        private Adf(List<MainProgram<T>> mainPrograms, List<FunctionDefinition<T>> functionDefinitions)
        {
            _mainPrograms = mainPrograms;
            _functionDefinitions = functionDefinitions;
        }

        public IEnumerable<T> GetValues()
        {
            return _mainPrograms.Select(main =>
            {
                try
                {
                    return main.GetValue();
                }
                catch (ProgramLoopException)
                {
                    return default(T);
                }
            });
        }

        public Adf<T> UseDefinition(FunctionDefinition<T> definition)
        {
            _functionDefinitions.Add(definition);
            return this;
        }
        
        public Adf<T> UseMain(MainProgram<T> main)
        {
            _mainPrograms.Add(main);
            return this;
        }

        public bool IsValid()
        {
            return _validator.IsValid(this);
        }

        public IEnumerable<FunctionDefinition<T>> GetDefinitions()
        {
            return _functionDefinitions.Select(function => (FunctionDefinition<T>)function.GetCopy());
        }

        public IEnumerable<MainProgram<T>> GetMainPrograms()
        {
            return _mainPrograms.Select(main => main.GetCopy());
        }

        public string GetId()
        {
            var mainIds = string.Join(";", GetMainProgramIds());
            var functionIds = string.Join(";", GetFunctionIds());
            
            return $"ADF({mainIds}-{functionIds})";
        }
        
        private IEnumerable<string> GetMainProgramIds()
        {
            return _mainPrograms.Select(main => main.GetId());
        }

        private IEnumerable<string> GetFunctionIds()
        {
            return _functionDefinitions.Select(func => func.GetId());
        }

        public int GetMainNodeCount(int mainIndex)
        {
            return _mainPrograms.ElementAt(mainIndex).GetNodeCount();
        }

        public Adf<T> GetCopy()
        {
            var mainProgramCopies = _mainPrograms.Select(main => main.GetCopy());
            var functionDefinitionCopies = _functionDefinitions.Select(func => (FunctionDefinition<T>)func.GetCopy());
            return new Adf<T>(mainProgramCopies.ToList(), functionDefinitionCopies.ToList());
        }

        public int GetNumberOfMainPrograms()
        {
            return _mainPrograms.Count;
        }

        public Adf<T> ReplaceNodeInMain(int mainIndex, int nodeIndexToReplace,FunctionGenerator generator,int maxDepth)
        {
            _mainPrograms[mainIndex] = _mainPrograms[mainIndex].ReplaceNode(nodeIndexToReplace, generator,maxDepth);
            return this;
        }
        
        public int GetNumberOfDefinitions()
        {
            return _functionDefinitions.Count;
        }
        
        public FunctionDefinition<T> GetFunctionDefinition(int index)
        {
            return (FunctionDefinition<T>) _functionDefinitions[index].GetCopy();
        }

        public Adf<T> SetFunctionDefinition(int definitionIndex, FunctionDefinition<T> newDefinition)
        {
            var copy = GetCopy();
            copy._functionDefinitions[definitionIndex] = newDefinition;

            return copy;
        }
    }
}