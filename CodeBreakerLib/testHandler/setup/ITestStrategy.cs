using System.Collections.Generic;

namespace CodeBreakerLib.testHandler.setup
{
    public interface ITestStrategy
    {
        List<Test<object>> LoadTests();
        Test<object> AddTest();
    }
}