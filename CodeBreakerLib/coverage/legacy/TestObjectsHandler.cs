using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CodeBreakerLib.coverage.http;

namespace CodeBreakerLib.coverage.legacy
{
    public class TestObjectsHandler
    {
        private readonly string webserver = "D:\\Honours\\second year\\COS700\\Research project\\code\\test objects\\TestObjects\\TestObjectsWeb\\bin\\Debug\\net5.0\\TestObjectsWeb.exe";
        private readonly string url = "http://localhost:5000/String";
        
        private static readonly HttpHelper Client = new();
        private Process webProcess;

        ~TestObjectsHandler()
        {
            webProcess.Close();
        }
        public void Begin()
        {
            webProcess = createProcess(webserver);

            webProcess.Start();
        }

        public async Task<object> Test()
        {
            var requestObj = new
            {
                MethodToCall = "anagram",
                Arguments = new List<string>() {"banana", "anan"}
            };
            
            return await Client.sendPost(url,requestObj);

        }
        private Process createProcess(string fileName)
        {
            return new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = fileName,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
        }

        private void CaptureProcessOutput(Process process)
        {
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                Console.WriteLine(line);
            }
            Console.WriteLine("Process end of stream");
        }
        
    }
}