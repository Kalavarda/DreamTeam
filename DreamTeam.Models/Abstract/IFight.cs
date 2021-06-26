using System.Collections.Generic;

namespace DreamTeam.Models.Abstract
{
    public interface IFight
    {
        IReadOnlyCollection<IFighter> Fighters { get; }
    }
}