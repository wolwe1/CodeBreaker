namespace GeneticAlgorithmLib.core.population
{
    public interface IPopulationGenerator<T>
    {
        IPopulationMember<T> GenerateNewMember();
    }
}