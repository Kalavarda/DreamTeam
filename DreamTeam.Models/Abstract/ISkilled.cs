using System;
using System.Collections.Generic;
using System.Linq;

namespace DreamTeam.Models.Abstract
{
    public interface ISkilled
    {
        IReadOnlyCollection<ISkill> Skills { get; }
    }

    public static class SkilledExtensions
    {
        public static float GetMaxSkillDistance(this ISkilled skilled)
        {
            var readyDistances = skilled.Skills
                .Where(sk => sk.TimeLimiter.Remain == TimeSpan.Zero)
                .Select(sk => sk.MaxDistance)
                .ToArray();
            return readyDistances.Any()
                ? readyDistances.Max()
                : skilled.Skills.OrderBy(sk => sk.TimeLimiter.Remain).First().MaxDistance;
        }

        public static Change UseSkillTo(this ISkilled skilled, IFighter target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            foreach (var skill in skilled.Skills.OrderByDescending(sk => sk.MaxDistance))
            {
                if (skill is ITargetSkill tSkill)
                {
                    var selectable = (ISelectable)target;
                    return tSkill.Use(selectable);
                }

                if (skill is IAreaSkill aSkill)
                {
                    var position = ((IPhysicalObject)target).Position;
                    // aSkill.Use(position);
                    throw new NotImplementedException();
                }
            }

            return null;
        }

    }
}
