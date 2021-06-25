namespace DreamTeam.Models.Abstract
{
    public interface IRelationDetector
    {
        /// <summary>
        /// Определяет отношение <see cref="from"/> к <see cref="to"/>
        /// </summary>
        Relation GetRelationTo(ICreature from, ICreature to);
    }
}
