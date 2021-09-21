using GeneticAlgorithmLib.source.core.population;

namespace GeneticAlgorithmLib.source.statistics
{
    public class MemberRecord<T>
    {
        private readonly double _fitness;
        private readonly IPopulationMember<T> _member;

        public MemberRecord(IPopulationMember<T> member, double fitness)
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

        public double GetFitness()
        {
            return _fitness;
        }

        public string GetMemberId()
        {
            return _member.GetId();
        }
    }
}