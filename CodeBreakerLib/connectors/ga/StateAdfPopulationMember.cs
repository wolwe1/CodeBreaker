using System;
using AutomaticallyDefinedFunctions.structure.adf;
using AutomaticallyDefinedFunctions.structure.state;
using CodeBreakerLib.connectors.ga.state;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors.ga
{
    public class StateAdfPopulationMember<T,TU> : AdfPopulationMember<T> where T : IComparable where TU : IComparable
    {
        public StateAdfPopulationMember(Adf<T> adf) : base(adf) { }
        
        public override IOutputContainer<T> GetResult()
        {
            var stateAdf = (StateBasedAdf<T,TU>) Adf;
            return new StateAdfOutputContainer<T,TU>(stateAdf.GetHistory());
        }
        
        public override IPopulationMember<T> GetCopy()
        {
            return new StateAdfPopulationMember<T,TU>(Adf.GetCopy());
        }
    }
    
    
}