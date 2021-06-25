using System;
using System.Collections.Generic;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public abstract class MobBase: IPhysicalObject, IFighter, ISkilled
    {
        protected List<ISkill> _skills = new List<ISkill>();

        public abstract float Radius { get; }
        
        public abstract float Speed { get; }

        public Point Position { get; } = new Point();

        public event Action<IPhysicalObject> PositionChanged;

        public abstract Fractions Fraction { get; }
        
        public void Attack(IFighter enemy)
        {
            if (enemy == null) throw new ArgumentNullException(nameof(enemy));
        }

        protected MobBase()
        {
            Position.Changed += Position_Changed;
        }

        private void Position_Changed()
        {
            PositionChanged?.Invoke(this);
        }

        public IReadOnlyCollection<ISkill> Skills => _skills;
    }
}
