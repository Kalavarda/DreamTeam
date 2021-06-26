using System;

namespace DreamTeam.Models
{
    public abstract class Bounds
    {
        public Point Center { get; }

        public abstract bool DoesIntersect(Point p);

        public abstract bool DoesIntersect(Bounds b);

        public abstract float Width { get; }

        public abstract float Height { get; }

        protected Bounds(Point center)
        {
            Center = center ?? throw new ArgumentNullException(nameof(center));
        }
    }

    public class RoundBounds: Bounds
    {
        public float Radius { get; }

        public RoundBounds(Point center, float radius): base(center)
        {
            Radius = radius;
        }

        public override bool DoesIntersect(Point p)
        {
            return Center.DistanceTo(p) <= Radius;
        }

        public override bool DoesIntersect(Bounds b)
        {
            if (b == null) throw new ArgumentNullException(nameof(b));

            if (b is RoundBounds round)
                return Center.DistanceTo(round.Center) < Radius + round.Radius;

            throw new NotImplementedException();
        }

        public override float Width => 2 * Radius;

        public override float Height => 2 * Radius;
    }

    public class RectBounds: Bounds
    {
        public override bool DoesIntersect(Point p)
        {
            throw new NotImplementedException();
        }

        public override bool DoesIntersect(Bounds b)
        {
            throw new NotImplementedException();
        }

        public override float Width { get; } = default;

        public override float Height { get; } = default;

        public RectBounds(Point center) : base(center)
        {
        }
    }
}
