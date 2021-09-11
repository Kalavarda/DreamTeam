using System;
using DreamTeam.Models.Skills;
using Kalavarda.Primitives.Geometry;

namespace DreamTeam.Models.Mobs
{
    public class Bug: MobBase
    {
        public override BoundsF Bounds { get; }

        public override float Speed { get; } = 5000f / 3600f;
        
        public override Fractions Fraction => Fractions.Animals;

        public Bug()
        {
            Bounds = new RoundBounds(Position, 0.05f);
            _skills.Add(new Bite(0.6f, TimeSpan.FromSeconds(0.5)));
        }
    }
}
