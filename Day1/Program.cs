using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Day1
{
    static class Program
    {
        public static void Main()
        {
            var text = File.ReadAllText("input.txt");
            var solution = new Solution();
            var answerPart1 = solution.GetAnswer(text);
            Console.WriteLine(answerPart1);
            var answerPart2 = solution.GetAnswer2(text);
            Console.WriteLine(answerPart2);
        }
    }

    public class Solution
    {
        public long GetAnswer(string input)
        {
            var digits = input.ToCharArray().Select(w => (int) char.GetNumericValue(w)).ToArray();

            long currentSum = 0;
            int lastDigit = -1;
            foreach (var digit in digits)
            {
                if (digit == lastDigit)
                {
                    currentSum += digit;
                }
                lastDigit = digit;
            }
            if (digits[0] == digits[digits.Length - 1])
            {
                currentSum += digits[0];
            }

            return currentSum;
        }

        public long GetAnswer2(string input)
        {
            var digits = input.ToCharArray().Select(w => (int) char.GetNumericValue(w)).ToArray();

            long currentSum = 0;
            for (int i = 0; i < digits.Length / 2; i++)
            {
                if (digits[i] == digits[i + digits.Length / 2])
                {
                    currentSum += 2* digits[i];
                }
            }

            return currentSum;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("1122", 3)]
        [TestCase("1111", 4)]
        [TestCase("1234", 0)]
        [TestCase("91212129", 9)]
        public void Part1Test(string input, int expected)
        {
            var solution = new Solution();
            var actual = solution.GetAnswer(input);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("1212", 6)]
        [TestCase("1221", 0)]
        [TestCase("123425", 4)]
        [TestCase("123123", 12)]
        [TestCase("12131415", 4)]
        public void Part2Test(string input, int expected)
        {
            var solution = new Solution();
            var actual = solution.GetAnswer2(input);
            Assert.AreEqual(expected, actual);
        }
    }
}