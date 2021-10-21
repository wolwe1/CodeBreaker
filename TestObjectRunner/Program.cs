using System;
using TestObjects.source.simple.numeric;

namespace TestObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            Mandelbrot mand = new Mandelbrot();
            
            Console.WriteLine($"Is in set? {mand.Get(1,18)}");

        }
    }
}