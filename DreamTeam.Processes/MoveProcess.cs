using System;
using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models.Abstract;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Process;

namespace DreamTeam.Processes
{
    public class MoveProcess: IProcess, IIncompatibleProcess
    {
        private readonly PointF _target;
        private readonly ICollisionDetector _collisionDetector;
        private readonly IPathFinder _pathFinder;
        private bool _stopRequired;
        private Path _path;
        private int _pathNextPointNumber;

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

            if (_path == null || !_path.Points.Any())
            {
                Completed?.Invoke(this);
                return;
            }

            var nextPoint = _path.Points.Skip(_pathNextPointNumber).First();
            var a = PhysicalObject.Position.AngleTo(nextPoint);
            var d = PhysicalObject.Speed * (float)delta.TotalSeconds;

            var oldX = PhysicalObject.Position.X;
            var oldY = PhysicalObject.Position.Y;

            PhysicalObject.Direction.Value = a;
            PhysicalObject.Position.Set(PhysicalObject.Position.X + d * MathF.Cos(a), PhysicalObject.Position.Y + d * MathF.Sin(a));

            if (_collisionDetector.HasCollision(PhysicalObject.Bounds, null))
            {
                PhysicalObject.Position.Set(oldX, oldY);
                ResetPath();
                return;
            }

            if (PhysicalObject.Position.DistanceTo(nextPoint) < 0.01f)
                _pathNextPointNumber++;

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

        public MoveProcess(IPhysicalObject physicalObject, PointF target, ICollisionDetector collisionDetector, IPathFinder pathFinder)
        {
            PhysicalObject = physicalObject ?? throw new ArgumentNullException(nameof(physicalObject));
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _collisionDetector = collisionDetector ?? throw new ArgumentNullException(nameof(collisionDetector));
            _pathFinder = pathFinder ?? throw new ArgumentNullException(nameof(pathFinder));

            ResetPath();
        }

        private void ResetPath()
        {
            _path = _pathFinder.FindPath(PhysicalObject.Position, _target, PhysicalObject.Bounds);
            _pathNextPointNumber = 0;
        }
    }
}
