using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Day10
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            GetAnswer1();
            var input = "106,118,236,1,130,0,235,254,59,205,2,87,129,25,255,118";
            var hash = Utils.GetKnotHash(input);

            Console.WriteLine(hash);
        }

        private static void GetAnswer1()
        {
            int skipSize = 0;
            var inputLengths = new List<int>() {106, 118, 236, 1, 130, 0, 235, 254, 59, 205, 2, 87, 129, 25, 255, 118};
            var arr = new int[256];
            for (int i = 0; i < 255; i++)
            {
                arr[i] = i;
            }

            int currentPosition = 0;
            for (int i = 0; i < inputLengths.Count; i++)
            {
                var len = inputLengths[i];
                Utils.Reverse(arr, currentPosition, len);
                currentPosition = (currentPosition + len + skipSize) % arr.Length;
                skipSize++;
            }

            Console.WriteLine(arr[0] * arr[1]);
        }
    }

    public static class Utils
    {
        public static string GetKnotHash(string input)
        {
            var charArray = input.ToCharArray();

            var inputLengths = new List<int>();
            for (int i = 0; i < charArray.Length; i++)
            {
                inputLengths.Add(charArray[i]);
            }
            inputLengths.AddRange(new List<int>() {17, 31, 73, 47, 23});

            var arr = new int[256];
            for (int i = 0; i < 256; i++)
            {
                arr[i] = i;
            }

            int skipSize = 0;
            int currentPosition = 0;

            for (int round = 0; round < 64; round++)
            {
                for (int i = 0; i < inputLengths.Count; i++)
                {
                    var len = inputLengths[i];
                    Reverse(arr, currentPosition, len);
                    currentPosition = (currentPosition + len + skipSize) % arr.Length;
                    skipSize++;
                }
            }

            string hash = "";
            for (int i = 0; i < 16; i++)
            {
                var arrx = arr.Skip(i * 16).Take(16).ToArray();
                int o = arrx[0];
                for (int j = 1; j < 16; j++)
                {
                    o ^= arrx[j];
                }
                hash += o.ToString("X2").ToLower();
            }
            return hash;
        }

        public static void Reverse(int[] array, int startIndex, int size)
        {
            for (int i = 0; i < size / 2; i++)
            {
                var x = (startIndex + i) % array.Length;
                var y = (startIndex + size - 1 - i) % array.Length;
                Swap(array, x, y);
            }
        }

        public static void Swap(int[] array, int x, int y)
        {
            int temp = array[x];
            array[x] = array[y];
            array[y] = temp;
        }

        public static void PrintArray(int[] arr)
        {
            Console.WriteLine(string.Join(", ", arr));
        }

        public static void PrintArray(int[] arr, int size)
        {
            var arr2 = arr.Take(size).ToArray();
            PrintArray(arr2);
        }
    }

    public class Tests
    {
        [TestCase("", "a2582a3a0e66e6e86e3812dcb672a272")]
        [TestCase("AoC 2017", "33efeb34ea91902bb2f59c9920caa6cd")]
        [TestCase("1,2,3", "3efbe78a8d82f29979031a4aa0b16a9d")]
        [TestCase("1,2,4", "63960835bcdc130f0b66d7ff4f6a5a8e")]
        public void Test(string input, string expected)
        {
            Assert.AreEqual(expected, Utils.GetKnotHash(input));
        }
    }
}