using System;

namespace AutomaticallyDefinedFunctions.exceptions
{
    public class InvalidStructureException : Exception
    {
        public InvalidStructureException(string message) : base(message){}
    }
}