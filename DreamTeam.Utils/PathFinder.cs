using System;
using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models.Abstract;
using Kalavarda.Primitives.Geometry;

namespace DreamTeam.Utils
{
    public class PathFinder: IPathFinder
    {
        private readonly ICollisionDetector _collisionDetector;
        private readonly Random _random = new Random();

        public PathFinder(ICollisionDetector collisionDetector)
        {
            _collisionDetector = collisionDetector ?? throw new ArgumentNullException(nameof(collisionDetector));
        }

        public Path FindPath(PointF @from, PointF to, BoundsF bounds)
        {
            if (@from == null) throw new ArgumentNullException(nameof(@from));
            if (to == null) throw new ArgumentNullException(nameof(to));
            if (bounds == null) throw new ArgumentNullException(nameof(bounds));

            if (@from.Equals(to)) // странный случай, такого не должно быть по идее
                return new Path(@from, new PointF[0]);

            var copy = bounds.DeepClone();
            var ignoreBounds = new[] { bounds };

            if (LineIsFree(@from, to, copy, ignoreBounds))
                return new Path(@from, new[] { to });

            for (var partsCount = 2; partsCount < 6; partsCount++)
                for (var r = 0; r < partsCount; r++) // сколько попыток найти случайный обход
                {
                    var path = FindPath(@from, to, copy, partsCount, ignoreBounds);
                    if (path != null)
                        return path;
                }

            return null;
        }

        private Path FindPath(PointF @from, PointF to, BoundsF bounds, int partsCount, IReadOnlyCollection<BoundsF> ignoreBounds)
        {
            var partLength = @from.DistanceTo(to) / partsCount;
            var a = @from.AngleTo(to);
            
            var points = new PointF[1 + partsCount];
            points[0] = @from;
            points[^1] = to;
            for (var i = 1; i < points.Length - 1; i++)
            {
                var x = @from.X + (partLength * i) * MathF.Cos(a);
                var y = @from.Y + (partLength * i) * MathF.Sin(a);

                var rotA = _random.Next(2) == 0
                    ? a + MathF.PI / 2
                    : a - MathF.PI / 2;
                var shiftLen = 0.5f + (float)_random.NextDouble();
                shiftLen *= partLength;
                var dx = shiftLen * MathF.Cos(rotA);
                var dy = shiftLen * MathF.Sin(rotA);

                points[i] = new PointF(x + dx, y + dy);
            }

            for (var i = 0; i < points.Length - 1; i++)
                if (!LineIsFree(points[i], points[i + 1], bounds, ignoreBounds))
                    return null;

            return new Path(@from, points.Skip(1).ToArray());
        }

        /// <summary>
        /// Свободен ли для <see cref="bounds"/> путь между двумя точками?
        /// </summary>
        private bool LineIsFree(PointF p1, PointF p2, BoundsF bounds, IReadOnlyCollection<BoundsF> ignoreBounds)
        {
            var distance = p1.DistanceTo(p2);
            var a = p1.AngleTo(p2);
            var step = (bounds.Size.Width + bounds.Size.Height) / 2;

            var d = 0f;
            while (d < distance)
            {
                d += step;
                var x = p1.X + d * MathF.Cos(a);
                var y = p1.Y + d * MathF.Sin(a);
                bounds.Position.Set(x, y);
                if (_collisionDetector.HasCollision(bounds, ignoreBounds))
                    return false;
            }

            return true;
        }
    }
}
