namespace GeneticAlgorithmLib.source.builder
{
    public class GeneticAlgorithmBuilder
    {
        public IControlModelPhaseBuilder UseSteadyStateControlModel()
        {
            return new SteadyStateControlModelPhaseBuilder();
        }
    }
}