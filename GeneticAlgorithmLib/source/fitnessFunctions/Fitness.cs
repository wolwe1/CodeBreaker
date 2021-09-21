using System.Collections.Generic;

namespace GeneticAlgorithmLib.source.fitnessFunctions
{
    public class Fitness
    {
        private List<FitnessEvaluations> _evaluations;

        public Fitness()
        {
            _evaluations = new List<FitnessEvaluations>();
        }
    }
}