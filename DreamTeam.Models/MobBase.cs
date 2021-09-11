using System;
using System.Collections.Generic;
using DreamTeam.Models.Abstract;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Skills;
using ISkilled = DreamTeam.Models.Abstract.ISkilled;

namespace DreamTeam.Models
{
    public abstract class MobBase: IFighter, ISkilledExt
    {
        protected List<ISkill> _skills = new List<ISkill>();
        private bool _isSelected;
        private ISelectable _target;

        public abstract BoundsF Bounds { get; }

        public abstract float Speed { get; }

        public PointF Position { get; } = new PointF();
        
        public AngleF Direction { get; } = new AngleF();

        public event Action<IPhysicalObject> PositionChanged;

        public abstract Fractions Fraction { get; }
        
        public RangeF HP { get; } = new RangeF(0, 50);
        
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

        private void Position_Changed(PointF p)
        {
            PositionChanged?.Invoke(this);
        }

        public IReadOnlyCollection<ISkill> Skills => _skills;

        public event Action<ISkilled, ISelectable, ISelectable> TargetChanged;

        public ISelectable Target
        {
            get => _target;
            set
            {
                if (_target == value)
                    return;

                if (_target is ICreature creature1)
                    creature1.Died -= TargetDied;

                var oldValue = _target;
                _target = value;

                if (_target is ICreature creature2)
                    creature2.Died += TargetDied;

                TargetChanged?.Invoke(this, oldValue, value);
            }
        }

        private void TargetDied(ICreature creature)
        {
            Target = null;
        }

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
