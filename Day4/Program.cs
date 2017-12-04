using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            int answerPart1 = 0;
            int answerPart2 = 0;
            
            foreach (var line in lines)
            {
                var words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var allWords = words.Length;
                var distinctWords = words.Distinct().Count();
                if (allWords == distinctWords)
                {
                    answerPart1++;
                }

                var orderedWords = words.Select(w => new string(w.ToCharArray().OrderBy(x => x).ToArray()));
                var allOrderedWords = words.Length;
                var distinctOrderedWords = orderedWords.Distinct().Count();
                if (allOrderedWords == distinctOrderedWords)
                {
                    answerPart2++;
                }
            }

            Console.WriteLine(answerPart1);
            Console.WriteLine(answerPart2);
        }
    }
}