using System;
using System.Collections.Generic;

namespace DreamTeam.Utils.Abstract
{
    public interface IProcess
    {
        event Action<IProcess> Finish;
        
        void Process(TimeSpan delta);

        /// <summary>
        /// Определяет процессы, которые должны быть отменены с добавлением этого процесса
        /// </summary>
        IReadOnlyCollection<IProcess> GetIncompatibleProcesses(IReadOnlyCollection<IProcess> processes);

        void Stop();
    }
}
