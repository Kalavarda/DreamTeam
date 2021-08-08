using System;
using System.Collections.Generic;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public abstract class MobBase: IFighter
    {
        protected List<ISkill> _skills = new List<ISkill>();
        private bool _isSelected;

        public abstract Bounds Bounds { get; }

        public abstract float Speed { get; }

        public PointF Position { get; } = new PointF();
        
        public AngleF Direction { get; } = new AngleF();

        public event Action<IPhysicalObject> PositionChanged;

        public abstract Fractions Fraction { get; }
        
        public RangeF HP { get; } = new RangeF(0, 100);
        
        public event Action<ICreature> Died;

        public bool IsAlive => !IsDead;

        public bool IsDead { get; private set; }

        public IFightTeam Team => null;
        
        public bool ManualManaged => false;

        protected MobBase()
        {
            Position.Changed += Position_Changed;
            HP.SetMax();
            HP.ValueMin += HP_ValueMin;
        }

        private void HP_ValueMin(RangeF hp)
        {
            IsDead = true;
            Died?.Invoke(this);
        }

        private void Position_Changed()
        {
            PositionChanged?.Invoke(this);
        }

        public IReadOnlyCollection<ISkill> Skills => _skills;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected == value)
                    return;

                _isSelected = value;
                SelectedChanged?.Invoke(this);
            }
        }

        public event Action<ISelectable> SelectedChanged;
    }
}
