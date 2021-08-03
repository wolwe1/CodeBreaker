namespace GeneticAlgorithmLib.core.population
{
    public interface IPopulationGenerator
    {
        IPopulationMember GenerateNewMember();
    }
}