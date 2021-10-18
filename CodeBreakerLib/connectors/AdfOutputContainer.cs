using System;
using AutomaticallyDefinedFunctions.structure.adf.helpers;
using GeneticAlgorithmLib.source.core.population;

namespace CodeBreakerLib.connectors
{
    public class AdfOutputContainer<T>  : AdfOutput<T>, IOutputContainer<T>
    {
        public AdfOutputContainer(AdfOutput<T> adfOutput) : base(adfOutput.GetOutputs()) { }

        public string GetOutputString()
        {
            throw new NotImplementedException();
        }
    }
}