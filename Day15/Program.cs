using System;
using System.Collections.Generic;
using System.Threading;

namespace Day15
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //long valueA = 65;
            //long valueB = 8921;

            long valueA = 516;
            long valueB = 190;
            
            GetAnswer1(valueA, valueB);
            GetAnswer2(valueA, valueB);
        }

        private static void GetAnswer2(long valueA, long valueB)
        {
            int requiredPairs = 5000000;
            long factorA = 16807;
            long factorB = 48271;
            long divider = 2147483647;

            var aResults = new List<long>();
            var bResults = new List<long>();

            var threadA = new Thread(() =>
            {
                int counter = 0;
                while (true)
                {
                    if (counter == requiredPairs)
                        break;

                    long result = (factorA * valueA) % divider;

                    if (result % 4 == 0)
                    {
                        aResults.Add(result);
                        counter++;
                    }

                    valueA = result;
                }
            });
            threadA.Start();

            var threadB = new Thread(() =>
            {
                int counter = 0;
                while (true)
                {
                    if (counter == requiredPairs)
                        break;

                    long result = (factorB * valueB) % divider;

                    if (result % 8 == 0)
                    {
                        bResults.Add(result);
                        counter++;
                    }

                    valueB = result;
                }
            });
            threadB.Start();

            threadA.Join();
            threadB.Join();

            int finalResult = 0;
            for (int i = 0; i < requiredPairs; i++)
            {
                var resultA = aResults[i];
                var resultB = bResults[i];

                if (CompareBits(resultA, resultB))
                    finalResult++;

                valueA = resultA;
                valueB = resultB;
            }

            Console.WriteLine(finalResult);
        }

        private static void GetAnswer1(long valueA, long valueB)
        {
            long factorA = 16807;
            long factorB = 48271;
            long divider = 2147483647;

            int counter = 0;

            for (int i = 0; i < 40000000; i++)
            {
                long resultA = (factorA * valueA) % divider;
                long resultB = (factorB * valueB) % divider;

                if (CompareBits(resultA, resultB))
                    counter++;

                valueA = resultA;
                valueB = resultB;
            }

            Console.WriteLine(counter);
        }

        private static bool CompareBits(long resultA, long resultB)
        {
            for (int i = 0; i < 16; i++)
            {
                var bitA = (resultA >> i) & 1;
                var bitB = (resultB >> i) & 1;

                if (bitA != bitB)
                    return false;
            }

            return true;
        }
    }
}