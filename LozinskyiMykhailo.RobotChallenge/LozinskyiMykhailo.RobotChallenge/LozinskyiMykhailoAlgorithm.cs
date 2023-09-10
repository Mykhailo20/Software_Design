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
            Robot.Common.Robot movingRobot = robots[robotToMoveIndex];
            if ((movingRobot.Energy > 500) && (robots.Count < map.Stations.Count) && (RoundCount < 40))
            {
                return new CreateNewRobotCommand();
            }

            Position stationPosition = FindNearestFreeStation(robots[robotToMoveIndex], map, robots);


            if (stationPosition == null)
            {
                return null;
            }
                
            if (stationPosition == movingRobot.Position)
            {
                return new CollectEnergyCommand();
            }
            else
            {
                Position newPosition = stationPosition;
                int distance = DistanceHelper.FindDistance(stationPosition, movingRobot.Position);
                if (distance > 20) {
                    // int dx = Math.Sign(stationPosition.X - movingRobot.Position.X) * Math.Min(Math.Abs(stationPosition.X - movingRobot.Position.X), ...?);
                    int x = movingRobot.Position.X + (stationPosition.X - movingRobot.Position.X) * distance / 20;
                    int y = movingRobot.Position.Y + (stationPosition.Y - movingRobot.Position.Y) * distance / 20;
                    newPosition = new Position(x, y);
                }
                return new MoveCommand() { NewPosition = newPosition };
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
