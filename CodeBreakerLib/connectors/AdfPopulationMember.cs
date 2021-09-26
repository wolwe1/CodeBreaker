using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.structure;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors
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

        public IEnumerable<T> GetResult()
        {
            return _adf.GetValues();
        }

        public Adf<T> GetAdf()
        {
            return _adf.GetCopy();
        }
    }
}