using System;
using System.Threading;
using System.Threading.Tasks;
using TestObjects.source.simple.numeric;
using TestObjects.source.simple.strings;

namespace TestObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Palindrome();
            var f = new Fibonacci();
            
            //Console.WriteLine(p.Get("abc").GetReturnValue());
            //Console.WriteLine(f.GetIterative(46).GetReturnValue());

            // var j = new SayJohn();
            // Console.WriteLine(j.Get("john").GetReturnValue());

            
            //Console.WriteLine(f.Get(34).GetReturnValue());
            Console.WriteLine(f.GetIterative(1000000000).GetReturnValue());

        }
    }
}