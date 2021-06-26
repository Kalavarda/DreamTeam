using System;
using System.Collections.Generic;
using System.Diagnostics;
using DreamTeam.Models.Abstract;
using DreamTeam.Models.Skills;

namespace DreamTeam.Models
{
    [DebuggerDisplay("Hero ({Class})")]
    public class Hero : IPhysicalObject, IFighter, ISelectable, ISkilled
    {
        private bool _isSelected;

        public HeroClass Class { get; }

        public float Radius { get; } = 0.25f;
        
        public float Speed { get; } = 5000f / 3600f;

        public Point Position { get; } = new Point();

        public IFightTeam Team { get; }

        public bool ManualManaged => IsSelected;

        public event Action<IPhysicalObject> PositionChanged;

        public Hero(HeroClass heroClass, IFightTeam team)
        {
            Class = heroClass;
            Team = team ?? throw new ArgumentNullException(nameof(team));

            Position.Changed += Position_Changed;
        }

        private void Position_Changed()
        {
            PositionChanged?.Invoke(this);
        }

        public Fractions Fraction { get; } = Fractions.Heroes;

        public void Attack(IFighter enemy)
        {
            if (enemy == null) throw new ArgumentNullException(nameof(enemy));
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
        
        public IReadOnlyCollection<ISkill> Skills { get; } = new ISkill[] { new Bite(1) };
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
