using DreamTeam.Models;
using NUnit.Framework;

namespace DreamTeam.Tests.Models
{
    public class RangeF_Tests
    {
        [TestCase(0, 100)]
        [TestCase(-50, 150)]
        [TestCase(-0.5f, 100500)]
        public void Value_Test(float min, float max)
        {
            var eventCount = 0;

            var range = new RangeF(min, max);
            range.ValueChanged += obj =>
            {
                eventCount++;
            };

            range.Value = (min + max) / 2;
            Assert.AreEqual((min + max) / 2, range.Value);
            Assert.AreEqual(1, eventCount);

            range.Value = (min + max) / 2;
            Assert.AreEqual((min + max) / 2, range.Value);
            Assert.AreEqual(1, eventCount);

            range.Value = max + 1;
            Assert.AreEqual(max, range.Value);
            Assert.AreEqual(2, eventCount);

            range.Value = max + 1;
            Assert.AreEqual(max, range.Value);
            Assert.AreEqual(2, eventCount);

            range.Value = min - 1;
            Assert.AreEqual(min, range.Value);
            Assert.AreEqual(3, eventCount);

            range.Value = min - 1;
            Assert.AreEqual(min, range.Value);
            Assert.AreEqual(3, eventCount);
        }
    }
}
