using System;
using Kalavarda.Primitives.Geometry;

namespace DreamTeam.Models.Abstract
{
    public interface IPhysicalObject
    {
        BoundsF Bounds { get; }

        float Speed { get; }

        PointF Position { get; }

        /// <summary>
        /// Направление (угол поворота)
        /// </summary>
        AngleF Direction { get; }

        public event Action<IPhysicalObject> PositionChanged;
    }
}