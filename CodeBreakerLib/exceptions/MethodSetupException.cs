using System;

namespace CodeBreakerLib.exceptions
{
    public class MethodSetupException : Exception
    {
        public MethodSetupException(string message) : base(message){}
    }
}