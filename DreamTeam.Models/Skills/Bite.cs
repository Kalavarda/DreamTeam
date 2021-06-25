using System;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models.Skills
{
    public class Bite: ITargetSkill
    {
        private readonly TimeLimiter _limiter = new TimeLimiter(TimeSpan.FromSeconds(2.5));

        public string Name => "Укус";
        
        public float MaxDistance { get; }

        public TimeSpan Cooldown => _limiter.Remain;

        public bool CanUse(ISkilled source, ISelectable target)
        {
            if (source is IPhysicalObject s)
                if (target is IPhysicalObject t)
                    return s.Position.DistanceTo(t.Position) <= MaxDistance;

            return false;
        }

        public Bite(float maxDistance)
        {
            MaxDistance = maxDistance;
        }
    }
}
