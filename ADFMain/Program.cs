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
            
            var funcCreator = NodeBuilder.CreateStateFunctionCreator<string,string>();

            var func = funcCreator.Choose<string>(3);

            var id = func.GetId();

            var funcCopy = funcCreator.GenerateFunctionFromId<string>(id);
            
            Console.WriteLine(id);
            Console.WriteLine(funcCopy.GetId().Equals(id));

        }
        
       
    }
}