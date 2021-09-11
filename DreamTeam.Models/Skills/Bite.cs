using System;
using DreamTeam.Models.Abstract;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Process;

namespace DreamTeam.Models.Skills
{
    public class Bite: ITargetSkill
    {
        private readonly TimeLimiter _limiter = new TimeLimiter(TimeSpan.FromSeconds(1));

        public ITimeLimiter TimeLimiter => _limiter;

        public IProcess Use(Kalavarda.Primitives.Skills.ISkilled initializer)
        {
            throw new NotImplementedException();
        }

        public float MaxDistance { get; protected set; }

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
