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

        [TestMethod]
        public void TestFindNearestFreeStation()
        {
            var algorithm = new LozinskyiMykhailoAlgorithm();
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(1, 1), RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(4, 5), RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(10, 10), RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(1, 1)},
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(6, 7)}
                                                };

            Position nearestFreeStationPosition = algorithm.FindNearestFreeStation(robots[1], map, robots);
            Assert.AreEqual(nearestFreeStationPosition, map.Stations[1].Position);
        }

        [TestMethod]
        public void TestIsStationFree()
        {
            var algorithm = new LozinskyiMykhailoAlgorithm();
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(1, 1), RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(4, 5), RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(10, 10), RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(1, 1)},
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(6, 7)}
                                                };

            bool isFirstStationFree = algorithm.IsStationFree(map.Stations[0], robots[1], robots);
            bool isSecondStationFree = algorithm.IsStationFree(map.Stations[1], robots[1], robots);
            bool isThirdStationFree = algorithm.IsStationFree(map.Stations[2], robots[1], robots);

            Assert.AreEqual(isFirstStationFree, false);
            Assert.AreEqual(isSecondStationFree, true);
            Assert.AreEqual(isThirdStationFree, true);
        }

        [TestMethod]
        public void TestIsStationSurrounded()
        {
            var algorithm = new LozinskyiMykhailoAlgorithm();
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(1, 1), RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(10, 10) },
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(1, 0)}      
                                                };

            bool stationIsSurrounded1 = algorithm.IsStationSurrounded(map.Stations[0].Position, robots[0], map, robots, 2);
            Assert.IsFalse(stationIsSurrounded1);

            robots.Add(new Robot.Common.Robot() { Energy = 200, Position = new Position(2, 1) });
            bool stationIsSurrounded2 = algorithm.IsStationSurrounded(map.Stations[0].Position, robots[0], map, robots, 2);
            Assert.IsTrue(stationIsSurrounded2);

            
            bool stationIsSurrounded3 = algorithm.IsStationSurrounded(map.Stations[0].Position, robots[0], map, robots, 3);
            Assert.IsFalse(stationIsSurrounded3);

            robots.Add(new Robot.Common.Robot() { Energy = 200, Position = new Position(2, 2) });
            bool stationIsSurrounded4 = algorithm.IsStationSurrounded(map.Stations[0].Position, robots[0], map, robots, 3);
            Assert.IsTrue(stationIsSurrounded4);

        }
    }
}
