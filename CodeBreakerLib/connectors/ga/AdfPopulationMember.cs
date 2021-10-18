using System;
using AutomaticallyDefinedFunctions.structure.adf;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors.ga
{
    public class AdfPopulationMember<T> : IPopulationMember<T> where T : IComparable
    {
        protected readonly Adf<T> Adf;

        public AdfPopulationMember(Adf<T> adf)
        {
            Adf = adf;
        }

        public string GetId()
        {
            return Adf.GetId();
        }

        public virtual IOutputContainer<T> GetResult()
        {
            return new AdfOutputContainer<T>(Adf.GetValues());
        }

        public IPopulationMember<T> GetCopy()
        {
            return new AdfPopulationMember<T>(Adf.GetCopy());
        }

        public Adf<T> GetAdf(bool copy = true)
        {
            return copy ? Adf.GetCopy() : Adf;
        }
    }
}