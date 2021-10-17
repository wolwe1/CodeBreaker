using System.Collections.Generic;

namespace GeneticAlgorithmLib.source.core.population
{
    public interface IOutputContainer<out T>
    {
        IEnumerable<T> GetOutputValues();
    }
}