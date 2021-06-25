using System;

namespace DreamTeam.Models.Abstract
{
    public interface ISkill
    {
        string Name { get; }

        float MaxDistance { get; }

        TimeSpan Cooldown { get; }
    }

    public interface ITargetSkill: ISkill
    {
        bool CanUse(ISkilled source, ISelectable target);
    }

    public interface IAreaSkill: ISkill
    {
        bool CanUse(ISkilled source, Point position);
    }
}
