using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public class Game
    {
        public HeroTeam Team { get; } = new HeroTeam();

        public Environment Environment { get; } = new Environment();

        public ISelectable Selected
        {
            get
            {
                foreach (var hero in Team.Heroes)
                    if (hero.IsSelected)
                        return hero;

                return null;
            }
        }
    }
}
