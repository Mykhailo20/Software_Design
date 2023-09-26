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
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(1, 1), RecoveryRate = 2 });

            Position expectedPosition = new Position(2, 2);

            var robots = new List<Robot.Common.Robot>() { 
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(2, 3)} 
                                                };
            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsTrue(command is MoveCommand);
            Assert.AreEqual(((MoveCommand)command).NewPosition, expectedPosition);
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
            Assert.AreEqual(((CreateNewRobotCommand)command).NewRobotEnergy, 200);
        }

        [TestMethod]
        public void TestCreateNewRobotAtEnd()
        {
            var algorithm = new LozinskyiMykhailoAlgorithm();
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(1, 1), RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 1500, Position = new Position(10, 10), RecoveryRate = 2 });

            Position expectedPosition = new Position(2, 2);

            var robots = new List<Robot.Common.Robot>() {
                                                    new Robot.Common.Robot(){ Energy = 1550, Position = new Position(2, 3)}
                                                };
            algorithm.RoundCount = 10;
            var command = algorithm.DoStep(robots, 0, map);
            Assert.IsTrue(command is CreateNewRobotCommand);
            Assert.AreEqual(((CreateNewRobotCommand)command).NewRobotEnergy, 200);

            algorithm.RoundCount = 46;
            var newCommand = algorithm.DoStep(robots, 0, map);
            Assert.IsTrue(newCommand is MoveCommand);
            Assert.AreEqual(((MoveCommand)newCommand).NewPosition, expectedPosition);
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
            Assert.IsFalse(isFirstStationFree);
            Assert.IsTrue(isSecondStationFree);
            Assert.IsTrue(isThirdStationFree);
        }

        [TestMethod]
        public void TestIsAvailablePosition()
        {
            var algorithm = new LozinskyiMykhailoAlgorithm();
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(1, 1), RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(0, 0) },
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(0, 1) },
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(1, 1)}
            };
            foreach (var robot in robots)
            {
                robot.OwnerName = algorithm.Author;
            }

            bool isPositionAvailable = algorithm.IsAvailablePosition(robots, new Position(0, 1), algorithm.Author);
            Assert.IsFalse(isPositionAvailable);

            isPositionAvailable = algorithm.IsAvailablePosition(robots, new Position(1, 0), algorithm.Author);
            Assert.IsTrue(isPositionAvailable);
        }

        [TestMethod]
        public void TestIsAlreadyNearStation()
        {
            var algorithm = new LozinskyiMykhailoAlgorithm();
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(1, 1), RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(0, 0) },
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(5, 5) },
            };
            foreach (var robot in robots)
            {
                robot.OwnerName = algorithm.Author;
            }

            bool isFirstRobotNearStation = algorithm.IsAlreadyNearStation(map, robots[0], robots);
            Assert.IsTrue(isFirstRobotNearStation);

            bool isSecondRobotNearStation = algorithm.IsAlreadyNearStation(map, robots[1], robots);
            Assert.IsFalse(isSecondRobotNearStation);
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

            int numberOfRobots = 2;
            bool stationIsSurrounded = algorithm.IsStationSurrounded(map.Stations[0].Position, robots[0], robots, numberOfRobots);
            Assert.IsFalse(stationIsSurrounded);

            robots.Add(new Robot.Common.Robot() { Energy = 200, Position = new Position(2, 1) });
            stationIsSurrounded = algorithm.IsStationSurrounded(map.Stations[0].Position, robots[0], robots, numberOfRobots);
            Assert.IsTrue(stationIsSurrounded);

            numberOfRobots += 1;
            stationIsSurrounded = algorithm.IsStationSurrounded(map.Stations[0].Position, robots[0], robots, numberOfRobots);
            Assert.IsFalse(stationIsSurrounded);

            robots.Add(new Robot.Common.Robot() { Energy = 200, Position = new Position(2, 2) });
            stationIsSurrounded = algorithm.IsStationSurrounded(map.Stations[0].Position, robots[0], robots, numberOfRobots);
            Assert.IsTrue(stationIsSurrounded);
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
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(1, 2)},
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(2, 2)}
                                                };
            int numberOfRobots = 2;
            Position nearestFreeStationPosition = algorithm.FindNearestFreeStation(robots[2], map, robots, numberOfRobots);
            Assert.AreEqual(nearestFreeStationPosition, map.Stations[1].Position);
        }

        [TestMethod]
        public void TestFindNearestFreeCell()
        {
            Position expectedPosition = new Position(4, 3);

            var algorithm = new LozinskyiMykhailoAlgorithm();
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(4, 4), RecoveryRate = 25});
            var robots = new List<Robot.Common.Robot>()
            {
                new Robot.Common.Robot() { Energy = 30, Position = new Position(2, 2) },
                new Robot.Common.Robot() { Energy = 30, Position = new Position(3, 3) },
            };
            var newPosition = algorithm.FindNearestFreeCell(robots[0], map, robots, new Position(3, 3), map.Stations[0].Position);
            Assert.AreEqual(expectedPosition, newPosition);
        }

        [TestMethod]
        public void TestFindCellToFoPosition() {
            Position expectedPosition = new Position(3, 4);
            var algorithm = new LozinskyiMykhailoAlgorithm();
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(1, 1), RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(4, 5), RecoveryRate = 2 });

            var robots = new List<Robot.Common.Robot>() {
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(2, 2)},
                                                    new Robot.Common.Robot(){ Energy = 200, Position = new Position(1, 1)},
                                                };
            foreach (var robot in robots)
            {
                robot.OwnerName = algorithm.Author;
            }
            int numberOfRobots = 1;
            Position cellToGoPosition = algorithm.FindCellToGoPosition(robots[0], map, robots, numberOfRobots);
            Assert.AreEqual(expectedPosition, cellToGoPosition);
        }
    }
}
