using System;
using System.Collections.Generic;
using GeneticAlgorithmLib.source.core.population;

namespace GeneticAlgorithmLib.source.mockImplementations
{
    public class RandomNumberMember : IPopulationMember<double>
    {
        private readonly double _value;

        public RandomNumberMember()
        {
            _value = new Random().Next(11);
        }

        public RandomNumberMember(int number)
        {
            _value = number;
        }

        public string GetId()
        {
            return _value.ToString();
        }

        public IEnumerable<double> GetResult()
        {
            return new List<double>(){_value};
        }
    }
}