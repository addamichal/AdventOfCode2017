using System;
using System.Collections.Generic;
using System.Linq;
using Day10;
using Day3;

namespace Day14
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var input = "oundnydw-";

            var grid = new int[128, 128];

            for (int i = 0; i < 128; i++)
            {
                var knotHash = Utils.GetKnotHash(input + i);

                string binarystring = String.Join(String.Empty,
                    knotHash.Select(
                        c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                    )
                );

                for (int j = 0; j < 128; j++)
                {
                    grid[i, j] = binarystring[j] == '1' ? 1 : '0';
                }
            }

            int result = 0;
            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 128; j++)
                {
                    if (grid[i, j] == 1)
                        result++;
                }
            }

            var sets = new HashSet<Point>();
            var alreadyInSet = new HashSet<Point>();
            for (int x = 0; x < 128; x++)
            {
                for (int y = 0; y < 128; y++)
                {
                    var point = new Point(x, y);
                    if (!alreadyInSet.Contains(point) && IsSquare(point, grid))
                    {
                        sets.Add(point);
                        Visit(point, grid, alreadyInSet);
                    }
                }
            }

            Console.WriteLine(result);
            Console.WriteLine(sets.Count);
        }

        private static void Visit(Point point, int[,] grid, HashSet<Point> alreadyInSet)
        {
            alreadyInSet.Add(point);

            var upPoint = new Point(point.X, point.Y + 1);
            if (IsValid(upPoint) && IsSquare(upPoint, grid) && !alreadyInSet.Contains(upPoint))
            {
                Visit(upPoint, grid, alreadyInSet);
            }

            var downPoint = new Point(point.X, point.Y - 1);
            if (IsValid(downPoint) && IsSquare(downPoint, grid) && !alreadyInSet.Contains(downPoint))
            {
                Visit(downPoint, grid, alreadyInSet);
            }

            var leftPoint = new Point(point.X - 1, point.Y);
            if (IsValid(leftPoint) && IsSquare(leftPoint, grid) && !alreadyInSet.Contains(leftPoint))
            {
                Visit(leftPoint, grid, alreadyInSet);
            }

            var rightPoint = new Point(point.X + 1, point.Y);
            if (IsValid(rightPoint) && IsSquare(rightPoint, grid) && !alreadyInSet.Contains(rightPoint))
            {
                Visit(rightPoint, grid, alreadyInSet);
            }
        }

        private static bool IsSquare(Point point, int[,] grid)
        {
            return grid[point.X, point.Y] == 1;
        }

        private static bool IsValid(Point point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < 128 && point.Y < 128;
        }
    }
}