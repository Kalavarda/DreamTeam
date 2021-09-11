using System;
using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models.Abstract;
using DreamTeam.Utils;
using Kalavarda.Primitives.Geometry;
using Moq;
using NUnit.Framework;

namespace DreamTeam.Tests.Utils
{
    public class PathFinder_Tests
    {
        private readonly Mock<ICollisionDetector> _collisionDetector = new Mock<ICollisionDetector>();

        [Test]
        public void FindPath_Test()
        {
            var start = new PointF(-10, -10);
            var finish = new PointF(10, 10);
            var bounds = new RoundBounds(new PointF(-10, -10), 1);

            _collisionDetector
                .Setup(cd => cd.HasCollision(It.IsAny<BoundsF>(), It.IsAny<IReadOnlyCollection<BoundsF>>()))
                .Returns(new Func<BoundsF, bool>(b => b.DoesIntersect(new PointF(0, 0))));

            var pathFinder = new PathFinder(_collisionDetector.Object);

            var path = pathFinder.FindPath(start, start, bounds);
            Assert.AreEqual(0, path.Points.Count);

            path = pathFinder.FindPath(start, finish, bounds);
            Assert.AreEqual(finish, path.Points.Single());

            path = pathFinder.FindPath(start, finish, bounds);
            Assert.AreEqual(2, path.Points.Count);
            Assert.AreEqual(finish, path.Points.Last());
        }
    }
}
