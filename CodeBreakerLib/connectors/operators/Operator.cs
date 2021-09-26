using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories;
using AutomaticallyDefinedFunctions.structure;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors.operators
{
    public abstract class Operator<T> : IOperator<T> where T : IComparable
    {
        protected readonly int ApplicationPercentage;
        
        public Operator(int applicationPercentage)
        {
            ApplicationPercentage = applicationPercentage;
        }

        public IEnumerable<string> CreateModifiedChildren(List<string> parents, IPopulationGenerator<T> populationGenerator)
        {
            var numberOfOffspringToProduce = GetNumberOfOffspringToProduce(parents);

            var offspring = new List<string>();

            while (offspring.Count < numberOfOffspringToProduce)
            {
                var mutatedOffspring = Operate(parents,populationGenerator);
                offspring.AddRange(mutatedOffspring);
            }

            return offspring;
        }

        protected abstract IEnumerable<string> Operate(List<string> parents, IPopulationGenerator<T> populationGenerator);

        protected string PickParent(List<string> parents)
        {
            var chosenParentIndex = RandomNumberFactory.Next(parents.Count);
            return parents.ElementAt(chosenParentIndex);
        }
        protected int GetNumberOfOffspringToProduce(ICollection parents)
        {
            var applicationPercentage = ApplicationPercentage / 100d;

            var numberOfOffspring = applicationPercentage * parents.Count;

            return (int) numberOfOffspring;
        }


        protected Adf<T> GetAdfFromParents(List<string> parents,IPopulationGenerator<T> generator)
        {
            var parent = PickParent(parents);

            var pop = (AdfPopulationMember<T>)generator.GenerateFromId(parent);
            return pop.GetAdf();
        }
    }
}