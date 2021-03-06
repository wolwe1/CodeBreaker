using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomaticallyDefinedFunctions.structure.adf.helpers
{
    public class AdfValidator
    {
        public bool IsValid<T>(Adf<T> adf) where T : IComparable
        {
            return AreMainsValid(adf.GetMainPrograms()) && AreDefinitionsValid(adf.GetDefinitions());
        }

        private bool AreMainsValid<T>(IEnumerable<MainProgram<T>> mainPrograms) where T : IComparable
        {
            return mainPrograms.Any() && mainPrograms.Any(main => !main.IsValid());
        }
        
        private bool AreDefinitionsValid<T>(IEnumerable<FunctionDefinition<T>> definitions) where T : IComparable
        {
            return definitions.Any() && definitions.Any(func => !func.IsValid());
        }
    }
}