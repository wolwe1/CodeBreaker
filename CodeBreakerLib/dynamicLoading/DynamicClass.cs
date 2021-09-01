using System;
using System.Collections.Generic;
using System.Reflection;

namespace CodeBreakerLib.dynamicLoading
{
    public class DynamicClass
    {
        private readonly Type _type;
        private readonly object _classObject;

        public DynamicClass(Type t)
        {
            _type = t;
            _classObject = Activator.CreateInstance(_type);
        }
        public Type GetTypeOf()
        {
            return _type;
        }

        public object GetObject()
        {
            return _classObject;
        }

        public void Print()
        {
            Console.WriteLine($"Type:{_type} , Object:{_classObject}");
        }
    }
}