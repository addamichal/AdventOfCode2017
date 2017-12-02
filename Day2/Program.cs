using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var arr = lines
                .Select(line => 
                    line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                    .Select(int.Parse).ToList()
                );

            var checksum = 0;
            foreach (var line in arr)
            {
                var max = line.Max();
                var min = line.Min();
                checksum += max - min;
            }
            
            Console.WriteLine(checksum);
            
            var checksum2 = 0;
            foreach (var line in arr)
            {
                for (int i = 0; i < line.Count; i++)
                {
                    for (int j = 0; j < line.Count; j++)
                    {
                        if (i != j && line[i] % line[j] == 0)
                        {
                            checksum2 += line[i] / line[j];
                        }
                    }
                }
            }
            
            Console.WriteLine(checksum2);
        }
    }
}