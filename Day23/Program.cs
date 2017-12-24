using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day23
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var instructions = File.ReadAllLines("input.txt").ToList();
            var myProgram = new MyProgram(instructions);
            myProgram.Run();
            Console.WriteLine(myProgram.mulCalled);

            var part2 = GetPart2();
            Console.WriteLine(part2);
        }

        private static int GetPart2()
        {
            int primeCount = 0;
            for (int i = 106500; i <= 123500; i += 17)
            {
                if (IsPrime(i))
                {
                    primeCount++;
                }
            }
            return primeCount;
        }

        private static bool IsPrime(long number)
        {
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class MyProgram
    {
        private long currentInstruction = 0;
        public int mulCalled = 0;
        public Dictionary<string, long> registers;
        private List<string> instructions;

        public MyProgram(List<string> instuctions)
        {
            this.registers = CreateRegisters();
            this.instructions = instuctions;
        }

        public void Run()
        {
            try
            {
                //currentInstruction = 0;

                while (true)
                {
                    var instruction = instructions[(int) currentInstruction];

                    var parts = instruction.Split(new[] {' '});

                    if (parts[0] == "set")
                    {
                        var register = parts[1];

                        var numVal = GetValue(parts[2]);
                        registers[register] = numVal;
                    }
                    else if (parts[0] == "add")
                    {
                        var register = parts[1];

                        var numVal = GetValue(parts[2]);
                        registers[register] += numVal;
                    }
                    else if (parts[0] == "sub")
                    {
                        var register = parts[1];

                        var numVal = GetValue(parts[2]);
                        registers[register] -= numVal;
                    }
                    else if (parts[0] == "mul")
                    {
                        var register = parts[1];

                        var numVal = GetValue(parts[2]);
                        registers[register] *= numVal;
                        mulCalled++;
                    }
                    else if (parts[0] == "mod")
                    {
                        var register = parts[1];

                        var numVal = GetValue(parts[2]);
                        registers[register] %= numVal;
                    }
                    else if (parts[0] == "jnz")
                    {
                        var value = GetValue(parts[1]);

                        if (value != 0)
                        {
                            long numOfJumps = GetValue(parts[2]);
                            currentInstruction += numOfJumps;
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine(instruction);
                        throw new Exception("unknown instruction");
                    }

                    currentInstruction++;
                }
            }
            catch (Exception e)
            {
                //i know this is bad, but in order not to produce any output i leave this one empty
            }
        }

        private long GetValue(string val)
        {
            long numVal;
            if (!long.TryParse(val, out numVal))
            {
                string targetRegister = val;
                numVal = registers[targetRegister];
            }
            return numVal;
        }

        private static Dictionary<string, long> CreateRegisters()
        {
            var registers = new Dictionary<string, long>();
            for (int i = 'a'; i <= 'h'; i++)
            {
                registers[((char) i).ToString()] = 0;
            }
            return registers;
        }
    }
}