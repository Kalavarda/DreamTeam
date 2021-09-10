using System;
using DreamTeam.Models.Abstract;
using Kalavarda.Primitives;

namespace DreamTeam.Models.Skills
{
    public class SimpleHeal: ITargetSkill
    {
        private readonly TimeLimiter _limiter = new TimeLimiter(TimeSpan.FromSeconds(1));

        public float MaxDistance { get; protected set; }

        public ITimeLimiter TimeLimiter => _limiter;

        public string Name { get; } = "Зелёнка";

        public Change Use(ISelectable selectable)
        {
            if (selectable == null) throw new ArgumentNullException(nameof(selectable));

            return _limiter.Do(() =>
            {
                if (selectable is ICreature creature)
                {
                    creature.HP.Value += 5f;
                    return new Change(5f, this);
                }

                throw new NotImplementedException();
            });
        }

        public SimpleHeal(float maxDistance, TimeSpan coolddown)
        {
            MaxDistance = maxDistance;
            _limiter.Interval = coolddown;
        }
    }
}
