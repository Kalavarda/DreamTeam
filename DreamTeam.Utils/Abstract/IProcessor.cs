namespace DreamTeam.Utils.Abstract
{
    public interface IProcessor
    {
        void Add(IProcess process, bool stopIncompatible = true);
    }
}