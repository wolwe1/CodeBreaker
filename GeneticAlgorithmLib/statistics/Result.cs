using GeneticAlgorithmLib.core.population;

namespace GeneticAlgorithmLib.statistics
{
    public class Result
    {
        private readonly IPopulationMember _member;
        private readonly double _fitness;
        private bool _isDuplicate;
        private int _numOfDuplicates;

        public Result(IPopulationMember member, double fitness)
        {
            _member = member;
            _fitness = fitness;

            _isDuplicate = false;
            _numOfDuplicates = 0;
        }

        public Result AddDuplicate()
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