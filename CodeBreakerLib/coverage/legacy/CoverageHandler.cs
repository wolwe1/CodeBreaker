using System;
using System.Diagnostics;

namespace CodeBreakerLib.coverage.legacy
{
    public class CoverageHandler
    {
        private const string Dotcover = @"C:\Users\jarro\AppData\Local\JetBrains\Installations\dotCoverCommandLine\dotCover.exe";

        private const string TEST = "cover-dotnet --Output=\"D:\\Honours\\second year\\COS700\\Research project\\code\\Implementation\\CodeBreaker\\CodeBreaker\\coverOutput.html\" --reportType=HTML";
        //private readonly string dotcover = @"C:\Users\jarro\.dotnet\tools\dotnet-dotcover.exe";
        private const string BeginCoverageArguments = "cover --TargetExecutable=\"D:\\Honours\\second year\\COS700\\Research project\\code\\test objects\\TestObjects\\TestObjectRunner\\bin\\Debug\\net5.0\\TestObjectRunner.exe\"  --reportType=HTML --Instance=1 --Output=\"D:\\Honours\\second year\\COS700\\Research project\\code\\Implementation\\CodeBreaker\\CodeBreaker\\coverOutput";

        private const string StartCoverArguments = @"send --Instance=1 --Command=Cover";
        private const string EndCoverArguments = @"send --Instance=1 --Command=GetSnapshotAndKillChildren";

        public void Test()
        {
            var process = createProcess(Dotcover, TEST);

            Console.WriteLine("TEST");
            RunProcess(process);
            Console.WriteLine("END TEST");
        }
        public void Begin()
        {
            var date = DateTime.Now.Second;
            var fileSuffix = $"{date}.html\"";
            
            var process = createProcess(Dotcover, BeginCoverageArguments + fileSuffix);

            Console.WriteLine("BEGIN");
            RunProcess(process);
            Console.WriteLine("END BEGIN");

        }
        
        public void Start()
        {
            var process = createProcess(Dotcover, StartCoverArguments);

            Console.WriteLine("START");
            RunProcess(process);
            Console.WriteLine("END START");
        }
        
        public void Collect()
        {
            var process = createProcess(Dotcover, EndCoverArguments);

            Console.WriteLine("COLLECT");
            RunProcess(process);
            Console.WriteLine("END COLLECT");
        }

        private Process createProcess(string fileName, string arguments)
        {
            return new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = fileName,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
        }

        private void RunProcess(Process process)
        {
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                Console.WriteLine(line);
            }
        }
        
    }
}