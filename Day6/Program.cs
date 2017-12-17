using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Day6
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //var input = new List<int> {0, 2, 7, 0};
            var input = new List<int> {2, 8, 8, 5, 4, 2, 3, 1, 5, 5, 1, 2, 15, 13, 5, 14};
            
            var previousState = GetAnswer(input);
            GetAnswer(previousState);
        }

        private static List<int> GetAnswer(List<int> input)
        {
            var states = new HashSet<List<int>>(new SimpleJsonComparer());
            states.Add(input);
            var inputsLenght = input.Count;
            int count = 0;
            while (true)
            {
                var max = input.Max();
                var maxIndex = input.IndexOf(max);
                int currentIndex = maxIndex + 1;

                input[maxIndex] = 0;
                while (true)
                {
                    if (max == 0)
                        break;

                    if (currentIndex == inputsLenght)
                        currentIndex = 0;

                    input[currentIndex]++;
                    currentIndex++;
                    max--;
                }

                count++;

                if (states.Contains(input))
                {
                    Console.WriteLine(count);
                    return input;
                }

                states.Add(input);
            }
        }
    }

    class SimpleJsonComparer : EqualityComparer<List<int>>
    {
        public override bool Equals(List<int> x, List<int> y)
        {
            return JsonConvert.SerializeObject(x) == JsonConvert.SerializeObject(y);
        }

        public override int GetHashCode(List<int> obj)
        {
            return JsonConvert.SerializeObject(obj).GetHashCode();
        }
    }
}