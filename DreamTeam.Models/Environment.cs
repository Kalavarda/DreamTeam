using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models.Mobs;

namespace DreamTeam.Models
{
    public class Environment
    {
        private readonly ICollection<MobBase> _mobs = new List<MobBase>();

        public IReadOnlyCollection<MobBase> Mobs => _mobs.ToArray();

        public Environment()
        {
            var bug1 = new Bug();
            bug1.Position.Set(4, 4);
            _mobs.Add(bug1);

            var bug2 = new Bug();
            bug2.Position.Set(-5, -5);
            _mobs.Add(bug2);
        }
    }
}
