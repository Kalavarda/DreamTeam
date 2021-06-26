using System;
using System.Collections.Generic;
using System.Diagnostics;
using DreamTeam.Models.Abstract;
using DreamTeam.Models.Skills;

namespace DreamTeam.Models
{
    [DebuggerDisplay("Hero ({Class})")]
    public class Hero : IFighter, ISelectable
    {
        private bool _isSelected;

        public HeroClass Class { get; }

        public Bounds Bounds { get; }

        public float Speed { get; } = 5000f / 3600f;

        public PointF Position { get; } = new PointF();

        public IFightTeam Team { get; }

        public bool ManualManaged => IsSelected;

        public event Action<IPhysicalObject> PositionChanged;

        public Hero(HeroClass heroClass, IFightTeam team)
        {
            Class = heroClass;
            Team = team ?? throw new ArgumentNullException(nameof(team));
            Bounds = new RoundBounds(Position, 0.25f);
            HP.Value = HP.Max;

            Position.Changed += Position_Changed;
        }

        private void Position_Changed()
        {
            PositionChanged?.Invoke(this);
        }

        public Fractions Fraction { get; } = Fractions.Heroes;
        
        public RangeF HP { get; } = new RangeF(0, 100);

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
        
        public IReadOnlyCollection<ISkill> Skills { get; } = new ISkill[] { new Bite(1f, TimeSpan.FromSeconds(2)) };
    }

    public enum HeroClass
    {
        Tank,
        Healer,
        MeleeDD,
        RangeDD,
        Support
    }
}
