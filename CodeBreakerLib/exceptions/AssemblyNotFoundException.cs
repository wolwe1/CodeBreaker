using System;

namespace CodeBreakerLib.exceptions
{
    public class AssemblyNotFoundException : Exception
    {
        public AssemblyNotFoundException(string assemblyName): base("Could not load assembly: " + assemblyName){}
    }
}