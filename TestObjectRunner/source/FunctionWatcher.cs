using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TestObjects.source.capture;

namespace TestObjects.source
{
    public class FunctionWatcher
    {
        public static async Task<CoverageResults<T>> Execute<T>(Func<T, CancellationToken, CoverageResults<T>> func, T i)
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(1000);
            var token = tokenSource.Token;

            var res = Task.Run(() => func(i,token),token);
            
            try
            {
                return await res;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            finally
            {
                tokenSource.Dispose();
            }
        }
    }
}