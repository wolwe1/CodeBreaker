using System;
using AutomaticallyDefinedFunctions.source;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors
{
    public class ADFPopulationMember<T> : IPopulationMember<T> where T : IComparable
    {
        private readonly ADF<T> _adf;

        public ADFPopulationMember(ADF<T> adf)
        {
            _adf = adf;
        }

        public string GetId()
        {
            return _adf.GetId();
        }

        public T GetResult()
        {
            return _adf.GetValue();
        }
    }
}