using GeneticAlgorithmLib.source.core.population;

namespace GeneticAlgorithmLib.source.statistics
{
    public class MemberRecord<T>
    {
        private readonly IPopulationMember<T> _member;
        private readonly double _fitness;
        private bool _isDuplicate;
        private int _numOfDuplicates;

        public MemberRecord(IPopulationMember<T> member, double fitness)
        {
            _member = member;
            _fitness = fitness;

            _isDuplicate = false;
            _numOfDuplicates = 1;
        }

        public MemberRecord<T> AddDuplicate()
        {
            _isDuplicate = true;
            _numOfDuplicates++;

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

        public bool IsDuplicate => _isDuplicate;

        public int NumberOfDuplicates => _numOfDuplicates;
    }
}