using System.Collections.Generic;

namespace CodeBreakerLib.resultsHandler
{
    public class ResultsHandler
    {
        public void CaptureCall(Test<object> currentTest, List<object> argumentsForTest)
        {
            currentTest.Run(argumentsForTest);
        }
    }
}