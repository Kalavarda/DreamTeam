using System;
using DreamTeam.Models.Skills;

namespace DreamTeam.Models.Mobs
{
    public class Bug: MobBase
    {
        public override Bounds Bounds { get; }

        public override float Speed { get; } = 5000f / 3600f;
        
        public override Fractions Fraction => Fractions.Animals;

        public Bug()
        {
            Bounds = new RoundBounds(Position, 0.05f);
            _skills.Add(new Bite(0.1f, TimeSpan.FromSeconds(0.5)));
        }
    }
}
