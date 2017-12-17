using System;
using System.Collections.Generic;
using System.IO;
using Day3;

namespace Day11
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");

            var point = new Point(0, 0);

            var instructions = input.Split(',');

            int maxDistance = int.MinValue;

            foreach (var instruction in instructions)
            {
                if (instruction == "n")
                {
                    var x = point.X;
                    var y = point.Y + 2;

                    point = new Point(x, y);
                }

                if (instruction == "s")
                {
                    var x = point.X;
                    var y = point.Y - 2;

                    point = new Point(x, y);
                }

                if (instruction == "nw")
                {
                    var x = point.X - 1;
                    var y = point.Y + 1;

                    point = new Point(x, y);
                }

                if (instruction == "sw")
                {
                    var x = point.X - 1;
                    var y = point.Y - 1;

                    point = new Point(x, y);
                }

                if (instruction == "ne")
                {
                    var x = point.X + 1;
                    var y = point.Y + 1;

                    point = new Point(x, y);
                }

                if (instruction == "se")
                {
                    var x = point.X + 1;
                    var y = point.Y - 1;

                    point = new Point(x, y);
                }

                var currentDistance = GetDistance(point);
                if (maxDistance < currentDistance)
                    maxDistance = currentDistance;
            }

            var answer1 = GetDistance(point);
            Console.WriteLine(answer1);
            Console.WriteLine(maxDistance);
        }

        private static int GetDistance(Point point)
        {
            return (int) (Math.Abs(point.X) + Math.Abs(point.Y)) / 2;
        }
    }
}