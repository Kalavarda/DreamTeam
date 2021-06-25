namespace DreamTeam.Models.Abstract
{
    public interface IFighter: ICreature
    {
        IFightTeam Team { get; }

        void Attack(IFighter enemy);
    }
}
