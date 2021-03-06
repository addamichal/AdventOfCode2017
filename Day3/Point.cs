﻿using System.Collections.Generic;

namespace Day3
{
    public class Point : IEqualityComparer<Point>
    {
        public long X { get; }
        public long Y { get; }

        public Point(long x, long y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return this.ToString() == obj.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public bool Equals(Point x, Point y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Point obj)
        {
            return obj.GetHashCode();
        }
    }
}