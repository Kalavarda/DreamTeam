using System;
using System.Threading;
using DreamTeam.Utils.Abstract;

namespace DreamTeam.Utils
{
    public class MultiProcessor: IProcessor
    {
        private readonly Processor[] _processors;
        private byte _nextProcess;

        public MultiProcessor(int maxFrequency, CancellationToken cancellationToken)
        {
            _processors = new Processor[Environment.ProcessorCount];
            for (var i = 0; i < _processors.Length; i++)
                _processors[i] = new Processor(maxFrequency, cancellationToken);
        }

        public void Add(IProcess process, bool stopIncompatible = true)
        {
            if (stopIncompatible)
                foreach (var processor in _processors)
                    processor.StopIncompatible(process);

            _processors[_nextProcess].Add(process, false);

            _nextProcess++;
            if (_nextProcess == _processors.Length)
                _nextProcess = 0;
        }
    }
}
