using System;
using System.Linq;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Utils
{
    public class PriorityTargetDetector: IPriorityTargetDetector
    {
        private readonly IFight _fight;
        private readonly IRelationDetector _relationDetector;

        public PriorityTargetDetector(IFight fight, IRelationDetector relationDetector)
        {
            _fight = fight ?? throw new ArgumentNullException(nameof(fight));
            _relationDetector = relationDetector ?? throw new ArgumentNullException(nameof(relationDetector));
        }

        public IFighter GetPriorityTarget(IFighter fighter)
        {
            if (fighter == null) throw new ArgumentNullException(nameof(fighter));

            if (fighter is IHealer healer)
                return healer.Team.TeamMates
                    .Where(tm => tm.IsAlive)
                    .OrderBy(tm => tm.HP.ValueN)
                    .FirstOrDefault();

            var enemies = _fight.Fighters
                .Where(f => f.IsAlive)
                .Where(f => _relationDetector.GetRelationTo(fighter, f) == Relation.Enemy);

            // TODO: вычислить aggro

            return enemies.FirstOrDefault();
        }
    }
}
