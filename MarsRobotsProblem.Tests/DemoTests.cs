using MarsRobotsProblem.Grid;
using MarsRobotsProblem.Robot;
using NUnit.Framework;
using System;
using System.Linq;

namespace MarsRobotsProblem.Tests
{
    [TestFixture]
    public class DemoTests
    {
        [Test]
        public void DemoTest_InputGrid5_3_Robot1_1_E_RFRFRFRF_Expected1_1_E_NotLost()
        {
            // Arrange
            var grid = new MarsGrid(5, 3);
            var robot = new MarsRobot(1, 1, RobotDirection.East);
            var instructions = Parsers.ParseInstructions("RFRFRFRF").ToArray();

            // Act
            var result = robot.PerformInstructions(grid, instructions);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual((1, 1), (result.endX, result.endY));
                Assert.AreEqual(RobotDirection.East, result.direction);
                Assert.IsFalse(result.lost);
            });
        }

        [Test]
        public void DemoTest_InputGrid5_3_Robot3_2_N_FRRFLLFFRRFLL_Expected3_3_N_Lost()
        {
            // Arrange
            var grid = new MarsGrid(5, 3);
            var robot = new MarsRobot(3, 2, RobotDirection.North);
            var instructions = Parsers.ParseInstructions("FRRFLLFFRRFLL").ToArray();

            // Act
            var result = robot.PerformInstructions(grid, instructions);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual((3, 3), (result.endX, result.endY));
                Assert.AreEqual(RobotDirection.North, result.direction);
                Assert.IsTrue(result.lost);
            });
        }

        [Test]
        public void DemoTest_InputGrid5_3_Robot0_3_W_LLFFFLFLFL_ScentAt3_3_N_Expected2_3_S_NotLost()
        {
            // Arrange 
            var grid = new MarsGrid(5, 3);

            // create scent
            var robot = new MarsRobot(3, 2, RobotDirection.North);
            var instructions = Parsers.ParseInstructions("FRRFLLFFRRFLL").ToArray();
            robot.PerformInstructions(grid, instructions);

            var robot2 = new MarsRobot(0, 3, RobotDirection.West);
            var instructions2 = Parsers.ParseInstructions("LLFFFLFLFL").ToArray();

            // Act 
            var result = robot2.PerformInstructions(grid, instructions2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual((2, 3), (result.endX, result.endY));
                Assert.AreEqual(RobotDirection.South, result.direction);
                Assert.IsFalse(result.lost);
            });
        }
    }
}
