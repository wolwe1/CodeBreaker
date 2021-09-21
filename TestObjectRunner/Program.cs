using System;
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
            
            
            Console.WriteLine(p.Get("abc").GetReturnValue());
            Console.WriteLine(f.Get(6).GetReturnValue());
            
        }
    }
}