using System;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.statistics;
using GeneticAlgorithmLib.source.statistics.calculatedResults;

namespace GeneticAlgorithmLib.source.fitnessFunctions
{
    public abstract class FitnessFunction : IFitnessFunction
    {
        protected bool IsMaximising;

        protected FitnessFunction()
        {
            IsMaximising = true;
        }

        public abstract double Evaluate<T>(IPopulationMember<T> member) where T : IComparable;

        public CalculationResultSet GetNormalisedFitnessValues<T>(GenerationRecord<T> results)
        {
            var fitnessValues = results.GetFitnessValues();
            var totalFitness = results.GetTotalFitness();

            return fitnessValues.Map(x => x / totalFitness);
        }

        public FitnessFunction UseMinimisation()
        {
            throw new NotImplementedException();
            IsMaximising = false;

            return this;
        }

        public FitnessFunction UseMaximisation()
        {
            IsMaximising = true;

            return this;
        }
    }
}