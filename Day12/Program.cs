using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var graph = new Dictionary<int, List<int>>();

            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                var parts = line.Split(new string[] {" <-> "}, StringSplitOptions.None);
                var index = int.Parse(parts[0]);
                var neighbours = parts[1].Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(w => int.Parse(w)).ToList();

                graph.Add(index, neighbours);
            }

            var groups = new Dictionary<int, HashSet<int>>();
            var alreadyGrouped = new HashSet<int>();
            foreach (var key in graph.Keys)
            {
                var visited = new HashSet<int>();
                var toVisit = new List<int>();
                toVisit.Add(key);

                if (alreadyGrouped.Contains(key))
                    continue;

                while (true)
                {
                    var current = toVisit.FirstOrDefault();
                    if (toVisit.Count == 0)
                        break;

                    foreach (var neighbour in graph[current])
                    {
                        if (!visited.Contains(neighbour))
                        {
                            toVisit.Add(neighbour);
                        }
                    }

                    visited.Add(current);
                    toVisit.Remove(current);
                }

                foreach (var i in visited)
                {
                    if (!alreadyGrouped.Contains(i))
                        alreadyGrouped.Add(i);
                }
                
                groups.Add(key, visited);
            }

            Console.WriteLine(groups[0].Count);
            Console.WriteLine(groups.Count);
        }
    }
}