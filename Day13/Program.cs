using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var fw = new Dictionary<int, int>();
            foreach (var line in lines)
            {
                var parts = line.Split(new[] {": "}, StringSplitOptions.None);
                var layer = int.Parse(parts[0]);
                var depth = int.Parse(parts[1]);
                fw.Add(layer, depth);
            }

            var result = GetAnswer(fw);
            Console.WriteLine(result);

            int delay = 0;
            while (true)
            {
                var currentResult = IsCaugth(delay, fw);
                if (currentResult)
                    break;

                delay++;
            }
            Console.WriteLine(delay);
        }

        private static int GetAnswer(Dictionary<int, int> fw)
        {
            int result = 0;
            for (int i = 0; i <= fw.Keys.Max(); i++)
            {
                if (fw.ContainsKey(i))
                {
                    var position = GetPosition(fw[i], i);
                    if (position == 0)
                    {
                        result += fw[i] * i;
                    }
                }
            }
            return result;
        }

        private static bool IsCaugth(int delay, Dictionary<int, int> fw)
        {
            int result = 0;
            for (int s = delay; s <= fw.Keys.Max() + delay; s++)
            {
                var currentPosition = s - delay;

                if (fw.ContainsKey(currentPosition))
                {
                    var position = GetPosition(fw[currentPosition], s);
                    if (position == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static int GetPosition(int depth, int second)
        {
            second = second % (depth + depth - 2);

            for (int i = 0; i < depth; i++)
            {
                if (second == 0)
                    return i;

                second--;
            }

            for (int i = depth - 2; i >= 0; i--)
            {
                if (second == 0)
                    return i;

                second--;
            }

            throw new Exception();
        }
    }
}