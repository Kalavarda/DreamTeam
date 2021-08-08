using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public class HeroTeam: IFightTeam
    {
        private readonly Hero[] _heroes;

        public IReadOnlyCollection<Hero> Heroes => _heroes;

        public Hero Tank { get; }

        public Hero Healer { get; }

        public Hero Support { get; }

        public Hero RangeDD { get; }

        public HeroTeam()
        {
            Tank = new Hero(HeroClass.Tank, this);
            Healer = new HealerHero(this);
            Support = new Hero(HeroClass.Support, this);
            RangeDD = new RangeDDHero(this);

            _heroes = new [] {
                Tank,
                Healer, 
                new Hero(HeroClass.MeleeDD, this),
                RangeDD,
                Support
            };

            Tank.Position.Set(0, 0);
            Healer.Position.Set(0, 1);
            Support.Position.Set(0, -1);
            _heroes.First(h => h.Class == HeroClass.RangeDD).Position.Set(2, 0);
            _heroes.First(h => h.Class == HeroClass.MeleeDD).Position.Set(-1, 0);
        }

        public IReadOnlyCollection<IFighter> TeamMates => _heroes;
    }
}
