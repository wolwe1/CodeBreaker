using System;

namespace AutomaticallyDefinedFunctions.structure.adf.helpers
{
    public class Output<T>
    {
        public T Value { get; }
        public bool Failed { get; private set; }
        
        public Output(T value, bool failed)
        {
            Value = value;
            Failed = failed;
        }

        public Output(T value)
        {
            Value = value;
            Failed = false;
        }

        public Output()
        {
            Failed = true;
        }

        public void Fail()
        {
            Failed = true;
        }
    }
}