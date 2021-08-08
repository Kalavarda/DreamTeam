using System;
using System.Collections.Generic;

namespace DreamTeam.Models.Abstract
{
    public interface IFight
    {
        IReadOnlyCollection<IFighter> Fighters { get; }

        IFightStatistics Statistics { get; }
    }

    public interface IFightsHistory
    {
        event Action Changed;

        IReadOnlyCollection<IFight> Fights { get; }
    }

    public interface IFightsHistoryExt: IFightsHistory
    {
        void Add(IFight fight);
    }

    public interface IFightStatistics
    {
        IReadOnlyCollection<ChangeExt> Changes { get; }

        event Action Changed;
    }

    public interface IFightStatisticsExt: IFightStatistics
    {
        void Add(ChangeExt changeExt);
    }
}