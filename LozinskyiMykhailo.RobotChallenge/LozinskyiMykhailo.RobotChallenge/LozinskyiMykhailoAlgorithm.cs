using Robot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            var Robot = robots[robotToMoveIndex];
            var CellToGo = map.GetNearbyResources(Robot.Position, 100).OrderBy(obj =>
                    Math.Abs(Robot.Position.X - obj.Position.X) + Math.Abs(Robot.Position.Y - obj.Position.Y))
                .ToList()[0]
                .Position;
            Position Distance = new Position();
            Position PositionToReturn = Robot.Position;
            if (Robot.Position.X - CellToGo.X < 0)
            {
                CellToGo.X -= 1;
            }
            else if (Robot.Position.X - CellToGo.X > 0)
            {
                CellToGo.X += 1;
            }
            if (Robot.Position.Y - CellToGo.Y < 0)
            {
                CellToGo.Y -= 1;
            }
            else if (Robot.Position.Y - CellToGo.Y > 0)
            {
                CellToGo.Y += 1;
            }
            if (Robot.Position.X == CellToGo.X && Robot.Position.Y == CellToGo.Y)
            {
                return new CollectEnergyCommand();
            }
            Distance.X = Robot.Position.X - CellToGo.X;
            Distance.Y = Robot.Position.Y - CellToGo.Y;
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
