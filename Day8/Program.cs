using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day8
{
    internal class Program
    {
        static int maxValue = int.MinValue;
        static Dictionary<string, int> registers = new Dictionary<string, int>();

        public static void Main(string[] args)
        {
            var instructions = File.ReadAllLines("input.txt");
            foreach (var instruction in instructions)
            {
                var parts = instruction.Split(new[] {" if "}, StringSplitOptions.None);
                var command = parts[0];
                var condition = parts[1];

                var evaluationResult = EvaluateCondition(condition);
                if (evaluationResult)
                {
                    ExecuteCommand(command);
                }
            }
            Console.WriteLine(registers.Values.Max());
            Console.WriteLine(maxValue);
        }

        private static bool EvaluateCondition(string condition)
        {
            var match = Regex.Match(condition, @"(\S*) (\S*) (\S*)");
            var matchSuccess = match.Success;
            var register = match.Groups[1].Value;
            var operation = match.Groups[2].Value;
            var value = int.Parse(match.Groups[3].Value);

            if (!registers.ContainsKey(register))
            {
                registers.Add(register, 0);
            }

            if (operation == ">")
            {
                return registers[register] > value;
            }
            if (operation == ">=")
            {
                return registers[register] >= value;
            }
            if (operation == "<")
            {
                return registers[register] < value;
            }
            if (operation == "<=")
            {
                return registers[register] <= value;
            }
            if (operation == "!=")
            {
                return registers[register] != value;
            }
            if (operation == "==")
            {
                return registers[register] == value;
            }

            throw new NotImplementedException();
        }

        private static void ExecuteCommand(string command)
        {
            var currentMax = registers.Values.Max();
            if (maxValue < currentMax)
            {
                maxValue = currentMax;
            }

            var match = Regex.Match(command, @"(\S*) (\S*) (\S*)");
            var register = match.Groups[1].Value;
            var operation = match.Groups[2].Value;
            var value = int.Parse(match.Groups[3].Value);

            if (!registers.ContainsKey(register))
            {
                registers.Add(register, 0);
            }

            if (operation == "inc")
            {
                registers[register] += value;
                return;
            }
            if (operation == "dec")
            {
                registers[register] -= value;
                return;
            }

            throw new NotImplementedException();
        }
    }
}