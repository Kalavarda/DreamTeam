namespace DreamTeam.Models.Abstract
{
    public interface ISkill
    {
        string Name { get; }

        float MaxDistance { get; }

        ITimeLimiter TimeLimiter { get; }
    }

    public interface ITargetSkill: ISkill
    {
        //bool CanUse(ISkilled source, ISelectable target);

        Change Use(ISelectable selectable);
    }

    public interface IAreaSkill: ISkill
    {
        //bool CanUse(ISkilled source, PointF position);
    }
}
