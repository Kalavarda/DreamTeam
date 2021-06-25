using System.Collections.Generic;

namespace DreamTeam.Models.Abstract
{
    public interface IFightTeam
    {
        IReadOnlyCollection<IFighter> TeamMates { get; }
    }
}
