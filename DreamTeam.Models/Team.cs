using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public class Team: IFightTeam
    {
        private readonly Hero[] _heroes;

        public IReadOnlyCollection<Hero> Heroes => _heroes;

        public Team()
        {
            _heroes = new [] {
                new Hero(HeroClass.Tank, this),
                new Hero(HeroClass.Healer, this),
                new Hero(HeroClass.MeleeDD, this),
                new Hero(HeroClass.RangeDD, this),
                new Hero(HeroClass.Support, this)
            };

            _heroes.First(h => h.Class == HeroClass.Tank).Position.Set(0, 0);
            _heroes.First(h => h.Class == HeroClass.Healer).Position.Set(0, 1);
            _heroes.First(h => h.Class == HeroClass.Support).Position.Set(0, -1);
            _heroes.First(h => h.Class == HeroClass.RangeDD).Position.Set(2, 0);
            _heroes.First(h => h.Class == HeroClass.MeleeDD).Position.Set(-1, 0);
        }

        public IReadOnlyCollection<IFighter> TeamMates => _heroes;
    }
}
