﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DreamTeam.Utils.Abstract;

namespace DreamTeam.Utils
{
    public class Processor : IProcessor
    {
        private readonly TimeSpan _maxDuration;
        private readonly CancellationToken _cancellationToken;
        private readonly IList<IProcess> _processes = new List<IProcess>();
        private readonly IDictionary<IProcess, DateTime> _times = new Dictionary<IProcess, DateTime>();

        public void Add(IProcess process, bool stopIncompatible = true)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));

            if (stopIncompatible)
                StopIncompatible(process);

            process.Completed += ProcessCompleted;
            _processes.Add(process);
        }

        private void ProcessCompleted(IProcess process)
        {
            _processes.Remove(process);
            _times.Remove(process);
        }

        public Processor(int maxFrequency, CancellationToken cancellationToken)
        {
            _maxDuration = TimeSpan.FromSeconds(1d / maxFrequency);
            _cancellationToken = cancellationToken;
            ThreadPool.QueueUserWorkItem(Process);
        }

        private void Process(object stateInfo)
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                var startTime = DateTime.Now;

                foreach (var p in _processes.ToArray())
                    if (!_cancellationToken.IsCancellationRequested)
                    {
                        if (!_times.TryGetValue(p, out var lastTime))
                            lastTime = DateTime.Now;
                        p.Process(DateTime.Now - lastTime);
                        _times[p] = DateTime.Now;
                    }

                var duration = DateTime.Now - startTime;
                var remaining = _maxDuration - duration;
                if (remaining.Ticks > 0)
                    Thread.Sleep(remaining);
            }
        }

        public void StopIncompatible(IProcess process)
        {
            var incompatibleProcesses = process.GetIncompatibleProcesses(_processes.ToArray());
            foreach (var incompatibleProcess in incompatibleProcesses)
                incompatibleProcess.Stop();
        }
    }
}
