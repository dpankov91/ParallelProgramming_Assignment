using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ppAssignmentApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sequential implementation");
            MeasureTime(() => GetPrimesSequential(1, 1000000));

            Console.WriteLine("Parallel implementation");
            MeasureTime(() => GetPrimesParallel(1, 1000000));

        }

        private static void MeasureTime(Action ac)
        {
            Stopwatch sw = Stopwatch.StartNew();
            ac.Invoke();
            sw.Stop();
            Console.WriteLine("Time = " + sw.Elapsed.Milliseconds);
        }

        public static void GetPrimesSequential(int first, int last)
        {

                List<long> primeList = new List<long>();
                for (long i = first; i <= last; i++)
                {
                        if(IsPrime(i))
                            primeList.Add(i);
                }
        }

        private static bool IsPrime(long number)
        {
            if (number == 1)
            {
                return false;
            }

            if (number % 2 == 0 && number != 2)
            {
                return false;
            }

            for (double divisor = 2; divisor <= Math.Sqrt(number); divisor++)
            {
                if (number % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static void GetPrimesParallel(long first, long last)
        {
                List<long> primeList = new List<long>();
                object lockObject = new List<long>();
                Parallel.ForEach(Partitioner.Create(first, last + 1), range =>
                {
                    var localPrimeList = new List<long>();
                    for (long i = range.Item1; i < range.Item2; i++)
                    {
                        if (IsPrime(i))
                            localPrimeList.Add(i);
                    }
                    lock (lockObject)
                    {
                        primeList.AddRange(localPrimeList);
                    }
            });
        }
    }
}
