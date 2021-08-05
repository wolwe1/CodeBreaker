using GeneticAlgorithmLib.core.population;

namespace GeneticAlgorithmLib.statistics
{
    public class Result<T>
    {
        private readonly IPopulationMember<T> _member;
        private readonly double _fitness;
        private bool _isDuplicate;
        private int _numOfDuplicates;

        public Result(IPopulationMember<T> member, double fitness)
        {
            _member = member;
            _fitness = fitness;

            _isDuplicate = false;
            _numOfDuplicates = 0;
        }

        public Result<T> AddDuplicate()
        {
            _isDuplicate = false;
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
    }
}