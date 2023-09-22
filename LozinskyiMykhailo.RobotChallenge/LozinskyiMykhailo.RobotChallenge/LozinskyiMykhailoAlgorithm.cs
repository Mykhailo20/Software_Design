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
        private int EnergyRadius = 1;

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

        public bool IsAlreadyNearStation(Map map, Robot.Common.Robot robot, IList<Robot.Common.Robot> robots)
        {
            Position nearestStation = FindNearestFreeStation(robot, map, robots);
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

        
        public bool IsValid(Position position) => position.X >= 0 && position.X < 100 && position.Y >= 0 && position.Y < 100;

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
        public Position FindNearestFreeStation(Robot.Common.Robot movingRobot, Map map, IList<Robot.Common.Robot> robots)
        {
            EnergyStation nearest = null;
            int minDistance = int.MaxValue;
            int numberOfRobots = 2;
            foreach (var station in map.Stations)
            {
                if (!IsStationSurrounded(station.Position, movingRobot, robots, numberOfRobots))
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
            return nearest == null ? null : nearest.Position;
        }

        public Position FindNearestFreeCell(Robot.Common.Robot robot, Map map, IList<Robot.Common.Robot> robots, Position potentialFreePosition, Position stationPosition){
            Position bestPosition = new Position();
            bestPosition.X = potentialFreePosition.X;
            bestPosition.Y = potentialFreePosition.Y;
            int bestDistance = 100;
            bool isCellFree = IsCellFree(potentialFreePosition, robot, robots);
            if(isCellFree){
                //Debug.Print("FindNearestFreeCell: Cell is Free");
                return potentialFreePosition;
            }
            else{
                Position currentCellPosition = stationPosition.Copy();
                for(int i = -1; i < 2; i++){
                    currentCellPosition.X = stationPosition.X + i;
                    for (int j =-1; j < 2; j++){
                        currentCellPosition.Y = stationPosition.Y + j;
                        if (IsCellFree(currentCellPosition, robot, robots) && (Math.Abs(potentialFreePosition.X - currentCellPosition.X) + Math.Abs(potentialFreePosition.Y - currentCellPosition.Y)) <= bestDistance )
                        {
                            bestDistance = Math.Abs(potentialFreePosition.X - currentCellPosition.X) + Math.Abs(potentialFreePosition.Y - currentCellPosition.Y) ;
                            bestPosition.X = currentCellPosition.X;
                            bestPosition.Y = currentCellPosition.Y;
                        }
                    }
                }
            }
            return bestPosition; 
        }

        public Position FindCellToGoPosition(Robot.Common.Robot robot, Map map, IList<Robot.Common.Robot> robots)
        {
            /*var stationPosition = map.GetNearbyResources(robot.Position, 100).OrderBy(obj =>
                Math.Abs(robot.Position.X - obj.Position.X) + Math.Abs(robot.Position.Y - obj.Position.Y))
                .ToList()[0]
                .Position;*/
            var stationPosition = FindNearestFreeStation(robot, map, robots);
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

            if (robot.Energy > 300 && RobotCount <= 99 && RoundCount <= 45)
            {
                RobotCount += 1;
                return new CreateNewRobotCommand();
            }

            var cellToGo = FindCellToGoPosition(robot, map, robots);

            Position distance = new Position();
            Position positionToReturn = robot.Position;

            if (IsAlreadyNearStation(map, robot, robots))
            {
                return new CollectEnergyCommand();
            }

            distance.X = robot.Position.X - cellToGo.X;
            distance.Y = robot.Position.Y - cellToGo.Y; 
            
            int distanceEnergy = DistanceHelper.FindDistance(cellToGo, robot.Position);

            const int energyToRemainsFirstRound = 50;
            const int energyToRemainsAnyRound = 20;

            // Problem is here
            if ((robot.Energy - energyToRemainsFirstRound >= distanceEnergy) && (RoundCount == 1) || (robot.Energy - energyToRemainsAnyRound >= distanceEnergy) && (RoundCount > 1))
            {
                return new MoveCommand() { NewPosition = cellToGo };
            }
 
            positionToReturn.X -= distance.X/ 2;
            positionToReturn.Y -= distance.Y/ 2;
            if(DistanceHelper.FindDistance(positionToReturn, robot.Position) + DistanceHelper.FindDistance(cellToGo, positionToReturn) <= robot.Energy - energyToRemainsAnyRound)
            {
                return new MoveCommand() { NewPosition = positionToReturn };
            }
            if (distance.X != 0) positionToReturn.X += distance.X/2 - distance.X/Math.Abs(distance.X) * 2;
            if(distance.Y != 0) positionToReturn.Y += distance.Y/2 - distance.Y/Math.Abs(distance.Y) * 2;
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
