using System;
using DreamTeam.Models.Abstract;
using Kalavarda.Primitives.Skills;

namespace DreamTeam.Models
{
    public class Change
    {
        public float HpDiff { get; }

        public ISkill Skill { get; }

        public Change(float hpDiff, ISkill skill)
        {
            HpDiff = hpDiff;
            Skill = skill ?? throw new ArgumentNullException(nameof(skill));
        }
    }

    public class ChangeExt: Change
    {
        public IFighter Source { get; }

        public IFighter Target { get; }

        public DateTime Time { get; } = DateTime.Now;

        public ChangeExt(Change change, IFighter source, IFighter target): base(change.HpDiff, change.Skill)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }
    }
}
