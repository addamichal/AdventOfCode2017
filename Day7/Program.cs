using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day7
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var root = GetAnswer1(lines);
            Console.WriteLine(root);

            var answer2 = GetAnswer2(lines, root);
            Console.WriteLine(answer2);
        }

        private static int GetAnswer2(string[] lines, string root)
        {
            var values = new Dictionary<string, int>();
            var neighbours = new Dictionary<string, List<string>>();

            foreach (var line in lines)
            {
                var parts = line.Split(new string[] {" -> "}, StringSplitOptions.RemoveEmptyEntries);

                var part0Match = Regex.Match(parts[0], @"([a-zA-Z]+) \((\d*)\)");
                var program = part0Match.Groups[1].Value;

                values.Add(program, int.Parse(part0Match.Groups[2].Value));

                if (parts.Length == 2)
                {
                    var matches = Regex.Matches(parts[1], @"[a-zA-Z]+");
                    var lst = (from Match match in matches select match.Value).ToList();

                    neighbours.Add(program, lst);
                }
                else
                {
                    neighbours.Add(program, new List<string>());
                }
            }


            //we will go deeper until we find first deepest unbalanced value
            int result = 0;
            string startNode = root;
            while (true)
            {
                var currentNeighbours = new Dictionary<string, int>();

                foreach (var neighbour in neighbours[startNode])
                {
                    var value = GetValue(neighbour, neighbours, values);
                    currentNeighbours.Add(neighbour, value);
                }

                var currentNeighbourValues = currentNeighbours.Values.ToList();
                if (currentNeighbourValues.Distinct().Count() == 1)
                    break;

                var valueOccuringOnlyOnce = GetValueOccuringOnlyOnce(currentNeighbourValues);
                var valueOccuringMultipleTimes = GetValueOccuringMultipleTimes(currentNeighbours.Values.ToList());
                var unbalancedNeighbour = currentNeighbours.SingleOrDefault(w => w.Value == valueOccuringOnlyOnce).Key;
                startNode = unbalancedNeighbour;

                result = values[startNode] - Math.Abs(valueOccuringOnlyOnce - valueOccuringMultipleTimes);
            }

            return result;
        }

        private static int GetValueOccuringMultipleTimes(List<int> values)
        {
            return values.Distinct().SingleOrDefault(w => values.Count(x => x == w) != 1);
        }

        public static int GetValueOccuringOnlyOnce(List<int> values)
        {
            return values.Distinct().SingleOrDefault(w => values.Count(x => x == w) == 1);
        }

        public static int GetValue(string node, Dictionary<string, List<string>> neighbours,
            Dictionary<string, int> values)
        {
            List<string> currentNeighbours = neighbours[node];
            if (currentNeighbours.Count == 0) return values[node];

            int value = values[node];
            foreach (var currentNeighbour in currentNeighbours)
            {
                value += GetValue(currentNeighbour, neighbours, values);
            }
            return value;
        }

        private static string GetAnswer1(string[] lines)
        {
            var histogram = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                var matches = Regex.Matches(line, @"[a-zA-Z]+");
                foreach (Match match in matches)
                {
                    var matchValue = match.Value;
                    if (histogram.ContainsKey(matchValue))
                    {
                        histogram[matchValue]++;
                    }
                    else
                    {
                        histogram.Add(matchValue, 1);
                    }
                }
            }

            return histogram.SingleOrDefault(w => w.Value == 1).Key;
        }
    }
}