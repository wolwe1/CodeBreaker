﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.exceptions;
using AutomaticallyDefinedFunctions.structure.adf.helpers;
using AutomaticallyDefinedFunctions.structure.visitors;

namespace AutomaticallyDefinedFunctions.structure.adf
{
    public class Adf<T> : IAdf where T : IComparable
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
        
        public AdfOutput<T> GetValues()
        {
            return new AdfOutput<T>(MainPrograms.Select(main =>
            {
                try
                {
                    return new Output<T>(main.GetValue());
                }
                catch (ProgramLoopException)
                {
                    return new Output<T>();
                }
            }));
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
            
            return $"ADF({mainIds}*{functionIds})";
        }
        
        public IEnumerable<string> GetMainProgramIds()
        {
            return MainPrograms.Select(main => main.GetId());
        }

        public IEnumerable<string> GetFunctionIds()
        {
            return FunctionDefinitions.Select(func => func.GetId());
        }

        public int GetMainNodeCount(int mainIndex)
        {
            return MainPrograms.ElementAt(mainIndex).GetNodeCount();
        }

        public virtual Adf<T> GetCopy()
        {
            var mainProgramCopies = MainPrograms.Select(main => main.GetCopy());
            var functionDefinitionCopies = FunctionDefinitions.Select(func => (FunctionDefinition<T>)func.GetCopy());
            return new Adf<T>(mainProgramCopies.ToList(), functionDefinitionCopies.ToList());
        }

        public int GetNumberOfMainPrograms()
        {
            return MainPrograms.Count;
        }

        public Adf<T> VisitMain(int mainIndex, INodeVisitor visitor)
        {
            MainPrograms[mainIndex].Visit(visitor);
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

        public void SetFunctionDefinition(int definitionIndex, FunctionDefinition<T> newDefinition)
        {
            FunctionDefinitions[definitionIndex] = newDefinition;
        }
        
        public void SetMain(int index, MainProgram<T> newMain)
        {
            MainPrograms[index] = newMain;
        }
    }
}