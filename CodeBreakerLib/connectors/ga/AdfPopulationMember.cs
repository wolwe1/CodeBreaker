using System;
using AutomaticallyDefinedFunctions.structure.adf;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors.ga
{
    public class AdfPopulationMember<T> : IPopulationMember<T> where T : IComparable
    {
        private readonly Adf<T> _adf;

        public AdfPopulationMember(Adf<T> adf)
        {
            _adf = adf;
        }

        public string GetId()
        {
            return _adf.GetId();
        }

        public IOutputContainer<T> GetResult()
        {
            return new AdfOutputContainer<T>(_adf.GetValues());
        }

        public Adf<T> GetAdf(bool copy = true)
        {
            return copy ? _adf.GetCopy() : _adf;
        }
    }
}