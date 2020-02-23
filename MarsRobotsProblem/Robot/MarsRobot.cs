using MarsRobotsProblem.Grid;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRobotsProblem.Robot
{
    public class MarsRobot
    {
        private MarsCoordinates _coordinates;
        private RobotDirection _direction;
        private bool _lost = false;

        public MarsRobot(int x, int y, RobotDirection direction)
        {
            _coordinates = new MarsCoordinates(x, y);
            _direction = direction;
            if (_direction == RobotDirection.Unknown)
                throw new ArgumentException("Invalid direction");
        }

        public (int endX, int endY, RobotDirection direction, bool lost) PerformInstructions(MarsGrid grid, RobotInstruction[] instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction == RobotInstruction.Forward)
                {
                    Move(grid);
                    if (_lost) break;
                }
                else if (instruction == RobotInstruction.TurnLeft)
                {
                    _direction = _direction == RobotDirection.North ? RobotDirection.West : _direction - 1;
                }
                else if (instruction == RobotInstruction.TurnRight)
                {
                    _direction = _direction == RobotDirection.West ? RobotDirection.North : _direction + 1;
                }
                else
                {
                    throw new NotImplementedException("Unknown command");
                }
            }
            return (_coordinates.PositionX, _coordinates.PositionY, _direction, _lost);
        }

        private void Move(MarsGrid grid)
        {
            var previousPosition = _coordinates;

            if (grid.HasScent(_coordinates, _direction))
            {
                return;
            }

            switch (_direction)
            {
                case RobotDirection.North:
                    _coordinates.PositionY++;
                    break;
                case RobotDirection.South:
                    _coordinates.PositionY--;
                    break;
                case RobotDirection.East:
                    _coordinates.PositionX++;
                    break;
                case RobotDirection.West:
                    _coordinates.PositionX--;
                    break;
                default: throw new ArgumentException("Invalid robot direction");
            };

            if (_coordinates.PositionX > grid.MaxX
             || _coordinates.PositionX < grid.MinX
             || _coordinates.PositionY > grid.MaxY
             || _coordinates.PositionY < grid.MinY)
            {
                _lost = true;
                _coordinates = previousPosition;
                grid.AddScent(_coordinates, _direction);
            }
        }
    }
}
