using GeneticAlgorithmLib.source.core.population;

namespace GeneticAlgorithmLib.source.mockImplementations
{
    public class RandomNumberGenerator : IPopulationGenerator<double>
    {
        public IPopulationMember<double> GenerateNewMember()
        {
            return new RandomNumberMember();
        }

        public IPopulationMember<double> GenerateFromId(string chromosome)
        {
            var number = int.Parse(chromosome);

            return new RandomNumberMember(number);
        }
    }
}