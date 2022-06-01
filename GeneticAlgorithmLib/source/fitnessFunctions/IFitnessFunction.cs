using System;
using System.Collections.Generic;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.statistics;
using GeneticAlgorithmLib.source.statistics.calculatedResults;

namespace GeneticAlgorithmLib.source.fitnessFunctions
{
    public interface IFitnessFunction
    {
        public Fitness Evaluate<T>(IPopulationMember<T> member) where T : IComparable;
        CalculationResultSet GetNormalisedFitnessValues<T>(GenerationRecord<T> results);

        MemberRecord<T> GetBest<T>(GenerationRecord<T> candidates);
        MemberRecord<T> GetBest<T>(IEnumerable<MemberRecord<T>> chosenMembers);
    }
}