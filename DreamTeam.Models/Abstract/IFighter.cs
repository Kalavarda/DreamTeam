namespace DreamTeam.Models.Abstract
{
    public interface IFighter: ICreature
    {
        void Attack(IFighter enemy);
    }
}
