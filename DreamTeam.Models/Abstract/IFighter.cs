namespace DreamTeam.Models.Abstract
{
    public interface IFighter: ICreature, IPhysicalObject, ISkilled, ISelectable
    {
        IFightTeam Team { get; }

        /// <summary>
        /// Manual / AI
        /// </summary>
        bool ManualManaged { get; }
    }
}
