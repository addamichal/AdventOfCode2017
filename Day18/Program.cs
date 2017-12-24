using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Day18
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var instructions = File.ReadLines("input.txt").ToList();

            var program = new MyProgram(0, instructions, true);
            program.Run();
            Console.WriteLine(program.lastSoundPlayed);

            var thread1 = new Thread(() =>
            {
                var myProgram = new MyProgram(0, instructions, false);
                myProgram.Run();

                //Console.WriteLine(myProgram.sendCount);
            });
            thread1.Name = "1";

            var thread2 = new Thread(() =>
            {
                var myProgram = new MyProgram(1, instructions, false);
                myProgram.Run();

                Console.WriteLine(myProgram.sendCount);
            });
            thread2.Name = "2";


            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();
        }
    }

    public class Memory
    {
        public static List<bool> waiting = new List<bool>() {false, false};

        public static object LockObject = new object();
        public static Dictionary<int, Queue<long>> Queues { get; set; }

        static Memory()
        {
            Queues = new Dictionary<int, Queue<long>>();
            Queues.Add(0, new Queue<long>());
            Queues.Add(1, new Queue<long>());
        }

        public static long GetValue(int queueNumber)
        {
            while (true)
            {
                lock (LockObject)
                {
                    if (waiting[0] && waiting[1])
                        throw new Exception("Deadlock");

                    var actualQueueNumber = queueNumber == 0 ? 1 : 0;

                    var queue = Queues[actualQueueNumber];
                    var numOfElements = queue.Count;

                    if (numOfElements != 0)
                    {
                        var num = queue.Dequeue();
                        waiting[queueNumber] = false;
                        return num;
                    }

                    waiting[queueNumber] = true;
                }
            }
        }

        public static void SetValue(int queueNumber, long value)
        {
            lock (LockObject)
            {
                //Console.WriteLine("SetValue {0} {1}", queueNumber, value);
                Queues[queueNumber].Enqueue(value);
            }
        }
    }

    public class MyProgram
    {
        private readonly bool runPart1;
        public int ProgramId { get; set; }
        public long lastSoundPlayed { get; set; }
        public long sendCount { get; set; }
        public Dictionary<string, long> registers;
        private List<string> instructions;

        public MyProgram(int programId, List<string> instuctions, bool runPart1)
        {
            this.ProgramId = programId;
            this.registers = CreateRegisters();
            this.instructions = instuctions;
            this.runPart1 = runPart1;
            this.registers["p"] = programId;
        }

        public void Run()
        {
            try
            {
                long currentInstruction = 0;

                while (true)
                {
                    string instruction = instructions[(int) currentInstruction];
                    var parts = instruction.Split(new[] {' '});

                    if (parts[0] == "snd")
                    {
                        var numValue = GetValue(parts[1]);
                        if (this.runPart1)
                        {
                            lastSoundPlayed = numValue;
                        }
                        else
                        {
                            Memory.SetValue(ProgramId, numValue);
                            sendCount++;
                        }
                    }
                    else if (parts[0] == "set")
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
                    else if (parts[0] == "mul")
                    {
                        var register = parts[1];

                        var numVal = GetValue(parts[2]);
                        registers[register] *= numVal;
                    }
                    else if (parts[0] == "mod")
                    {
                        var register = parts[1];

                        var numVal = GetValue(parts[2]);
                        registers[register] %= numVal;
                    }
                    else if (parts[0] == "rcv")
                    {
                        if (this.runPart1)
                        {
                            var numValue = GetValue(parts[1]);
                            if (numValue != 0)
                            {
                                throw new Exception("end");
                            }
                        }
                        else
                        {
                            long value = Memory.GetValue(ProgramId);
                            registers[parts[1]] = value;
                        }
                    }
                    else if (parts[0] == "jgz")
                    {
                        var value = GetValue(parts[1]);

                        if (value > 0)
                        {
                            long numOfJumps = GetValue(parts[2]);
                            currentInstruction += numOfJumps;
                            continue;
                        }
                    }
                    else
                    {
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
            for (int i = 'a'; i < 'z'; i++)
            {
                registers[((char) i).ToString()] = 0;
            }
            return registers;
        }
    }
}