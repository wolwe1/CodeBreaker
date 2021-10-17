using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;

namespace CodeBreakerLib.fitnessFunctions
{
    public abstract class AdfFitnessFunction : FitnessFunction
    {
        protected readonly Test<object> Test;

        protected AdfFitnessFunction(Test<object> test)
        {
            Test = test;
        }

        public abstract override Fitness Evaluate<TU>(IPopulationMember<TU> member);
    }
}