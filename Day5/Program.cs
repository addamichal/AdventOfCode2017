using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var answer1 = GetAnswer(lines, (i) => i + 1);
            var answer2 = GetAnswer(lines, (i) => i >= 3 ? i - 1 : i + 1);
            
            Console.WriteLine(answer1);
            Console.WriteLine(answer2);
        }

        private static int GetAnswer(string[] lines, Func<int, int> GetValue)
        {
            int currentPosition = 0;
            var array = lines.Select(w => int.Parse(w)).ToArray();
            var arrayLenght = array.Length;

            int steps = 0;
            while (true)
            {
                int nextStep = array[currentPosition];

                array[currentPosition] = GetValue(array[currentPosition]);               
                currentPosition += nextStep;
                steps++;

                if (currentPosition >= arrayLenght)
                {
                    break;
                }
            }
            return steps;
        }
    }
}