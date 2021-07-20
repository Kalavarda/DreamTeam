using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using DreamTeam.Utils;
using Moq;
using NUnit.Framework;

namespace DreamTeam.Tests.Utils
{
    public class PriorityTargetDetector_Tests
    {
        private readonly Mock<IFight> _fight = new Mock<IFight>();
        private readonly Mock<IRelationDetector> _relationDetector = new Mock<IRelationDetector>();

        [Test]
        public void GetPriorityTarget_Test()
        {
            var detector = new PriorityTargetDetector(_fight.Object, _relationDetector.Object);

            var team = new HeroTeam();
            team.Support.HP.Value = 0;
            team.Tank.HP.Value = 50;
            team.Healer.HP.Value = 75;
            Assert.AreEqual(team.Tank, detector.GetPriorityTarget(team.Healer));

            team = new HeroTeam();
            team.Tank.HP.Value = 0;
            team.Healer.HP.Value = 75;
            Assert.AreEqual(team.Healer, detector.GetPriorityTarget(team.Healer));
        }
    }
}
