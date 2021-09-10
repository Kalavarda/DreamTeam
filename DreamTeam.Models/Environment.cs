using System;
using System.Collections.Generic;
using DreamTeam.Models.Abstract;
using DreamTeam.Models.Mobs;

namespace DreamTeam.Models
{
    public class Environment
    {
        private readonly List<MobBase> _mobs = new List<MobBase>();

        public IReadOnlyCollection<MobBase> Mobs => _mobs;

        public event Action<MobBase> MobAdded;
        public event Action<MobBase> MobRemoved;

        public void Add(MobBase mob)
        {
            if (mob == null) throw new ArgumentNullException(nameof(mob));
            _mobs.Add(mob);
            mob.Died += Mob_Died;
            MobAdded?.Invoke(mob);
        }

        private void Mob_Died(ICreature creature)
        {
            var mob = (MobBase)creature;
            creature.Died -= Mob_Died;
            _mobs.Remove(mob);
            MobRemoved?.Invoke(mob);
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
