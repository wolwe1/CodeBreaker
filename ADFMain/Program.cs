using System;
using AutomaticallyDefinedFunctions.factories;
using AutomaticallyDefinedFunctions.factories.functionFactories.operators;

namespace ADFMain
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomNumberFactory.SetSeed(0);
            
            var funcCreator = NodeBuilder.CreateFunctionCreator();

            var func = funcCreator.Choose<string>(3);

        }
        
       
    }
}