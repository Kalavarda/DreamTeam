using System;
using DreamTeam.Models.Abstract;
using PointF = DreamTeam.Models.PointF;

namespace DreamTeam.Utils
{
    public class PathFinder: IPathFinder
    {
        private readonly ICollisionDetector _collisionDetector;

        public PathFinder(ICollisionDetector collisionDetector)
        {
            _collisionDetector = collisionDetector ?? throw new ArgumentNullException(nameof(collisionDetector));
        }

        public Path FindPath(PointF @from, PointF to)
        {
            return new Path(@from, new[] { to });
        }
    }
}
