using System;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;

namespace GeneticAlgorithmLib.source.statistics
{
    public class MemberRecord<T>
    {
        private readonly Fitness _fitness;
        private readonly IPopulationMember<T> _member;

        public MemberRecord(IPopulationMember<T> member, Fitness fitness)
        {
            _member = member;
            _fitness = fitness;

            IsDuplicate = false;
            NumberOfDuplicates = 1;
        }

        public bool IsDuplicate { get; private set; }

        public int NumberOfDuplicates { get; private set; }

        public MemberRecord<T> AddDuplicate()
        {
            IsDuplicate = true;
            NumberOfDuplicates++;

            return this;
        }

        public double GetFitnessValue()
        {
            return _fitness.GetFitness();
        }

        public string GetMemberId()
        {
            return _member.GetId();
        }

        public Fitness GetFitness()
        {
            return _fitness;
        }
    }
}