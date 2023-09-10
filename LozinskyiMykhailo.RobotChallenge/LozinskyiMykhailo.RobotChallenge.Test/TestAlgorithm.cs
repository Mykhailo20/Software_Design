using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LozinskyiMykhailo.RobotChallenge.Test
{
    [TestClass]
    public class TestAlgorithm
    {
        [TestMethod]
        public void TestMoveCommand()
        {
            var algorithm = new LozinskyiMykhailoAlgorithm();
            var map = new Map();
            Position stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>() { 
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(2, 3)} 
                                                };
            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsTrue(command is MoveCommand);
            Assert.AreEqual(((MoveCommand)command).NewPosition, stationPosition);
        }

        [TestMethod]
        public void TestCollectEnergyCommand()
        {
            var algorithm = new LozinskyiMykhailoAlgorithm();
            var map = new Map();
            Position stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>() {
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(1, 1)}
                                                };
            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsTrue(command is CollectEnergyCommand);
        }

        [TestMethod]
        public void TestCreateNewRobotCommand()
        {
            var algorithm = new LozinskyiMykhailoAlgorithm();
            var map = new Map();
            Position stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 1500, Position = stationPosition, RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                                                    new Robot.Common.Robot(){ Energy = 550, Position = new Position(2, 3)}
                                                };
            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsTrue(command is CreateNewRobotCommand);
            Assert.AreEqual(((CreateNewRobotCommand)command).NewRobotEnergy, 100);
        }

        [TestMethod]
        public void TestCreateNewRobotAtEnd()
        {
            var algorithm = new LozinskyiMykhailoAlgorithm();
            var map = new Map();
            Position stationPosition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = stationPosition, RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 1500, Position = new Position(10, 10), RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                                                    new Robot.Common.Robot(){ Energy = 550, Position = new Position(2, 3)}
                                                };
            algorithm.RoundCount = 20;
            var command = algorithm.DoStep(robots, 0, map);
            Assert.IsTrue(command is CreateNewRobotCommand);
            Assert.AreEqual(((CreateNewRobotCommand)command).NewRobotEnergy, 100);

            algorithm.RoundCount = 40;
            var newCommand = algorithm.DoStep(robots, 0, map);
            Assert.IsTrue(newCommand is MoveCommand);
            Assert.AreEqual(((MoveCommand)newCommand).NewPosition, stationPosition);
        }
    }
}
