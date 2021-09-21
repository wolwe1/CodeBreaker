using System;
using System.Threading.Tasks;
using Flurl.Http;

namespace CodeBreakerLib.coverage.http
{
    public class HttpHelper
    {
        public async Task<object> sendPost(string url, object body)
        {
            try
            {
                return await url
                    .PostJsonAsync(body)
                    .ReceiveJson<object>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
    }
}