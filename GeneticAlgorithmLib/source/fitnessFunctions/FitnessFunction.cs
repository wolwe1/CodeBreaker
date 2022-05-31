using System;
using System.Collections.Generic;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.statistics;
using GeneticAlgorithmLib.source.statistics.calculatedResults;

namespace GeneticAlgorithmLib.source.fitnessFunctions
{
    public abstract class FitnessFunction : IFitnessFunction
    {
        public abstract Fitness Evaluate<T>(IPopulationMember<T> member) where T : IComparable;

        public CalculationResultSet GetNormalisedFitnessValues<T>(GenerationRecord<T> results)
        {
            var fitnessValues = results.GetFitnessValues();
            var totalFitness = results.GetTotalFitness();

            return fitnessValues.Map(x => x / totalFitness);
        }

        public MemberRecord<T> GetBest<T>(GenerationRecord<T> record)
        {
            return GetBest(record.GetMemberRecords());
        }
        public abstract MemberRecord<T> GetBest<T>(IEnumerable<MemberRecord<T>> chosenMembers);
    }
}