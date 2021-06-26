using System;

namespace DreamTeam.Models.Abstract
{
    public interface IPhysicalObject
    {
        Bounds Bounds { get; }

        float Speed { get; }

        Point Position { get; }

        public event Action<IPhysicalObject> PositionChanged;
    }
}