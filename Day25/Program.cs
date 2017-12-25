using System;
using System.Collections.Generic;
using System.Linq;

namespace Day25
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int[] tape = new int[100000];
            int currentPosition = 50000;
            char state = 'A';

            for(long i = 0; i < 12586542; i++)
            {
                if (state == 'A')
                {
                    int currentValue = tape[currentPosition];
                    if (currentValue == 0)
                    {
                        tape[currentPosition] = 1;
                        currentPosition++;
                        state = 'B';
                    }
                    if (currentValue == 1)
                    {
                        tape[currentPosition] = 0;
                        currentPosition--;
                        state = 'B';
                    }
                    continue;
                }
                if (state == 'B')
                {
                    int currentValue = tape[currentPosition];
                    if (currentValue == 0)
                    {
                        tape[currentPosition] = 0;
                        currentPosition++;
                        state = 'C';
                    }
                    if (currentValue == 1)
                    {
                        tape[currentPosition] = 1;
                        currentPosition--;
                        state = 'B';
                    }
                    continue;
                }
                if (state == 'C')
                {
                    int currentValue = tape[currentPosition];
                    if (currentValue == 0)
                    {
                        tape[currentPosition] = 1;
                        currentPosition++;
                        state = 'D';
                    }
                    if (currentValue == 1)
                    {
                        tape[currentPosition] = 0;
                        currentPosition--;
                        state = 'A';
                    }
                    continue;
                }
                if (state == 'D')
                {
                    int currentValue = tape[currentPosition];
                    if (currentValue == 0)
                    {
                        tape[currentPosition] = 1;
                        currentPosition--;
                        state = 'E';
                    }
                    if (currentValue == 1)
                    {
                        tape[currentPosition] = 1;
                        currentPosition--;
                        state = 'F';
                    }
                    continue;
                }
                if (state == 'E')
                {
                    int currentValue = tape[currentPosition];
                    if (currentValue == 0)
                    {
                        tape[currentPosition] = 1;
                        currentPosition--;
                        state = 'A';
                    }
                    if (currentValue == 1)
                    {
                        tape[currentPosition] = 0;
                        currentPosition--;
                        state = 'D';
                    }
                    continue;
                }
                if (state == 'F')
                {
                    int currentValue = tape[currentPosition];
                    if (currentValue == 0)
                    {
                        tape[currentPosition] = 1;
                        currentPosition++;
                        state = 'A';
                    }
                    if (currentValue == 1)
                    {
                        tape[currentPosition] = 1;
                        currentPosition--;
                        state = 'E';
                    }
                    continue;
                }
            }

            long count = tape.Count(w => w == 1);
            Console.WriteLine(count);
        }
    }
}