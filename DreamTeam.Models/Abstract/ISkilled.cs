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
    }
}
