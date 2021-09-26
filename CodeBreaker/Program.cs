using CodeBreakerLib.testHandler;
using CodeBreakerLib.testHandler.integration;
using CodeBreakerLib.testHandler.setup;

namespace CodeBreaker
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestString();
            
            var testHandler = new TestHandler(new TestFileDescriptorStrategy(),new GeneticAlgorithmBuilder());
            testHandler.Setup();
            testHandler.RunAllTests();
        }
    }
}