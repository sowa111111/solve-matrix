using System;

namespace solve_matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            int allCombination = AlesiaBuyalskaya.SolveMatrix();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(allCombination);
            Console.WriteLine(elapsedMs);
            Console.ReadKey();
        }
    }
}
