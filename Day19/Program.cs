using System;
using System.Collections.Generic;
using System.IO;
using Day3;

namespace Day19
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int steps = 1;
            string letterString = "";

            var lines = File.ReadAllLines("input.txt");

            Direction currentDirection = Direction.Down;

            var indexOf = lines[0].IndexOf('|');
            var currentPoint = new Point(indexOf, 0);

            while (true)
            {
                currentPoint = DirectionHelpers.GetNextPoint(currentDirection, currentPoint);
                var letter = DirectionHelpers.GetLetter(lines, currentPoint);

                if (letter == ' ')
                {
                    break;
                }

                if (letter != '+' && letter != '-' && letter != '|')
                {
                    letterString += letter;
                }

                if (letter == '+')
                {
                    currentDirection = GetNextDirection(currentDirection, currentPoint, lines);
                }

                steps++;
            }

            Console.WriteLine(letterString);
            Console.WriteLine(steps);
        }

        private static Direction GetNextDirection(Direction currentDirection, Point currentPoint, string[] lines)
        {
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (direction == DirectionHelpers.GetOppositeDirection(currentDirection))
                    continue;

                var upPoint = DirectionHelpers.GetNextPoint(direction, currentPoint);
                var upLetter = DirectionHelpers.GetLetter(lines, upPoint);
                if (upLetter != ' ')
                {
                    return direction;
                }
            }

            throw new Exception("wheeeere");
        }
    }

    public class DirectionHelpers
    {
        public static Direction GetOppositeDirection(Direction currentDirection)
        {
            if (currentDirection == Direction.Up) return Direction.Down;
            if (currentDirection == Direction.Down) return Direction.Up;
            if (currentDirection == Direction.Left) return Direction.Right;
            if (currentDirection == Direction.Right) return Direction.Left;

            throw new NotImplementedException();
        }

        public static char GetLetter(string[] lines, Point nextPoint)
        {
            if (nextPoint.Y < 0 || nextPoint.Y >= lines.Length)
            {
                return ' ';
            }

            if (nextPoint.X < 0 || nextPoint.X >= lines[0].Length)
            {
                return ' ';
            }

            var letter = lines[nextPoint.Y][(int) nextPoint.X];
            return letter;
        }

        public static Point GetNextPoint(Direction direction, Point startingPoint)
        {
            Point nextPoint = null;
            if (direction == Direction.Down)
            {
                nextPoint = new Point(startingPoint.X, startingPoint.Y + 1);
            }
            if (direction == Direction.Up)
            {
                nextPoint = new Point(startingPoint.X, startingPoint.Y - 1);
            }
            if (direction == Direction.Left)
            {
                nextPoint = new Point(startingPoint.X - 1, startingPoint.Y);
            }
            if (direction == Direction.Right)
            {
                nextPoint = new Point(startingPoint.X + 1, startingPoint.Y);
            }
            return nextPoint;
        }

        public static Direction TurnLeft(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Left;
                    break;
                case Direction.Down:
                    return Direction.Right;
                    break;
                case Direction.Left:
                    return Direction.Down;
                    break;
                case Direction.Right:
                    return Direction.Up;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public static Direction TurnRight(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Right;
                    break;
                case Direction.Down:
                    return Direction.Left;
                    break;
                case Direction.Left:
                    return Direction.Up;
                    break;
                case Direction.Right:
                    return Direction.Down;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}