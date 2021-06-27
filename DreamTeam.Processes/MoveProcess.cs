using System;
using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using DreamTeam.Utils.Abstract;

namespace DreamTeam.Processes
{
    public class MoveProcess: IProcess
    {
        private readonly PointF _target;
        private readonly ICollisionDetector _collisionDetector;
        private bool _stopRequired;

        public IPhysicalObject PhysicalObject { get; }

        public event Action<IProcess> Completed;

        public void Process(TimeSpan delta)
        {
            if (PhysicalObject is ICreature creature)
                if (creature.IsDead)
                {
                    Completed?.Invoke(this);
                    return;
                }

            if (_stopRequired)
            {
                Completed?.Invoke(this);
                return;
            }

            var a = MathF.Atan2(_target.Y - PhysicalObject.Position.Y, _target.X - PhysicalObject.Position.X);
            var d = PhysicalObject.Speed * (float)delta.TotalSeconds;

            var oldX = PhysicalObject.Position.X;
            var oldY = PhysicalObject.Position.Y;
            PhysicalObject.Position.Set(PhysicalObject.Position.X + d * MathF.Cos(a), PhysicalObject.Position.Y + d * MathF.Sin(a));

            if (_collisionDetector.HasCollision(PhysicalObject.Bounds))
            {
                PhysicalObject.Position.Set(oldX, oldY);
                Completed?.Invoke(this);
                return;
            }

            if (PhysicalObject.Position.DistanceTo(_target) < 0.01f)
                Completed?.Invoke(this);
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

        public MoveProcess(IPhysicalObject physicalObject, PointF target, ICollisionDetector collisionDetector)
        {
            PhysicalObject = physicalObject ?? throw new ArgumentNullException(nameof(physicalObject));
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _collisionDetector = collisionDetector ?? throw new ArgumentNullException(nameof(collisionDetector));
        }
    }
}
