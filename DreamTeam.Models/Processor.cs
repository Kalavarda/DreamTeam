using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public class Processor : IProcessor
    {
        private const int MaxFreq = 100;

        private readonly CancellationToken _cancellationToken;
        private readonly IList<IProcess> _processes = new List<IProcess>();

        public void Add(IProcess process)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));

            var incompatibleProcesses = process.GetIncompatibleProcesses(_processes.ToArray());
            foreach (var incompatibleProcess in incompatibleProcesses)
                incompatibleProcess.Stop();

            process.Finish += Process_Finish;
            _processes.Add(process);
        }

        private void Process_Finish(IProcess process)
        {
            _processes.Remove(process);
        }

        public Processor(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            ThreadPool.QueueUserWorkItem(Process);
        }

        private void Process(object stateInfo)
        {
            // Сделать нормальное вычисление delta для каждого процесса

            var lastTime = DateTime.Now;
            while (!_cancellationToken.IsCancellationRequested)
            {
                foreach (var p in _processes.ToArray())
                    p.Process(DateTime.Now - lastTime);
                lastTime = DateTime.Now;

                Thread.Sleep(TimeSpan.FromSeconds(1d / MaxFreq));
            }
        }
    }
}
