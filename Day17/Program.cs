using System;
using System.Collections.Generic;
using Day10;

namespace Day17
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var input = 359;
            var result1 = GetAnswer1(input);
            Console.WriteLine(result1);

            var result = GetAnswer2(input);
            Console.WriteLine(result);
        }

        private static int GetAnswer2(int input)
        {
            int result = 0;
            int value = 1;
            int currentPositionIndex = 0;
            for (int i = 0; i < 50000000; i++)
            {
                currentPositionIndex = (currentPositionIndex + input) % (i + 1) + 1;
                if (currentPositionIndex == 1)
                {
                    result = value;
                }
                value++;
            }
            return result;
        }

        private static int GetAnswer1(int input)
        {
            var maxValue = 2017;
            var lookupValue = 2017;

            int value = 1;
            var spinLock = new List<int>();

            int currentPositionIndex = 0;
            spinLock.Add(0);

            for (int i = 0; i < maxValue; i++)
            {
                currentPositionIndex = (currentPositionIndex + input) % (i + 1) + 1;
                spinLock.Insert(currentPositionIndex, value);
                value++;
            }

            var indexOf = spinLock.IndexOf(lookupValue);
            return spinLock[indexOf + 1];
        }
    }
}