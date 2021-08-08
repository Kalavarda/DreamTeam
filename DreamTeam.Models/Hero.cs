using System;
using System.Collections.Generic;
using System.Diagnostics;
using DreamTeam.Models.Abstract;
using DreamTeam.Models.Skills;

namespace DreamTeam.Models
{
    [DebuggerDisplay("Hero ({Class})")]
    public class Hero : IFighter
    {
        private bool _isSelected;

        public HeroClass Class { get; }

        public Bounds Bounds { get; }

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

        private void Position_Changed()
        {
            PositionChanged?.Invoke(this);
        }
        
        public virtual IReadOnlyCollection<ISkill> Skills { get; } = new ISkill[]
        {
            new SimpleAttack(1f, "Простой удар", TimeSpan.FromSeconds(2), 1)
        };

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
