using System;
using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Processes
{
    public class MoveProcess: IProcess
    {
        private readonly Point _target;
        private bool _stopRequired;

        public IPhysicalObject PhysicalObject { get; }

        public event Action<IProcess> Finish;

        public void Process(TimeSpan delta)
        {
            if (_stopRequired)
            {
                Finish?.Invoke(this);
                return;
            }

            var a = MathF.Atan2(_target.Y - PhysicalObject.Position.Y, _target.X - PhysicalObject.Position.X);
            var d = PhysicalObject.Speed * (float)delta.TotalSeconds;
            var dx = d * MathF.Cos(a);
            var dy = d * MathF.Sin(a);

            PhysicalObject.Position.Set(PhysicalObject.Position.X + dx, PhysicalObject.Position.Y + dy);

            if (PhysicalObject.Position.DistanceTo(_target) < PhysicalObject.Radius / 10)
                Finish?.Invoke(this);
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<IProcess> GetIncompatibleProcesses(IReadOnlyCollection<IProcess> processes)
        {
            if (processes == null) throw new ArgumentNullException(nameof(processes));

            return processes.Where(p => !IsCompatible(p)).ToArray();
        }

        public void Stop()
        {
            _stopRequired = true;
        }

        private bool IsCompatible(IProcess p)
        {
            if (p is MoveProcess mp)
                if (mp.PhysicalObject == PhysicalObject)
                    return false;
            return true;
        }

        public MoveProcess(IPhysicalObject physicalObject, Point target)
        {
            PhysicalObject = physicalObject ?? throw new ArgumentNullException(nameof(physicalObject));
            _target = target ?? throw new ArgumentNullException(nameof(target));
        }
    }
}
