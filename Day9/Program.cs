using System;
using System.Collections.Generic;
using System.IO;

namespace Day9
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");

            //remove !
            while (true)
            {
                if (!input.Contains("!")) break;

                var indexOf = input.IndexOf('!');
                input = input.Remove(indexOf, 2);
            }

            int deleted = 0;
            
            //remove <>
            while (true)
            {
                int previousLength = input.Length;
                
                if (!input.Contains("<")) break;

                var indexOfOpen = input.IndexOf('<');
                var indexOfClose = input.IndexOf('>');
                input = input.Remove(indexOfOpen, indexOfClose - indexOfOpen + 1);

                deleted += previousLength - input.Length - 2;
            }

            input = input.Replace(",", "");

            int result = 0;
            int currentDepth = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (c == '{') currentDepth++;
                if (c == '}')
                {
                    result += currentDepth;
                    currentDepth--;
                }
            }

            Console.WriteLine(result);
            Console.WriteLine(deleted);
        }
    }
}