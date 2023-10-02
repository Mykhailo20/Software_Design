using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;

namespace LozinskyiMykhailo.RobotChallenge.Test
{
    [TestClass]
    public class TestDistanceHelper
    {
        [TestMethod]
        public void TestDistance()
        {
            var pos1 = new Position(1, 1);
            var pos2 = new Position(2, 4);

            Assert.AreEqual(10, DistanceHelper.FindDistance(pos1, pos2));
        }
    }
}
