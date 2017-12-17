using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day16
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var endChar = 'p';
            var numOfChars = endChar - 'a' + 1;

            var programs = GetInitialPrograms(numOfChars);
            var commands = File.ReadAllText("input.txt").Split(new char[] {','}).ToList();

            var result1 = Dance(programs, commands);
            Console.WriteLine(result1);

            programs = GetInitialPrograms(numOfChars);

            string result = "";
            var repetitions = GetNumOfRepetitions(programs, commands);
            Console.WriteLine(repetitions);

            programs = GetInitialPrograms(numOfChars);
            for (int i = 0; i < 1000000000 % repetitions; i++)
            {
                result = Dance(programs, commands);
            }
            Console.WriteLine(result);
        }

        private static int GetNumOfRepetitions(List<char> programs, List<string> commands)
        {
            var results = new HashSet<string>();
            while (true)
            {
                var result = Dance(programs, commands);
                if (results.Contains(result))
                {
                    return results.Count;
                }
                results.Add(result);
            }
        }

        private static List<char> GetInitialPrograms(int numOfChars)
        {
            List<char> programs = new List<char>();
            for (int i = 0; i < numOfChars; i++)
            {
                var c = (char) ('a' + i);
                programs.Add(c);
            }
            return programs;
        }

        private static string Dance(List<char> programs, List<string> commands)
        {
            foreach (var command in commands)
            {
                var spinMatch = Regex.Match(command, @"s(\d*)");
                if (spinMatch.Success)
                {
                    var spinSize = int.Parse(spinMatch.Groups[1].Value);
                    RotateRight(spinSize, programs);
                }

                var swapMatchIndex = Regex.Match(command, @"x(\d*)/(\d*)");
                if (swapMatchIndex.Success)
                {
                    var swapPosition = int.Parse(swapMatchIndex.Groups[1].Value);
                    var withPosition = int.Parse(swapMatchIndex.Groups[2].Value);
                    SwapPosition(programs, swapPosition, withPosition);
                }

                var swapMatchLetter = Regex.Match(command, @"p(\D*)/(\D*)");
                if (swapMatchLetter.Success)
                {
                    var swapLetter = swapMatchLetter.Groups[1].Value[0];
                    var withLetter = swapMatchLetter.Groups[2].Value[0];
                    SwapLetter(programs, swapLetter, withLetter);
                }
            }

            return string.Join("", programs);
        }

        private static void SwapLetter(List<char> inputChars, char swapPosition, char withPosition)
        {
            var swapWhat = inputChars.IndexOf(swapPosition);
            var swapWith = inputChars.IndexOf(withPosition);
            SwapPosition(inputChars, swapWhat, swapWith);
        }

        private static void RotateRight(int times, List<char> inputChars)
        {
            for (int time = 0; time < times; time++)
            {
                var tmp = inputChars[inputChars.Count - 1];
                for (int i = inputChars.Count - 1; i >= 1; i--)
                {
                    inputChars[i] = inputChars[i - 1];
                }
                inputChars[0] = tmp;
            }
        }

        private static void SwapPosition(List<char> inputChars, int swapPosition, int withPosition)
        {
            var tmp = inputChars[swapPosition];
            inputChars[swapPosition] = inputChars[withPosition];
            inputChars[withPosition] = tmp;
        }
    }
}