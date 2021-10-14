using System;
using AutomaticallyDefinedFunctions.generators.adf;

namespace ADFMain
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new StateAdfSettings<string,int>(2, 3, 1, 65);

            var generator = new StateAdfGenerator<string,int>(1, settings);

            var adf = generator.Generate();

            var output = adf.GetValues();
            
            Console.WriteLine(string.Join("",output));
            
            adf.Update(output[0],0);
            
            output = adf.GetValues();
            
            Console.WriteLine(string.Join("",output));
        }
        
       
    }
}