using MarsRobotsProblem.Grid;
using MarsRobotsProblem.Robot;
using NUnit.Framework;
using System.Linq;

namespace MarsRobotsProblem.Tests
{
    [TestFixture]
    public class RobotLossTests
    {
        [Test]
        public void NotLostTest_InputGrid2_2_Robot1_2_E_Perform_F_Expected2_2_E_NotLost()
        {
            // Arrange 
            var grid = new MarsGrid(2, 2);
            var robot = new MarsRobot(1, 2, RobotDirection.East);
            var instructions = new RobotInstruction[] { RobotInstruction.Forward };

            // Act 
            var result = robot.PerformInstructions(grid, instructions);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual((2, 2), (result.endX, result.endY));
                Assert.AreEqual(RobotDirection.East, result.direction);
                Assert.IsFalse(result.lost);
            });
        }

        [Test]
        public void NotLostTest_InputGrid2_2_Robot2_1_N_Perform_F_Expected2_2_N_NotLost()
        {
            // Arrange 
            var grid = new MarsGrid(2, 2);
            var robot = new MarsRobot(2, 1, RobotDirection.North);
            var instructions = new RobotInstruction[] { RobotInstruction.Forward };

            // Act 
            var result = robot.PerformInstructions(grid, instructions);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual((2, 2), (result.endX, result.endY));
                Assert.AreEqual(RobotDirection.North, result.direction);
                Assert.IsFalse(result.lost);
            });
        }

        [Test]
        public void NotLostTest_InputGrid2_2_Robot0_1_S_Perform_F_Expected0_0_S_NotLost()
        {
            // Arrange 
            var grid = new MarsGrid(2, 2);
            var robot = new MarsRobot(0, 1, RobotDirection.South);
            var instructions = new RobotInstruction[] { RobotInstruction.Forward };

            // Act 
            var result = robot.PerformInstructions(grid, instructions);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual((0, 0), (result.endX, result.endY));
                Assert.AreEqual(RobotDirection.South, result.direction);
                Assert.IsFalse(result.lost);
            });
        }

        [Test]
        public void NotLostTest_InputGrid2_2_Robot1_0_W_Perform_F_Expected0_0_W_NotLost()
        {
            // Arrange 
            var grid = new MarsGrid(2, 2);
            var robot = new MarsRobot(1, 0, RobotDirection.West);
            var instructions = new RobotInstruction[] { RobotInstruction.Forward };

            // Act 
            var result = robot.PerformInstructions(grid, instructions);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual((0, 0), (result.endX, result.endY));
                Assert.AreEqual(RobotDirection.West, result.direction);
                Assert.IsFalse(result.lost);
            });
        }

        [Test]
        public void LostTest_InputGrid2_2_Robot2_2_N_Perform_F_Expected2_2_N_Lost()
        {
            // Arrange 
            var grid = new MarsGrid(2, 2);
            var robot = new MarsRobot(2, 2, RobotDirection.North);
            var instructions = new RobotInstruction[] { RobotInstruction.Forward };

            // Act 
            var result = robot.PerformInstructions(grid, instructions);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual((2, 2), (result.endX, result.endY));
                Assert.AreEqual(RobotDirection.North, result.direction);
                Assert.IsTrue(result.lost);
            });
        }

        [Test]
        public void LostTest_InputGrid2_2_Robot2_2_E_Perform_F_Expected2_2_E_Lost()
        {
            // Arrange 
            var grid = new MarsGrid(2, 2);
            var robot = new MarsRobot(2, 2, RobotDirection.East);
            var instructions = new RobotInstruction[] { RobotInstruction.Forward };

            // Act 
            var result = robot.PerformInstructions(grid, instructions);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual((2, 2), (result.endX, result.endY));
                Assert.AreEqual(RobotDirection.East, result.direction);
                Assert.IsTrue(result.lost);
            });
        }

        [Test]
        public void LostTest_InputGrid2_2_Robot0_0_S_Perform_F_Expected0_0_S_Lost()
        {
            // Arrange 
            var grid = new MarsGrid(2, 2);
            var robot = new MarsRobot(0, 0, RobotDirection.South);
            var instructions = new RobotInstruction[] { RobotInstruction.Forward };

            // Act 
            var result = robot.PerformInstructions(grid, instructions);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual((0, 0), (result.endX, result.endY));
                Assert.AreEqual(RobotDirection.South, result.direction);
                Assert.IsTrue(result.lost);
            });
        }

        [Test]
        public void LostTest_InputGrid2_2_Robot0_0_W_Perform_F_Expected0_0_W_Lost()
        {
            // Arrange 
            var grid = new MarsGrid(2, 2);
            var robot = new MarsRobot(0, 0, RobotDirection.West);
            var instructions = new RobotInstruction[] { RobotInstruction.Forward };

            // Act 
            var result = robot.PerformInstructions(grid, instructions);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual((0, 0), (result.endX, result.endY));
                Assert.AreEqual(RobotDirection.West, result.direction);
                Assert.IsTrue(result.lost);
            });
        }

        [Test]
        public void LapOfHonorTest_InputGrid2_2_Robot0_0_N_Perform_FFRFFRFFRFFRRFFLFFLFFLFF_Expected0_0_S_NotLost()
        {
            // Arrange 
            var grid = new MarsGrid(2, 2);
            var robot = new MarsRobot(0, 0, RobotDirection.North);
            var instructions = Parsers.ParseInstructions("FFRFFRFFRFFRRFFLFFLFFLFF").ToArray();

            // Act 
            var result = robot.PerformInstructions(grid, instructions);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual((0, 0), (result.endX, result.endY));
                Assert.AreEqual(RobotDirection.South, result.direction);
                Assert.IsFalse(result.lost);
            });
        }

        [Test]
        public void ScentTest_InputFirstRobotLostSecondRepeatsRouteButHasDifferentLastDirection_ExpectedBothLost()
        {
            // Arrange 
            var grid = new MarsGrid(2, 2);
            var robot = new MarsRobot(2, 2, RobotDirection.North);
            var robot2 = new MarsRobot(2, 2, RobotDirection.North);
            var instructions = new RobotInstruction[] { RobotInstruction.Forward };
            var instructions2 = new RobotInstruction[] { RobotInstruction.TurnRight, RobotInstruction.Forward };

            // Act 
            var result1 = robot.PerformInstructions(grid, instructions);
            var result2 = robot2.PerformInstructions(grid, instructions2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual((2, 2), (result1.endX, result1.endY));
                Assert.AreEqual((2, 2), (result2.endX, result2.endY));
                Assert.IsTrue(result1.lost);
                Assert.IsTrue(result2.lost);
            });
        }
    }
}
