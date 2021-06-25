using System.Collections.Generic;

namespace DreamTeam.Models.Abstract
{
    public interface ISkilled
    {
        IReadOnlyCollection<ISkill> Skills { get; }
    }
}
