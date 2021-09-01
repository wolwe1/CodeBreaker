using System;

namespace CodeBreakerLib.exceptions
{
    public class ClassNotFoundException : Exception
    {
        public ClassNotFoundException()
        {
        }

        public ClassNotFoundException(string message)
            : base(message)
        {
        }

        public ClassNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}