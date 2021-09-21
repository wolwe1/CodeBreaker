using System;

namespace GeneticAlgorithmLib.source.fitnessFunctions
{
    public class FitnessEvaluations
    {
        private Type _evaluator;
        private double _fitness;

        public FitnessEvaluations(Type evaluator, double fitness)
        {
            _evaluator = evaluator;
            _fitness = fitness;
        }
    }
}