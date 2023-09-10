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
        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            var myRobot = robots[robotToMoveIndex];

            var newPosition = myRobot.Position;
            newPosition.X = newPosition.X + 1;
            newPosition.Y = newPosition.Y + 1;

            return new MoveCommand() { NewPosition = newPosition };
        }

        public string Author { 
            get { return "Lozinskyi Mykhailo"; }
        }

        public string Description { 
            get { return "Demo for students"; } 
        }
    }
}
