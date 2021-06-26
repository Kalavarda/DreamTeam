using System;

namespace DreamTeam.Models.Skills
{
    public abstract class SkillBase
    {
        protected readonly TimeLimiter _limiter = new TimeLimiter(TimeSpan.FromSeconds(1));

        public TimeSpan Cooldown => _limiter.Remain;

        public float MaxDistance { get; protected set; }
    }
}
