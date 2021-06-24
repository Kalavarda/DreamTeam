using System;

namespace DreamTeam.Models.Abstract
{
    public interface IPhysicalObject
    {
        float Radius { get; }

        float Speed { get; }

        Point Position { get; }

        public event Action<IPhysicalObject> PositionChanged;
    }
}