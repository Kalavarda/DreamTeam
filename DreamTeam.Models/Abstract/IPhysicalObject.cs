using System;

namespace DreamTeam.Models.Abstract
{
    public interface IPhysicalObject
    {
        float Radius { get; }

        Point Position { get; }

        public event Action<IPhysicalObject> PositionChanged;
    }
}