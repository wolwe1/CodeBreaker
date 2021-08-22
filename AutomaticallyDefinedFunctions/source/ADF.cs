using System;

namespace AutomaticallyDefinedFunctions.source
{
    public class ADF<T> where T : IComparable
    {
        private readonly MainProgram<T> _main;
        private readonly FunctionDefinition<T> _definition;

        public ADF()
        {
            
        }
        public ADF(MainProgram<T> main, FunctionDefinition<T> definition)
        {
            _main = main;
            _definition = definition;
        }

        public T GetValue()
        {
            return _main.GetValue();
        }

        public ADF<T> UseDefinition(FunctionDefinition<T> definition)
        {
            return new ADF<T>(_main,definition);
        }
        
        public ADF<T> UseMain(MainProgram<T> main)
        {
            return new ADF<T>(main,_definition);
        }

        public bool IsValid()
        {
            return _main != null && _main.IsValid() && _definition != null && _definition.IsValid();
        }
    }
}