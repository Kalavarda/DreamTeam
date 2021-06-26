using NUnit.Framework;
using DreamTeam.Models;

namespace DreamTeam.Tests.Models
{
    public class PointF_Tests
    {
        [Test]
        public void SetTest()
        {
            var count = 0;

            var p = new PointF();
            p.Changed += () =>
            {
                count++;
            };
            p.Set(1, 1);
            Assert.AreEqual(1, count);

            p.Set(1, 1);
            Assert.AreEqual(1, count);

            p.Set(1.1f, 1);
            Assert.AreEqual(2, count);
        }
    }
}
