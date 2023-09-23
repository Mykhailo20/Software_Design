 using Robot.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LozinskyiMykhailo.RobotChallenge
{
    public class LozinskyiMykhailoAlgorithm : IRobotAlgorithm
    {
        public int RoundCount { get; set; }
        private int RobotCount { get; set; }
        private int SmallMigrationCount { get; set; }
        private int energyToCollect = 50;
        public LozinskyiMykhailoAlgorithm() {
            Logger.OnLogRound += Logger_OnLogRound;
        }

        private void Logger_OnLogRound(object sender, LogRoundEventArgs e)
        {
            RoundCount += 1;
        }

        public bool IsStationFree(EnergyStation station, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            return IsCellFree(station.Position, movingRobot, robots);
        }

        public bool IsCellFree(Position cell, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            foreach (var robot in robots)
            {
                if (robot != movingRobot)
                {
                    if (robot.Position == cell)
                        return false;
                }
            }
            return true;
        }

        public bool IsValid(Position position) => position.X >= 0 && position.X < 100 && position.Y >= 0 && position.Y < 100;

        public bool IsAvailablePosition(Map map, IList<Robot.Common.Robot> robots, Position position, string author)
        {
            if (!IsValid(position))
            {
                return false;
            }
            foreach (Robot.Common.Robot robot in (IEnumerable<Robot.Common.Robot>)robots)
            {
                if (Position.Equals(robot.Position, position) && robot.OwnerName == author)
                    return false;
            }
            return true;
        }

        public bool IsAlreadyNearStation(Map map, Robot.Common.Robot robot, IList<Robot.Common.Robot> robots)
        {
            Position nearestStation = FindNearestFreeStation(robot, map, robots, 1);
            Position currentCellPosition = nearestStation.Copy();
            for (int i = -1; i < 2; i++)
            {
                currentCellPosition.X = nearestStation.X + i;
                for (int j = -1; j < 2; j++)
                {
                    currentCellPosition.Y = nearestStation.Y + j;
                    if (robot.Position.X == currentCellPosition.X && robot.Position.Y == currentCellPosition.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        
       
        public bool IsStationSurrounded(Position stationPosition, Robot.Common.Robot robot, IList<Robot.Common.Robot> robots, int numberOfRobots)
        {
            int counter = 0;
            Position currentCellPosition = stationPosition.Copy();
            for(int i = -1; i < 2; i++){
                currentCellPosition.X = stationPosition.X + i;
                for (int j =-1; j < 2; j++){
                    
                    currentCellPosition.Y = stationPosition.Y + j;
                    if(!IsCellFree(currentCellPosition, robot, robots) && IsValid(currentCellPosition))
                    {
                        counter++;
                        // If more than two robots are within the station's energy collection radius, it is invalid
                        if (counter >= numberOfRobots)
                        {
                            return true; 
                        }
                    }
                }
            }
            return false; 
        }

        // Changed
        public Position FindNearestFreeStation(Robot.Common.Robot movingRobot, Map map, IList<Robot.Common.Robot> robots, int numberOfRobots)
        {
            EnergyStation nearest = null;
            int minDistance = int.MaxValue;
            foreach (var station in map.Stations)
            {
                if (!IsStationSurrounded(station.Position, movingRobot, robots, numberOfRobots) && (station.Energy > 75) && (station.Position != movingRobot.Position))
                {
                    //if (IsStationFree(station, movingRobot, robots))
                    //{
                        int d = DistanceHelper.FindDistance(station.Position, movingRobot.Position);
                        if (d < minDistance)
                        {
                            minDistance = d;
                            nearest = station;
                        }
                    //}
                }
            }
            return (nearest == null) ? map.GetNearbyResources(movingRobot.Position, 100).OrderBy(obj =>
                    Math.Abs(movingRobot.Position.X - obj.Position.X) + Math.Abs(movingRobot.Position.Y - obj.Position.Y))
                .ToList()[0].Position : nearest.Position;
        }

        public Position FindNearestFreeCell(Robot.Common.Robot robot, Map map, IList<Robot.Common.Robot> robots, Position potentialFreePosition, Position stationPosition){
            Position bestPosition = new Position();
            bestPosition.X = potentialFreePosition.X;
            bestPosition.Y = potentialFreePosition.Y;
            int bestDistance = 100000;
            bool isCellFree = IsCellFree(potentialFreePosition, robot, robots);
            if(isCellFree && (IsValid(potentialFreePosition)) && (potentialFreePosition != robot.Position) &&
                IsAvailablePosition(map, robots, potentialFreePosition, Author) && 
                IsAvailablePosition(map, robots, potentialFreePosition, "Petrushynets Erikh"))
            {
                //Debug.Print("FindNearestFreeCell: Cell is Free");
                return potentialFreePosition;
            }
            else{
                Position currentCellPosition = stationPosition.Copy();
                for(int i = -1; i < 2; i++){
                    currentCellPosition.X = stationPosition.X + i;
                    for (int j =-1; j < 2; j++){
                        currentCellPosition.Y = stationPosition.Y + j;
                        if (IsCellFree(currentCellPosition, robot, robots) && 
                            (Math.Abs(potentialFreePosition.X - currentCellPosition.X) + 
                            Math.Abs(potentialFreePosition.Y - currentCellPosition.Y)) <= bestDistance 
                            && IsValid(currentCellPosition)
                            && (currentCellPosition != robot.Position) &&
                            IsAvailablePosition(map, robots, currentCellPosition, Author) &&
                            IsAvailablePosition(map, robots, currentCellPosition, "Petrushynets Erikh"))
                        {
                            bestDistance = Math.Abs(potentialFreePosition.X - currentCellPosition.X) + 
                                Math.Abs(potentialFreePosition.Y - currentCellPosition.Y) ;
                            bestPosition.X = currentCellPosition.X;
                            bestPosition.Y = currentCellPosition.Y;
                        }
                    }
                }
            }
            return bestPosition; 
        }

        public List<KeyValuePair<int, Position>> BestPositions(Map map, Robot.Common.Robot robot)
        {
            List<KeyValuePair<int, Position>> bestPositions = new List<KeyValuePair<int, Position>>();
            foreach (EnergyStation station in (IEnumerable<EnergyStation>)map.Stations)
            {
                Position currentCellPosition = station.Position;
                for (int i = -1; i < 2; i++)
                {
                    currentCellPosition.X = station.Position.X + i;
                    for (int j = -1; j < 2; j++)
                    {
                        currentCellPosition.Y = station.Position.Y + j;
                        if (IsValid(currentCellPosition))
                        {
                            bool positionExists = bestPositions.Any(kvp => kvp.Value.Equals(currentCellPosition));
                            if (positionExists)
                            {
                                var existingPosition = bestPositions.First(kvp => kvp.Value.Equals(currentCellPosition));
                                int updatedEnergy = existingPosition.Key + station.Energy;
                                bestPositions.Remove(existingPosition);
                                bestPositions.Add(new KeyValuePair<int, Position>(updatedEnergy, currentCellPosition));
                            }
                            else
                            {
                                bestPositions.Add(new KeyValuePair<int, Position>(station.Energy - DistanceHelper.FindDistance(robot.Position, currentCellPosition), currentCellPosition));
                            }
                        }
                    }
                }
            }

            return bestPositions.OrderByDescending(kvp => kvp.Key).ToList();
        }

        
        public Position FindCellToGoPosition(Robot.Common.Robot robot, Map map, IList<Robot.Common.Robot> robots)
        {
            /*var stationPosition = map.GetNearbyResources(robot.Position, 100).OrderBy(obj =>
                Math.Abs(robot.Position.X - obj.Position.X) + Math.Abs(robot.Position.Y - obj.Position.Y))
                .ToList()[0]
                .Position;*/
            int numberOfRobots = 1;
            var stationPosition = FindNearestFreeStation(robot, map, robots, numberOfRobots);   // returns null
            var CellToGo = stationPosition.Copy();

            if (robot.Position.X - CellToGo.X < 0)
            {
                CellToGo.X -= 1;
            }
            else if (robot.Position.X - CellToGo.X > 0)
            {
                CellToGo.X += 1;
            }
            if (robot.Position.Y - CellToGo.Y < 0)
            {
                CellToGo.Y -= 1;
            }
            else if (robot.Position.Y - CellToGo.Y > 0)
            {
                CellToGo.Y += 1;
            }
            CellToGo = FindNearestFreeCell(robot, map, robots, CellToGo, stationPosition);
            return CellToGo;                                                                                                                                                                                                                                                                                            
        }
       

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            var robot = robots[robotToMoveIndex];
            List<KeyValuePair<int, Position>> bestPositions = BestPositions(map, robot);

            if(RoundCount > 40)
            {
                energyToCollect = 20;
            }
            if (RoundCount == 30 || RoundCount == 40)
            {
                foreach (KeyValuePair<int, Position> bestPosition in bestPositions)
                {

                    if (IsAvailablePosition(map, robots, bestPosition.Value, Author) && 
                        IsAvailablePosition(map, robots, bestPosition.Value, "Petrushynets Erikh") &&
                        (DistanceHelper.FindDistance(robot.Position, bestPosition.Value) + 
                        DistanceHelper.FindDistance(robot.Position, bestPosition.Value) / 2 < bestPosition.Key) &&
                        (robot.Energy > DistanceHelper.FindDistance(robot.Position, bestPosition.Value) + 10) &&
                        DistanceHelper.FindDistance(robot.Position, bestPosition.Value) <= 400 &&
                            (bestPosition.Value != robot.Position))
                        return new MoveCommand
                        {
                            NewPosition = bestPosition.Value
                        };
                }
            }

            if ((RoundCount >= 20) && (RoundCount <= 23) && (RobotCount >= 85) && (SmallMigrationCount == 0))
            {
                foreach (KeyValuePair<int, Position> bestPosition in bestPositions)
                {

                    if (IsAvailablePosition(map, robots, bestPosition.Value, Author) &&
                        IsAvailablePosition(map, robots, bestPosition.Value, "Petrushynets Erikh") &&
                        (DistanceHelper.FindDistance(robot.Position, bestPosition.Value) +
                        DistanceHelper.FindDistance(robot.Position, bestPosition.Value) / 2 < bestPosition.Key) &&
                        (robot.Energy > DistanceHelper.FindDistance(robot.Position, bestPosition.Value) + 10) &&
                            (bestPosition.Value != robot.Position))
                        return new MoveCommand
                        {
                            NewPosition = bestPosition.Value
                        };
                }
                SmallMigrationCount += 1;
            }

            if (robot.Energy > 300 && RobotCount <= 89 && RoundCount <= 45) // RobotCount <= 99
            {
                RobotCount += 1;
                if(RobotCount == 90)
                {
                    energyToCollect = 25;
                }
                return new CreateNewRobotCommand();
            }

            IList<EnergyStation> nearbyResources = map.GetNearbyResources(robot.Position, 1);
            if (nearbyResources.Count > 0)
            {
                foreach (EnergyStation energyStation in nearbyResources)
                {
                    if (energyStation.Energy >= energyToCollect || RoundCount >= 48)
                        return new CollectEnergyCommand();
                }
            }

            if ((RobotCount < 90) && (RoundCount < 25))
            {
                foreach (var cell in bestPositions)
                {
                    if (IsCellFree(cell.Value, robot, robots) &&
                        DistanceHelper.FindDistance(robot.Position, cell.Value) < robot.Energy &&
                        IsAvailablePosition(map, robots, cell.Value, Author) &&
                        IsAvailablePosition(map, robots, cell.Value, "Petrushynets Erikh") &&
                        cell.Value != robot.Position)
                    {
                        return new MoveCommand() { NewPosition = cell.Value };
                    }

                    if (!IsCellFree(cell.Value, robot, robots) &&
                        DistanceHelper.FindDistance(robot.Position, cell.Value) + 10 < cell.Key &&
                        DistanceHelper.FindDistance(robot.Position, cell.Value) + 10 < robot.Energy &&
                        IsAvailablePosition(map, robots, cell.Value, Author) &&
                        IsAvailablePosition(map, robots, cell.Value, "Petrushynets Erikh") &&
                        cell.Value != robot.Position)
                    {
                        return new MoveCommand() { NewPosition = cell.Value };
                    }
                }
            }
            else
            {
                foreach (var cell in bestPositions)
                {
                    if (IsCellFree(cell.Value, robot, robots) &&
                        DistanceHelper.FindDistance(robot.Position, cell.Value) + 10 < cell.Key &&
                        DistanceHelper.FindDistance(robot.Position, cell.Value) + 10 <= 60 &&
                        IsAvailablePosition(map, robots, cell.Value, Author) &&
                        IsAvailablePosition(map, robots, cell.Value, "Petrushynets Erikh") &&
                        cell.Value != robot.Position)
                    {
                        return new MoveCommand() { NewPosition = cell.Value };
                    }

                    if (!IsCellFree(cell.Value, robot, robots) &&
                        DistanceHelper.FindDistance(robot.Position, cell.Value) + 20 < cell.Key &&
                        DistanceHelper.FindDistance(robot.Position, cell.Value) + 20 <= 70 &&
                        IsAvailablePosition(map, robots, cell.Value, Author) &&
                        IsAvailablePosition(map, robots, cell.Value, "Petrushynets Erikh") &&
                        cell.Value != robot.Position)
                    {
                        return new MoveCommand() { NewPosition = cell.Value };
                    }
                }
            }
            if(IsAlreadyNearStation(map, robot, robots))
            {
                foreach (EnergyStation energyStation in nearbyResources)
                {
                    if (energyStation.Energy > 0)
                    {
                        return new CollectEnergyCommand();
                    }     
                }
            }

            // Robot is not near station
            Position nearestPosition = FindCellToGoPosition(robot, map, robots);
            Position distance = new Position();
            Position positionToReturn = robot.Position;
            distance.X = robot.Position.X - nearestPosition.X;
            distance.Y = robot.Position.Y - nearestPosition.Y;
            if (distance.X != 0) positionToReturn.X -= distance.X / Math.Abs(distance.X) * 2;
            if (distance.Y != 0) positionToReturn.Y -= distance.Y / Math.Abs(distance.Y) * 2;
            return new MoveCommand() { NewPosition = positionToReturn };
        }

        public string Author { 
            get { return "Lozinskyi Mykhailo"; }
        }

        public string Description { 
            get { return "Demo for students"; } 
        }
    }
}
