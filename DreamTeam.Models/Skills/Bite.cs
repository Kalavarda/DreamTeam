using System;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models.Skills
{
    public class Bite: SkillBase, ITargetSkill
    {
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
            if (selectable is ICreature creature)
            {
                creature.HP.Value -= 1.5f;
                return new Change(-1.5f);
            }
            else
                throw new NotImplementedException();
        }

        public Bite(float maxDistance, TimeSpan coolddown)
        {
            MaxDistance = maxDistance;
            _limiter.Interval = coolddown;
        }
    }
}
