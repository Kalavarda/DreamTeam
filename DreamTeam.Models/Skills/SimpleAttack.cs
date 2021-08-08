using System;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models.Skills
{
    public class SimpleAttack: ITargetSkill
    {
        private readonly float _power;
        private readonly TimeLimiter _limiter = new TimeLimiter(TimeSpan.FromSeconds(1));

        public ITimeLimiter TimeLimiter => _limiter;

        public float MaxDistance { get; protected set; }

        public string Name { get; }

        public Change Use(ISelectable selectable)
        {
            return _limiter.Do(() =>
            {
                if (selectable is ICreature creature)
                {
                    creature.HP.Value -= _power;
                    return new Change(-_power, this);
                }

                throw new NotImplementedException();
            });
        }

        public SimpleAttack(float maxDistance, string name, TimeSpan coolddown, float power)
        {
            _power = power;
            Name = name;
            MaxDistance = maxDistance;
            _limiter.Interval = coolddown;
        }
    }
}
