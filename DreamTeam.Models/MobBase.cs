using System;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public abstract class MobBase: IPhysicalObject, ICreature
    {
        public abstract float Radius { get; }
        
        public abstract float Speed { get; }

        public Point Position { get; } = new Point();

        public event Action<IPhysicalObject> PositionChanged;

        public abstract Fractions Fraction { get; }

        protected MobBase()
        {
            Position.Changed += Position_Changed;
        }

        private void Position_Changed()
        {
            PositionChanged?.Invoke(this);
        }
    }
}
