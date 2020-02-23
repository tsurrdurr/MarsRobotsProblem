using MarsRobotsProblem.Grid;
using MarsRobotsProblem.Robot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRobotsProblem
{
    class Program
    {
        private const char defaultSeparator = ' ';

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Waiting for input.");
                var (rightLimit, topLimit) = ParseGridSize();
                var grid = new MarsGrid(rightLimit, topLimit);

                while (true)
                {
                    (int startX, int startY, RobotDirection direction) = ParseRobotPosition();
                    var robot = new MarsRobot(startX, startY, direction);

                    var instructions = ParseInstructions();
                    (int endX, int endY, RobotDirection endDirection, bool lost) = robot.PerformInstructions(grid, instructions);
                    var lostText = lost ? "LOST" : "";
                    Console.WriteLine($"{endX} {endY} {endDirection.ToString()[0]} {lostText}");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Bad input: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }
        }

        private static (int, int) ParseGridSize()
        {
            var parsedLine = Console.ReadLine();
            var splitResult = parsedLine.Split(defaultSeparator).Take(2);
            var (rightLimitString, topLimitString) = (splitResult.FirstOrDefault(), splitResult.LastOrDefault());
            if (ReferenceEquals(topLimitString, rightLimitString))
                throw new ArgumentException("Not enough arguments.");

            if (!int.TryParse(topLimitString, out int topLimit)
             || !int.TryParse(rightLimitString, out int rightLimit))
                throw new ArgumentException("Expected 2 integers.");

            if (topLimit > 50 || rightLimit > 50)
                throw new ArgumentException("Maximum value for coordinate is 50.");

            return (rightLimit, topLimit);
        }

        private static (int startX, int startY, RobotDirection direction) ParseRobotPosition()
        {
            var parsedLine = Console.ReadLine();
            var splitResult = parsedLine.Split(defaultSeparator).Take(3).ToArray();
            if (splitResult.Length != 3)
                throw new ArgumentException("Not enough arguments.");
            var (stringX, stringY, directionString) = (splitResult[0], splitResult[1], splitResult[2]);
            var direction = ParseDirectionEnum(directionString);

            if (!int.TryParse(stringX, out int startX)
             || !int.TryParse(stringY, out int startY)
             || direction == RobotDirection.Unknown)
                throw new ArgumentException("Expected 2 integers and direction.");

            if (startX > 50 || startY > 50)
                throw new ArgumentException("Maximum value for coordinate is 50.");

            return (startX, startY, direction);
        }

        private static RobotInstruction[] ParseInstructions()
        {
            var parsedLine = Console.ReadLine().Trim();
            return Parsers.ParseInstructions(parsedLine).ToArray();
        }

        private static RobotDirection ParseDirectionEnum(string directionString) =>
            directionString switch
            {
                "N" => RobotDirection.North,
                "E" => RobotDirection.East,
                "S" => RobotDirection.South,
                "W" => RobotDirection.West,
                _   => RobotDirection.Unknown,
            };
    }
}
