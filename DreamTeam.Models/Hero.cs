using System;
using System.Collections.Generic;
using System.Diagnostics;
using DreamTeam.Models.Abstract;
using DreamTeam.Models.Skills;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Skills;
using ISkilled = DreamTeam.Models.Abstract.ISkilled;

namespace DreamTeam.Models
{
    [DebuggerDisplay("Hero ({Class})")]
    public class Hero : IFighter, ISkilledExt
    {
        private bool _isSelected;
        private ISelectable _target;

        public HeroClass Class { get; }

        public BoundsF Bounds { get; }

        public float Speed { get; } = 5000f / 3600f;

        public PointF Position { get; } = new PointF();
        
        public AngleF Direction { get; } = new AngleF();

        public IFightTeam Team { get; }

        public bool ManualManaged => IsSelected;

        public Fractions Fraction { get; } = Fractions.Heroes;

        public RangeF HP { get; } = new RangeF(0, 100);

        public bool IsAlive => !IsDead;
        
        public bool IsDead { get; private set; }

        public event Action<ICreature> Died;

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

        public event Action<IPhysicalObject> PositionChanged;

        public Hero(HeroClass heroClass, IFightTeam team)
        {
            Class = heroClass;
            Team = team ?? throw new ArgumentNullException(nameof(team));
            Bounds = new RoundBounds(Position, 0.25f);
            HP.Value = HP.Max;
            HP.ValueMin += HP_ValueMin;

            Position.Changed += Position_Changed;
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
        
        public virtual IReadOnlyCollection<ISkill> Skills { get; } = new ISkill[]
        {
            new SimpleAttack(1f, "Простой удар", TimeSpan.FromSeconds(2), 1)
        };

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

        public override string ToString()
        {
            return Class.ToString();
        }
    }

    public enum HeroClass
    {
        Tank,
        Healer,
        MeleeDD,
        RangeDD,
        Support
    }

    public class HealerHero: Hero, IHealer
    {
        public HealerHero(IFightTeam team) : base(HeroClass.Healer, team)
        {
        }

        public override IReadOnlyCollection<ISkill> Skills { get; } = new ISkill[]
        {
            new SimpleHeal(5f, TimeSpan.FromSeconds(5)),
        };
    }

    public class RangeDDHero: Hero
    {
        public RangeDDHero(IFightTeam team) : base(HeroClass.RangeDD, team)
        {
        }

        public override IReadOnlyCollection<ISkill> Skills { get; } = new ISkill[]
        {
            new SimpleAttack(30f, "Выстрел из лука", TimeSpan.FromSeconds(5), 5),
            new SimpleAttack(20f, "Сильный выстрел", TimeSpan.FromSeconds(15), 15),
        };
    }
}
