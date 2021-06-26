using System;

namespace DreamTeam.Models
{
    public abstract class Bounds
    {
        public abstract bool Intersect(Point p);

        public abstract float Width { get; }

        public abstract float Height { get; }
    }

    public class RoundBounds: Bounds
    {
        public Point Center { get; }

        public float Radius { get; }

        public RoundBounds(Point center, float radius)
        {
            Center = center ?? throw new ArgumentNullException(nameof(center));
            Radius = radius;
        }

        public override bool Intersect(Point p)
        {
            return Center.DistanceTo(p) <= Radius;
        }

        public override float Width => 2 * Radius;

        public override float Height => 2 * Radius;
    }

    public class RectBounds: Bounds
    {
        public override bool Intersect(Point p)
        {
            throw new NotImplementedException();
        }

        public override float Width { get; } = default;

        public override float Height { get; } = default;
    }
}
