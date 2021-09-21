using AutomaticallyDefinedFunctions.exceptions;
using GeneticAlgorithmLib.source.core.population;
using GeneticAlgorithmLib.source.fitnessFunctions;

namespace CodeBreakerLib.fitnessFunctions
{
    public class StringLengthFitnessFunction : FitnessFunction
    {
        public override Fitness Evaluate<T>(IPopulationMember<T> member)
        {
            try
            {
                var value = member.GetResult() as string;

                var bonus = value != null && value.Contains(" ") ? 3 : 0;

                return new Fitness(typeof(StringLengthFitnessFunction),value?.Length + bonus ?? 0);
            }
            catch (ProgramLoopException e)
            {
                return new Fitness(typeof(StringLengthFitnessFunction),0);
            }
            
        }
    }
}