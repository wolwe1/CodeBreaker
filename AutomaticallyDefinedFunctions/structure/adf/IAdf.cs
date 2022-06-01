using System.Collections.Generic;
using AutomaticallyDefinedFunctions.structure.visitors;

namespace AutomaticallyDefinedFunctions.structure.adf
{
    public interface IAdf
    {
        bool IsValid();

        string GetId();

        IEnumerable<string> GetMainProgramIds();

        IEnumerable<string> GetFunctionIds();

        int GetMainNodeCount(int mainIndex);

        int GetNumberOfMainPrograms();

        int GetNumberOfDefinitions();
        
    }
}