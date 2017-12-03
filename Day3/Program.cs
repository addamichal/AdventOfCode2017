using System;
using System.Collections.Generic;
using System.Reflection;

namespace Day3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            long actual = 289326;
            var answer1 = GetAnswer1(actual);
            var answer2 = GetAnswer2(actual);
            
            Console.WriteLine(answer1);
            Console.WriteLine(answer2);
        }

        private static long GetAnswer2(long actual)
        {
            var data = new Dictionary<Point, long>();
            long currentNumber = 1;
            long x = 0;
            long y = 0;
            data.Add(new Point(x, y), currentNumber);

            long maxX = 0;
            long maxY = 0;

            while (true)
            {
                maxX++;
                maxY++;

                //step one right
                x++;
                currentNumber = GetCurrentPointValue(data, x, y);
                data.Add(new Point(x, y), currentNumber);

                if (currentNumber >= actual)
                {
                    return currentNumber;
                }

                //up
                while (y != maxY)
                {
                    y++;
                    currentNumber = GetCurrentPointValue(data, x, y);
                    data.Add(new Point(x, y), currentNumber);

                    if (currentNumber >= actual)
                    {
                        return currentNumber;
                    }
                }

                //left
                while (x != -maxX)
                {
                    x--;
                    currentNumber = GetCurrentPointValue(data, x, y);
                    data.Add(new Point(x, y), currentNumber);

                    if (currentNumber >= actual)
                    {
                        return currentNumber;
                    }
                }

                //down
                while (y != -maxY)
                {
                    y--;
                    currentNumber = GetCurrentPointValue(data, x, y);
                    data.Add(new Point(x, y), currentNumber);

                    if (currentNumber >= actual)
                    {
                        return currentNumber;
                    }
                }

                //right
                while (x != maxX)
                {
                    x++;
                    currentNumber = GetCurrentPointValue(data, x, y);
                    data.Add(new Point(x, y), currentNumber);

                    if (currentNumber >= actual)
                    {
                        return currentNumber;
                    }
                }
            }
        }

        private static long GetCurrentPointValue(Dictionary<Point, long> data, long x, long y)
        {
            long value = 0;
            value += GetPointValue(data, x, y + 1);
            value += GetPointValue(data, x, y - 1);
            value += GetPointValue(data, x + 1, y);
            value += GetPointValue(data, x + 1, y + 1);
            value += GetPointValue(data, x + 1, y - 1);
            value += GetPointValue(data, x - 1, y);
            value += GetPointValue(data, x - 1, y + 1);
            value += GetPointValue(data, x - 1, y - 1);
            return value;
        }

        private static long GetPointValue(Dictionary<Point, long> data, long x, long y)
        {
            return !data.ContainsKey(new Point(x, y)) ? 0 : data[new Point(x, y)];
        }

        private static long GetAnswer1(long actual)
        {
            long currentNumber = 1;
            long x = 0;
            long y = 0;

            long maxX = 0;
            long maxY = 0;

            while (true)
            {
                maxX++;
                maxY++;

                //step one right
                x++;
                currentNumber++;

                if (currentNumber == actual)
                {
                    return Math.Abs(x) + Math.Abs(y);
                }

                //up
                while (y != maxY)
                {
                    currentNumber++;
                    y++;

                    if (currentNumber == actual)
                    {
                        return Math.Abs(x) + Math.Abs(y);
                    }
                }

                //left
                while (x != -maxX)
                {
                    currentNumber++;
                    x--;

                    if (currentNumber == actual)
                    {
                        return Math.Abs(x) + Math.Abs(y);
                    }
                }

                //down
                while (y != -maxY)
                {
                    currentNumber++;
                    y--;

                    if (currentNumber == actual)
                    {
                        return Math.Abs(x) + Math.Abs(y);
                    }
                }

                //right
                while (x != maxX)
                {
                    currentNumber++;
                    x++;

                    if (currentNumber == actual)
                    {
                        return Math.Abs(x) + Math.Abs(y);
                    }
                }
            }
        }
    }
}