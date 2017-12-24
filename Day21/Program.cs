using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day21
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            GetAnswer(5);
            GetAnswer(18);
        }

        private static void GetAnswer(int input)
        {
            var patternArray = new List<List<char>>();
            patternArray.Add(new List<char>() {'.', '#', '.'});
            patternArray.Add(new List<char>() {'.', '.', '#'});
            patternArray.Add(new List<char>() {'#', '#', '#'});

            var lines = File.ReadAllLines("input.txt");
            var patternBook = new Dictionary<Pattern, Pattern>();
            foreach (var line in lines)
            {
                var parts = line.Split(new[] {" => "}, StringSplitOptions.RemoveEmptyEntries);
                patternBook.Add(new Pattern(parts[0]), new Pattern(parts[1]));
            }

            var basePattern = new Pattern(patternArray);
            //Console.WriteLine(basePattern);


            for (int i = 0; i < input; i++)
            {
                Console.WriteLine(i);
                var newBasePattern = new Pattern(basePattern.Size + basePattern.GetNumberOfSubParts());
                foreach (var inputPattern in patternBook.Keys)
                {
                    for (int y = 0; y < basePattern.GetNumberOfSubParts(); y++)
                    {
                        for (int x = 0; x < basePattern.GetNumberOfSubParts(); x++)
                        {
                            var subPattern = basePattern.GetSubPattern(x, y);

                            var flippedPattern = inputPattern.Flip();
                            for (int flipCount = 0; flipCount < 2; flipCount++)
                            {
                                flippedPattern = flippedPattern.Flip();
                                var rotatedPattern = flippedPattern.Rotate();

                                for (int rotateCound = 0; rotateCound < 4; rotateCound++)
                                {
                                    rotatedPattern = rotatedPattern.Rotate();

                                    if (Equals(subPattern, rotatedPattern))
                                    {
                                        newBasePattern.Replace(x, y, patternBook[inputPattern]);
                                    }
                                }
                            }
                        }
                    }
                }
                basePattern = newBasePattern;
            }

            Console.WriteLine(basePattern.GetAnswer());
        }
    }

    public class Pattern : IEquatable<Pattern>
    {
        private List<List<char>> pattern = new List<List<char>>();

        public Pattern(int size)
        {
            var newPattern = new List<List<char>>();
            for (int i = 0; i < size; i++)
            {
                newPattern.Add(new List<char>());
                for (int j = 0; j < size; j++)
                {
                    newPattern[i].Add('.');
                }
            }

            pattern = newPattern;
        }

        public Pattern(List<List<char>> pattern)
        {
            this.pattern = pattern;
        }

        public Pattern(string originalPatter)
        {
            var parts = originalPatter.Split(new[] {'/'});
            foreach (var part in parts)
            {
                pattern.Add(part.ToCharArray().ToList());
            }
        }

        public int Size
        {
            get { return pattern.Count; }
        }

        public bool Equals(Pattern other)
        {
            return this.ToString() == other.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Pattern) obj);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            List<string> lines = new List<string>();
            foreach (var arr in pattern)
            {
                lines.Add(string.Join("", arr));
            }
            return string.Join(Environment.NewLine, lines);
        }

        public bool CanSplit()
        {
            return pattern.Count % 2 == 0 || pattern.Count % 3 == 0;
        }

        public int GetNumberOfSubParts()
        {
            return pattern.Count % 2 == 0 ? pattern.Count / 2 : pattern.Count / 3;
        }

        public Pattern GetSubPattern(int x, int y)
        {
            int splitSize = pattern.Count % 2 == 0 ? 2 : 3;

            var newPatternArray = new List<List<char>>();

            for (int j = splitSize * y; j < splitSize * (y + 1); j++)
            {
                var line = new List<char>();
                for (int i = splitSize * x; i < splitSize * (x + 1); i++)
                {
                    line.Add(pattern[j][i]);
                }
                newPatternArray.Add(line);
            }

            return new Pattern(newPatternArray);
        }

        public void Replace(int x, int y, Pattern subPattern)
        {
            int splitSize = subPattern.pattern.Count;
            var startJ = splitSize * y;
            var maxJ = splitSize * (y + 1);

            for (int j = startJ; j < maxJ; j++)
            {
                var maxX = splitSize * (x + 1);
                var startI = splitSize * x;
                for (int i = startI; i < maxX; i++)
                {
                    var c = subPattern.pattern[j - startJ][i - startI];
                    pattern[j][i] = c;
                }
            }
        }

        public Pattern Rotate()
        {
            var newPattern = new List<List<char>>();
            for (int i = 0; i < pattern.Count; i++)
            {
                newPattern.Add(new List<char>());
                for (int j = 0; j < pattern.Count; j++)
                {
                    newPattern[i].Add('.');
                }
            }

            int n = pattern.Count;
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    newPattern[i][j] = pattern[n - j - 1][i];
                }
            }

            return new Pattern(newPattern);
        }

        public Pattern Flip()
        {
            var newPattern = new List<List<char>>();
            for (int i = 0; i < pattern.Count; i++)
            {
                var clone = Clone(pattern[i]);
                if (pattern[i] == clone) throw new Exception("not a clone");

                clone.Reverse();
                newPattern.Add(clone);
            }
            return new Pattern(newPattern);
        }

        private List<char> Clone(List<char> list)
        {
            return list.ToList();
        }

        public int GetAnswer()
        {
            int answer = 0;
            for (int i = 0; i < pattern.Count; i++)
            {
                for (int j = 0; j < pattern.Count; j++)
                {
                    if (pattern[i][j] == '#')
                        answer++;
                }
            }

            return answer;
        }
    }
}