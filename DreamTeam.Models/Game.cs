namespace DreamTeam.Models
{
    public class Game
    {
        public Team Team { get; } = new Team();

        public Environment Environment { get; } = new Environment();
    }
}
