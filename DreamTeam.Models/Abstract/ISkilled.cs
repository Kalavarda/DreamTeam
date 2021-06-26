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
                .Where(sk => sk.Cooldown == TimeSpan.Zero)
                .Select(sk => sk.MaxDistance)
                .ToArray();
            return readyDistances.Any()
                ? readyDistances.Max()
                : skilled.Skills.OrderBy(sk => sk.Cooldown).First().MaxDistance;
        }

        public static Change UseSkillTo(this ISkilled skilled, IFighter enemy)
        {
            if (enemy == null) throw new ArgumentNullException(nameof(enemy));

            foreach (var skill in skilled.Skills.OrderByDescending(sk => sk.MaxDistance))
            {
                if (skill is ITargetSkill tSkill)
                {
                    var selectable = (ISelectable)enemy;
                    return tSkill.Use(selectable);
                }

                if (skill is IAreaSkill aSkill)
                {
                    var position = ((IPhysicalObject)enemy).Position;
                    // aSkill.Use(position);
                    throw new NotImplementedException();
                }
            }

            return null;
        }

    }
}
