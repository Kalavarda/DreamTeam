using System;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public class Hero : IPhysicalObject, ICreature, ISelectable
    {
        private bool _isSelected;

        public HeroClass Class { get; }

        public float Radius { get; } = 0.25f;
        
        public float Speed { get; } = 5000f / 3600f;

        public Point Position { get; } = new Point();
        
        public event Action<IPhysicalObject> PositionChanged;

        public Hero(HeroClass heroClass)
        {
            Class = heroClass;

            Position.Changed += Position_Changed;
        }

        private void Position_Changed()
        {
            PositionChanged?.Invoke(this);
        }

        public Fractions Fraction { get; } = Fractions.Heroes;

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

    public enum HeroClass
    {
        Tank,
        Healer,
        MeleeDD,
        RangeDD,
        Support
    }
}
