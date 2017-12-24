using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Day20
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var particles = new List<Particle>();

            for (int i = 0; i < lines.Length; i++)
            {
                string parsedLine = lines[i];
                parsedLine = parsedLine.Replace("p=<", "");
                parsedLine = parsedLine.Replace("v=<", "");
                parsedLine = parsedLine.Replace("a=<", "");
                parsedLine = parsedLine.Replace(">", "");
                //Console.WriteLine(parsedLine);

                var entries = parsedLine.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(w => long.Parse(w)).ToList();
                //p=<-3770,-455,1749>, v=<-4,-77,53>, a=<11,7,-9>

                var particle = new Particle(i,
                    new Tuple<long, long, long>(entries[0], entries[1], entries[2]),
                    new Tuple<long, long, long>(entries[3], entries[4], entries[5]),
                    new Tuple<long, long, long>(entries[6], entries[7], entries[8]));

                particles.Add(particle);
            }

            for (int i = 0; i < 5000; i++)
            {
                foreach (var particle in particles)
                {
                    particle.GetNextDistance();
                }


                var notDistinct = particles.NonDistinct(w => w.CurrentP).ToList();
                foreach (var position in notDistinct)
                {
                    position.Deleted = true;
                }
            }

            var minValue = particles.Select(w => w.Distances.Average()).Min();
            var results = particles.Where(w => w.Distances.Average() == minValue).ToList();
            foreach (var particle in results)
            {
                Console.WriteLine(particle.Number);
            }

            var notDeletedParticles = particles.Where(w => w.Deleted == false).Count();
            Console.WriteLine(notDeletedParticles);
        }
    }

    public static class LinqExtensions
    {
        public static IEnumerable<T> NonDistinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            return source.GroupBy(keySelector).Where(g => g.Count() > 1).SelectMany(r => r);
        }
    }

    public class Particle
    {
        public bool Deleted { get; set; }

        public List<long> Distances { get; set; }
        public long MinValue { get; set; }
        public int Number { get; set; }

        public Tuple<long, long, long> CurrentP { get; set; }
        public Tuple<long, long, long> CurrentV { get; set; }
        public Tuple<long, long, long> CurrentA { get; set; }

        public Particle(int number, Tuple<long, long, long> p, Tuple<long, long, long> v, Tuple<long, long, long> a)
        {
            Distances = new List<long>();
            Number = number;
            CurrentP = p;
            CurrentV = v;
            CurrentA = a;

            MinValue = long.MaxValue;
        }

        public long GetNextDistance()
        {
            var currentVX = CurrentV.Item1 + CurrentA.Item1;
            var currentVY = CurrentV.Item2 + CurrentA.Item2;
            var currentVZ = CurrentV.Item3 + CurrentA.Item3;

            CurrentV = new Tuple<long, long, long>(currentVX, currentVY, currentVZ);

            var currentPX = CurrentP.Item1 + CurrentV.Item1;
            var currentPY = CurrentP.Item2 + CurrentV.Item2;
            var currentPZ = CurrentP.Item3 + CurrentV.Item3;

            CurrentP = new Tuple<long, long, long>(currentPX, currentPY, currentPZ);

            var result = Math.Abs(CurrentP.Item1) + Math.Abs(CurrentP.Item2) + Math.Abs(CurrentP.Item3);

            if (result < MinValue)
            {
                MinValue = result;
            }

            Distances.Add(result);
            return result;
        }
    }
}