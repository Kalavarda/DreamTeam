using System;
using System.Linq;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using Environment = DreamTeam.Models.Environment;

namespace DreamTeam
{
    public class CollisionDetector: ICollisionDetector
    {
        private readonly Team _team;
        private readonly Environment _environment;

        public CollisionDetector(Team team, Environment environment)
        {
            _team = team ?? throw new ArgumentNullException(nameof(team));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public bool HasCollision(Bounds bounds)
        {
            if (bounds == null) throw new ArgumentNullException(nameof(bounds));

            return _team.Heroes.Select(h => h.Bounds)
                .Union(_environment.Mobs.Select(m => m.Bounds))
                .Where(b => b != bounds)
                .Any(b => b.DoesIntersect(bounds));
        }
    }
}
