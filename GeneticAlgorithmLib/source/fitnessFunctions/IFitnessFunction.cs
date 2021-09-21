using System;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.statistics;
using GeneticAlgorithmLib.source.statistics.calculatedResults;

namespace GeneticAlgorithmLib.source.fitnessFunctions
{
    public interface IFitnessFunction
    {
        public double Evaluate<T>(IPopulationMember<T> member) where T : IComparable;
        CalculationResultSet GetNormalisedFitnessValues<T>(GenerationRecord<T> results);
    }
}