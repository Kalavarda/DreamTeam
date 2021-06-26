using DreamTeam.Models;
using NUnit.Framework;

namespace DreamTeam.Tests.Models
{
    public class RoundBounds_Tests
    {
        [Test]
        public void Intersect_Test()
        {
            var bounds = new RoundBounds(new Point(4, 2), 0.25f);

            Assert.IsTrue(bounds.Intersect(new Point(4, 2)));
            
            Assert.IsTrue(bounds.Intersect(new Point(4.24f, 2)));
            Assert.IsTrue(bounds.Intersect(new Point(3.76f, 2)));
            Assert.IsTrue(bounds.Intersect(new Point(4, 2.24f)));
            Assert.IsTrue(bounds.Intersect(new Point(4, 1.76f)));
            
            Assert.IsFalse(bounds.Intersect(new Point(4.25f, 2.25f)));
            Assert.IsFalse(bounds.Intersect(new Point(3.75f, 1.75f)));
        }
    }
}
