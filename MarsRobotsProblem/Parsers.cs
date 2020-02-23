using MarsRobotsProblem.Robot;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRobotsProblem
{
    public class Parsers
    {
        public static IEnumerable<RobotInstruction> ParseInstructions(string input)
        {
            if (input.Length >= 100)
                throw new ArgumentException("Instruction strings must be less than 100 characters in length.");

            for (int i = 0; i < input.Length; i++)
            {
                var instruction = ParseInstructionEnum(input[i]);
                if (instruction == RobotInstruction.Unknown)
                    throw new ArgumentException($"Encountered incorrect command at index {i}");
                yield return instruction;
            }
        }

        private static RobotInstruction ParseInstructionEnum(char instructionChar) =>
            instructionChar switch
            {
                'F' => RobotInstruction.Forward,
                'L' => RobotInstruction.TurnLeft,
                'R' => RobotInstruction.TurnRight,
                _ => RobotInstruction.Unknown,
            };

    }
}
