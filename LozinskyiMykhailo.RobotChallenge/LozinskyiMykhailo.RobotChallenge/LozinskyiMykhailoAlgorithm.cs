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
        public LozinskyiMykhailoAlgorithm() {
            Logger.OnLogRound += Logger_OnLogRound;
        }

        private void Logger_OnLogRound(object sender, LogRoundEventArgs e)
        {
            RoundCount += 1;
        }

        public int RoundCount { get; set; }
        public Position FindNearestFreeStation(Robot.Common.Robot movingRobot, Map map, IList<Robot.Common.Robot> robots)
        {
            EnergyStation nearest = null;
            int minDistance = int.MaxValue;
            foreach (var station in map.Stations)
            {
                if (IsStationFree(station, movingRobot, robots))
                {
                    int d = DistanceHelper.FindDistance(station.Position, movingRobot.Position);
                    if (d < minDistance)
                    {
                        minDistance = d;
                        nearest = station;
                    }
                }
            }
            return nearest == null ? null : nearest.Position;
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

        public bool IsStationSurrounded(Position stationPosition, Robot.Common.Robot robot, Map map, IList<Robot.Common.Robot> robots,
            int numberOfRobots)
        {
            int counter = 0;
            Position currentCellPosition = stationPosition.Copy();
            for(int i = -1; i < 2; i++){
                currentCellPosition.X = stationPosition.X + i;
                for (int j =-1; j < 2; j++){
                    
                    currentCellPosition.Y = stationPosition.Y + j;
                    if(!IsCellFree(currentCellPosition, robot, robots)){
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

        public Position FindNearestFreeCell(Robot.Common.Robot robot, Map map, IList<Robot.Common.Robot> robots, Position potentialFreePosition, Position stationPosition){
            Position bestPosition = new Position();
            bestPosition.X = potentialFreePosition.X;
            bestPosition.Y = potentialFreePosition.Y;
            int bestDistance = 100;
            bool isCellFree = IsCellFree(potentialFreePosition, robot, robots);
            if(isCellFree){
                Debug.Print("FindNearestFreeCell: Cell is Free");
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
            var stationPosition = map.GetNearbyResources(robot.Position, 100).OrderBy(obj =>
                Math.Abs(robot.Position.X - obj.Position.X) + Math.Abs(robot.Position.Y - obj.Position.Y))
                .ToList()[0]
                .Position;
            var CellToGo = stationPosition.Copy();
            int numOfRobots = 2;
            //if (!IsStationSurrounded(CellToGo, robot, map, robots, numOfRobots))
            //{
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
            //}
            /*else
            {
                Debug.Write($"Station is surrounded by {numOfRobots} robots");
                return robot.Position;
            } */                                                                                                                                                                                                                                                                                             
        }
       

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            const int energyToRemainsFirstRound = 50;
            const int energyToRemainsAnyRound = 20;
            var robot = robots[robotToMoveIndex];
            var CellToGo = FindCellToGoPosition(robot, map, robots);

            Position Distance = new Position();
            Position PositionToReturn = robot.Position;

            if (robot.Position.X == CellToGo.X && robot.Position.Y == CellToGo.Y)
            {
                return new CollectEnergyCommand();
            }
            Distance.X = robot.Position.X - CellToGo.X;
            Distance.Y = robot.Position.Y - CellToGo.Y; 
            
            int distanceEnergy = DistanceHelper.FindDistance(CellToGo, robot.Position);

            // Problem is here
            if ((robot.Energy - energyToRemainsFirstRound >= distanceEnergy) && (RoundCount == 1) || (robot.Energy - energyToRemainsAnyRound >= distanceEnergy) && (RoundCount > 1))
            {
                return new MoveCommand() { NewPosition = CellToGo };
            }
            else
            {   
                if(robot.Energy >= distanceEnergy){
                    return new MoveCommand() { NewPosition = CellToGo };
                }
                PositionToReturn.X -= (Distance.X)/ 2;
                PositionToReturn.Y -= (Distance.Y)/ 2;
                if(DistanceHelper.FindDistance(PositionToReturn, robot.Position) + DistanceHelper.FindDistance(CellToGo, PositionToReturn) <= robot.Energy - 20){
                    return new MoveCommand() { NewPosition = PositionToReturn };
                }
                else {
                    if(Distance.X != 0) PositionToReturn.X += Distance.X/ 2 - (Distance.X)/Math.Abs(Distance.X) * 2;
                    if(Distance.Y != 0) PositionToReturn.Y += Distance.Y/ 2 - (Distance.Y)/Math.Abs(Distance.Y) * 2;
                }
                return new MoveCommand() { NewPosition = PositionToReturn };
            }
        }

        public string Author { 
            get { return "Lozinskyi Mykhailo"; }
        }

        public string Description { 
            get { return "Demo for students"; } 
        }
    }
}
