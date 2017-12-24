using System;
using System.Collections.Generic;
using System.IO;
using Day19;
using Day3;

namespace Day22
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var burstInfection = GetPart2(lines);
            Console.WriteLine(burstInfection);
        }

        private static int GetPart2(string[] lines)
        {
            var map = LoadDictionary(lines);

            PrintMap(map, 9);

            int burstInfection = 0;

            var currentDirection = Direction.Up;
            var currentPoint = new Point(0, 0);

            for (int i = 0; i < 10000000; i++)
            {
                if (!map.ContainsKey(currentPoint))
                {
                    map[currentPoint] = '.';
                }

                var c = map[currentPoint];
                if (c == '.')
                {
                    map[currentPoint] = 'W';
                    currentDirection = DirectionHelpers.TurnLeft(currentDirection);
                }
                else if (c == '#')
                {
                    map[currentPoint] = 'F';
                    currentDirection = DirectionHelpers.TurnRight(currentDirection);
                }
                else if (c == 'W')
                {
                    map[currentPoint] = '#';
                    burstInfection++;
                }
                else if (c == 'F')
                {
                    map[currentPoint] = '.';
                    currentDirection = DirectionHelpers.GetOppositeDirection(currentDirection);
                }

                currentPoint = DirectionHelpers.GetNextPoint(currentDirection, currentPoint);
            }

            PrintMap(map, 9);
            return burstInfection;
        }

        private static int GetPart1(string[] lines)
        {
            var map = LoadDictionary(lines);

            PrintMap(map, 9);

            int burstInfection = 0;

            var currentDirection = Direction.Up;
            var currentPoint = new Point(0, 0);

            for (int i = 0; i < 10000; i++)
            {
                if (!map.ContainsKey(currentPoint))
                {
                    map[currentPoint] = '.';
                }

                var c = map[currentPoint];
                if (c == '.')
                {
                    map[currentPoint] = '#';
                    currentDirection = DirectionHelpers.TurnLeft(currentDirection);
                    burstInfection++;
                }
                else if (c == '#')
                {
                    map[currentPoint] = '.';
                    currentDirection = DirectionHelpers.TurnRight(currentDirection);
                }

                currentPoint = DirectionHelpers.GetNextPoint(currentDirection, currentPoint);
            }

            PrintMap(map, 9);
            return burstInfection;
        }

        private static Dictionary<Point, char> LoadDictionary(string[] lines)
        {
            int len = lines.Length;

            var map = new Dictionary<Point, char>();

            for (int y = 0; y < len; y++)
            {
                for (int x = 0; x < len; x++)
                {
                    var point = new Point(GetCoord(x, len), GetCoord(y, len));
                    map[point] = lines[y][x];
                }
            }
            return map;
        }

        static void PrintMap(Dictionary<Point, char> map, int size)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    var xp = GetCoord(x, size);
                    var yp = GetCoord(y, size);

                    var p = new Point(xp, yp);
                    if (map.ContainsKey(p))
                    {
                        Console.Write(map[p]);
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        static int GetCoord(int i, int len)
        {
            if (i < len / 2)
            {
                int y = -((len / 2) - (i));
                return y;
            }
            else
            {
                int y = i - ((len / 2));
                return y;
            }
        }
    }
}