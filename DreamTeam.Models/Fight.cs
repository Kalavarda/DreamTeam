using System;
using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public class Fight : IFight
    {
        private readonly IRelationDetector _relationDetector;
        private readonly List<IFighter> _fighters = new List<IFighter>();

        public IReadOnlyCollection<IFighter> Fighters => _fighters;

        public Fight(IFighter source, IFighter target, IRelationDetector relationDetector)
        {
            _relationDetector = relationDetector ?? throw new ArgumentNullException(nameof(relationDetector));

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

            if (_fighters.Contains(fighter))
                return;

            _fighters.Add(fighter);
        }

        public IFighter GetPriorityTarget(IFighter fighter)
        {
            if (fighter == null) throw new ArgumentNullException(nameof(fighter));

            var enemies = Fighters.Where(f => _relationDetector.GetRelationTo(fighter, f) == Relation.Enemy);
            
            // TODO: вычислить aggro

            return enemies.FirstOrDefault();
        }

        public void UseSkill(IFighter fighter, IFighter target)
        {
            var change = fighter.UseSkillTo(target);
        }
    }
}
