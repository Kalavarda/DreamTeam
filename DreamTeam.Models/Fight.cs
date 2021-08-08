using System;
using System.Collections.Generic;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public class Fight : IFight
    {
        private readonly List<IFighter> _fighters = new List<IFighter>();
        private readonly FightStatistics _fightStatistics = new FightStatistics();

        public IReadOnlyCollection<IFighter> Fighters => _fighters;
        
        public IFightStatistics Statistics => _fightStatistics;

        public Fight(IFighter source, IFighter target)
        {
            Add(source);
            Add(target);

            if (source.Team != null)
                foreach (var teamMate in source.Team.TeamMates)
                    Add(teamMate);

            if (target.Team != null)
                foreach (var teamMate in target.Team.TeamMates)
                    Add(teamMate);
        }

        public void Add(IFighter fighter)
        {
            if (fighter == null) throw new ArgumentNullException(nameof(fighter));

            if (fighter.IsDead)
                return;

            if (_fighters.Contains(fighter))
                return;

            _fighters.Add(fighter);
        }

        public void UseSkill(IFighter fighter, IFighter target)
        {
            var change = fighter.UseSkillTo(target);
            if (change != null)
                _fightStatistics.Add(new ChangeExt(change, fighter, target));
        }
    }

    public class FightsHistory : IFightsHistoryExt
    {
        public const int MaxHistoryCount = 10;

        private readonly Queue<IFight> _history = new Queue<IFight>();

        public IReadOnlyCollection<IFight> Fights => _history;

        public event Action Changed;

        public void Add(IFight fight)
        {
            if (fight == null) throw new ArgumentNullException(nameof(fight));

            _history.Enqueue(fight);

            while (_history.Count > MaxHistoryCount)
                _history.Dequeue();

            Changed?.Invoke();
        }
    }
}
