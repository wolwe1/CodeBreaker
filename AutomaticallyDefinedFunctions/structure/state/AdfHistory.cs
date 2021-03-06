using System;
using System.Collections.Generic;
using AutomaticallyDefinedFunctions.structure.adf.helpers;

namespace AutomaticallyDefinedFunctions.structure.state
{
    public class AdfHistory<T,TU>
    {
        public Dictionary<AdfOutput<T>,TU> OutputsWithResponse { get; }

        public AdfHistory()
        {
            OutputsWithResponse = new Dictionary<AdfOutput<T>, TU>();
        }

        public void AddHistory(AdfOutput<T> output, TU programResponse)
        {
            OutputsWithResponse.Add(output,programResponse);
        }

        public void AddHistory(AdfOutput<T> output)
        {
            OutputsWithResponse.Add(output,default);
        }
    }
}