namespace GeneticAlgorithmLib.source.core.population
{
    public interface IPopulationGenerator<T>
    {
        IPopulationMember<T> GenerateNewMember();
        IPopulationMember<T> GenerateFromId(string chromosome);
    }
}