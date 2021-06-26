namespace DreamTeam.Models.Abstract
{
    public interface IFighter: ICreature, IPhysicalObject, ISkilled
    {
        IFightTeam Team { get; }

        /// <summary>
        /// Manual / AI
        /// </summary>
        bool ManualManaged { get; }

        void Attack(IFighter enemy);
    }
}
