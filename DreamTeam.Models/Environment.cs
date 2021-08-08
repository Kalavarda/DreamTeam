using System;
using System.Collections.Generic;
using DreamTeam.Models.Mobs;

namespace DreamTeam.Models
{
    public class Environment
    {
        private readonly List<MobBase> _mobs = new List<MobBase>();

        public IReadOnlyCollection<MobBase> Mobs => _mobs;

        public event Action<MobBase> MobAdded;

        public void Add(MobBase mob)
        {
            if (mob == null) throw new ArgumentNullException(nameof(mob));
            _mobs.Add(mob);
            MobAdded?.Invoke(mob);
        }

        public Environment()
        {
            var bug1 = new Bug();
            bug1.Position.Set(4, 4);
            Add(bug1);

            var bug2 = new Bug();
            bug2.Position.Set(-5, -5);
            Add(bug2);
        }
    }
}
