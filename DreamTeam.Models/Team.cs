using System.Collections.Generic;
using System.Linq;

namespace DreamTeam.Models
{
    public class Team
    {
        private readonly Hero[] _heroes = {
            new Hero(HeroClass.Tank),
            new Hero(HeroClass.Healer),
            new Hero(HeroClass.MeleeDD),
            new Hero(HeroClass.RangeDD),
            new Hero(HeroClass.Support)
        };

        public IReadOnlyCollection<Hero> Heroes => _heroes;

        public Team()
        {
            _heroes.First(h => h.Class == HeroClass.Tank).Position.Set(0, 0);
            _heroes.First(h => h.Class == HeroClass.Healer).Position.Set(0, 1);
            _heroes.First(h => h.Class == HeroClass.Support).Position.Set(0, -1);
            _heroes.First(h => h.Class == HeroClass.RangeDD).Position.Set(2, 0);
            _heroes.First(h => h.Class == HeroClass.MeleeDD).Position.Set(-1, 0);
        }
    }
}
