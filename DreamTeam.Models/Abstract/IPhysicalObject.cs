using System;
using Kalavarda.Primitives.Geometry;

namespace DreamTeam.Models.Abstract
{
    public interface IPhysicalObject: Kalavarda.Primitives.Abstract.IPhysicalObject
    {
        PointF Position { get; }

        public event Action<IPhysicalObject> PositionChanged;
    }
}