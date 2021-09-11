using Kalavarda.Primitives.Skills;

namespace DreamTeam.Models.Abstract
{
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
