﻿using System;
using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using Environment = DreamTeam.Models.Environment;

namespace DreamTeam.Utils
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

        public bool HasCollision(Bounds bounds, IReadOnlyCollection<Bounds> ignoreBounds = null)
        {
            if (bounds == null) throw new ArgumentNullException(nameof(bounds));

            return _team.Heroes.Where(h => h.IsAlive).Select(h => h.Bounds)
                .Union(_environment.Mobs.Where(m => m.IsAlive).Select(m => m.Bounds))
                .Where(b => b != bounds)
                .Where(b => ignoreBounds != null &&!ignoreBounds.Contains(b))
                .Any(b => b.DoesIntersect(bounds));
        }
    }
}
