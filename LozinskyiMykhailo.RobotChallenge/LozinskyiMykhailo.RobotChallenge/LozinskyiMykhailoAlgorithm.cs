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
        

        public Position FindCellToGoPosition(Robot.Common.Robot robot, Map map, IList<Robot.Common.Robot> robots)
        {
            var CellToGo = map.GetNearbyResources(robot.Position, 100).OrderBy(obj =>
                    Math.Abs(robot.Position.X - obj.Position.X) + Math.Abs(robot.Position.Y - obj.Position.Y))
                .ToList()[0]
                .Position;
            int numOfRobots = 2;
            if (IsStationSurrounded(CellToGo, robot, map, robots, numOfRobots))
            {
                Console.WriteLine($"Station is surrounded by {numOfRobots} robots");
                return robot.Position;
            }
            else
            {
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
                return CellToGo;
            }
        }
       

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
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
            if (Distance.X != 0) PositionToReturn.X -= Distance.X / Math.Abs(Distance.X);
            if (Distance.Y != 0) PositionToReturn.Y -= Distance.Y / Math.Abs(Distance.Y);
            return new MoveCommand() { NewPosition = PositionToReturn };
        }

        public string Author { 
            get { return "Lozinskyi Mykhailo"; }
        }

        public string Description { 
            get { return "Demo for students"; } 
        }
    }
}
