using System;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models.Skills
{
    public class Bite: ITargetSkill
    {
        private readonly TimeLimiter _limiter = new TimeLimiter(TimeSpan.FromSeconds(1));

        public ITimeLimiter TimeLimiter => _limiter;

        public float MaxDistance { get; protected set; }

        public bool CanUse(ISkilled source, ISelectable target)
        {
            if (source is IPhysicalObject s)
                if (target is IPhysicalObject t)
                    return s.Position.DistanceTo(t.Position) <= MaxDistance;

            return false;
        }

        public string Name => "Укус";

        public Change Use(ISelectable selectable)
        {
            return _limiter.Do(() =>
            {
                if (selectable is ICreature creature)
                {
                    creature.HP.Value -= 2f;
                    return new Change(-2f, this);
                }

                throw new NotImplementedException();
            });
        }

        public Bite(float maxDistance, TimeSpan coolddown)
        {
            MaxDistance = maxDistance;
            _limiter.Interval = coolddown;
        }
    }
}
