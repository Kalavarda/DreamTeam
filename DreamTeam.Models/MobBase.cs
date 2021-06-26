using System;
using System.Collections.Generic;
using System.Linq;
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

        public IFightTeam Team => null;
        
        public bool ManualManaged => false;

        public void Attack(IFighter enemy)
        {
            if (enemy == null) throw new ArgumentNullException(nameof(enemy));

            foreach (var skill in _skills.OrderByDescending(sk => sk.MaxDistance))
            {
                if (skill is ITargetSkill tSkill)
                {
                    var selectable = (ISelectable) enemy;
                    if (tSkill.CanUse(this, selectable))
                    {
                        // tSkill.Use(selectable);
                        return;
                    }
                }

                if (skill is IAreaSkill aSkill)
                {
                    var position = ((IPhysicalObject) enemy).Position;
                    if (aSkill.CanUse(this, position))
                    {
                        // aSkill.Use(position);
                        return;
                    }
                }
            }
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
