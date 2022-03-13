using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class PrimeGenerator
    {
        public List<long> GetPrimesSequential(long first, long last)
        {
            List<long> primeList = new List<long>();
            for (long i = first; i <= last; i++)
            {
                if (IsPrime(i))
                    primeList.Add(i);
            }
            return primeList;
        }

        private bool IsPrime(long number)
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

        public List<long> GetPrimesParallel(long first, long last)
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
            return primeList;
        }

        public Task<List<long>> GetPrimesSequentialAsync(long first, long last)
        {
            return Task.Factory.StartNew(() =>
            {
                return GetPrimesSequential(first, last);
            });
        }

        public Task<List<long>> GetPrimesParallelAsync(long first, long last)
        {
            return Task.Factory.StartNew(() =>
            {
                return GetPrimesParallel(first, last);
            });
        }
    }
}
