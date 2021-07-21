using System;

namespace AutomaticallyDefinedFunctions.source
{
    public class ADF<T> where T : IComparable
    {
        private readonly MainProgram<T> _main;
        private readonly FunctionDefinition<T> _definition;

        public ADF(MainProgram<T> main, FunctionDefinition<T> definition)
        {
            _main = main;
            _definition = definition;
        }

        public T GetValue()
        {
            return _main.GetValue();
        }
    }
}