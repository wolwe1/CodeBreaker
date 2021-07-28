using System.Collections.Generic;

namespace CodeBreakerLib.TestHandler
{
    public interface ITestStrategy
    {
        List<Test<object>> Setup();
        Test<object> AddTest();
    }
}