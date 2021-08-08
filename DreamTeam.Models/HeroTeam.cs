using System;
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

        public Hero MeleeDD { get; }

        public Hero SelectedHero
        {
            get
            {
                return _heroes.FirstOrDefault(h => h.IsSelected);
            }
        }

        public event Action<Hero, Hero> SelectedHeroChanged;

        public HeroTeam()
        {
            Tank = new Hero(HeroClass.Tank, this) { IsSelected = true };
            Healer = new HealerHero(this);
            Support = new Hero(HeroClass.Support, this);
            RangeDD = new RangeDDHero(this);
            MeleeDD = new Hero(HeroClass.MeleeDD, this);

            _heroes = new [] {
                Tank,
                Healer,
                MeleeDD,
                RangeDD,
                Support
            };

            Tank.Position.Set(0, 0);
            Healer.Position.Set(0, 1);
            Support.Position.Set(0, -1);
            RangeDD.Position.Set(2, 0);
            MeleeDD.Position.Set(-1, 0);
        }

        public IReadOnlyCollection<IFighter> TeamMates => _heroes;

        public void Select(HeroClass heroClass)
        {
            var prev = SelectedHero;
            foreach (var hero in _heroes)
                hero.IsSelected = hero.Class == heroClass;
            SelectedHeroChanged?.Invoke(prev, SelectedHero);
        }
    }
}
