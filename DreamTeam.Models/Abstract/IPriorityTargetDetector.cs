namespace DreamTeam.Models.Abstract
{
    public interface IPriorityTargetDetector
    {
        /// <summary>
        /// Определяет приоритетную цель для <see cref="fighter"/>
        /// </summary>
        IFighter GetPriorityTarget(IFighter fighter);
    }
}
