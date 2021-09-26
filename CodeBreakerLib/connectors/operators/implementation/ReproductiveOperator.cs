using System;
using System.Collections.Generic;
using System.Linq;
using AutomaticallyDefinedFunctions.factories;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors.operators.implementation
{
    public class ReproductiveOperator<T> : Operator<T> where T : IComparable
    {
        public ReproductiveOperator(int applicationPercentage) : base(applicationPercentage)
        {
        }

        protected override List<string> Operate(List<string> parents, IPopulationGenerator<T> generator)
        {
            //No op
            var chosenParent = RandomNumberFactory.Next(parents.Count);
            
            return new List<string>(){parents.ElementAt(chosenParent)};
        }
    }
}