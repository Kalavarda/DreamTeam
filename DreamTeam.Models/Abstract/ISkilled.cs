using System;
using System.Linq;

namespace DreamTeam.Models.Abstract
{
    public interface ISkilled: Kalavarda.Primitives.Skills.ISkilled
    {
        event Action<ISkilled, ISelectable, ISelectable> TargetChanged;

        ISelectable Target { get; }
    }

    public interface ISkilledExt: ISkilled
    {
        ISelectable Target { get; set; }
    }

    public static class SkilledExtensions
    {
        public static Change UseSkillTo(this ISkilled skilled, IFighter target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            foreach (var skill in skilled.Skills.OrderByDescending(sk => sk.MaxDistance))
            {
                if (skill is ITargetSkill tSkill)
                {
                    var selectable = (ISelectable)target; // TODO: возможно, имеет смысл вынести из цикла
                    var change = tSkill.Use(selectable);
                    if (change != null)
                        return change;
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
