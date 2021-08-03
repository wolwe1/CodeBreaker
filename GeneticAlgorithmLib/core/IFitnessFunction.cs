using GeneticAlgorithmLib.core.population;

namespace GeneticAlgorithmLib.core
{
    public interface IFitnessFunction
    {
        public double Evaluate(IPopulationMember member);
    }
}