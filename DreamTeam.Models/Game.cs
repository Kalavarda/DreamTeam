using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public class Game
    {
        public Team Team { get; } = new Team();

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
