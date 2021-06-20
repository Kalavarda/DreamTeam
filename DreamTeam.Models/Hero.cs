using System;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public class Hero : IPhysicalObject
    {
        public HeroClass Class { get; }

        public float Radius { get; } = 0.25f;
        
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
