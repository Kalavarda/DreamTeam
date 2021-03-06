using System;
using System.Linq;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;

namespace DreamTeam.Processes
{
    public class FightProcess: IProcess
    {
        private bool _stopRequired;

        private readonly Fight _fight;
        private readonly IProcessor _processor;
        private readonly ICollisionDetector _collisionDetector;
        private readonly IPathFinder _pathFinder;
        private readonly IPriorityTargetDetector _priorityTargetDetector;
        private readonly TimeLimiter _timeLimiter = new TimeLimiter(TimeSpan.FromSeconds(0.5f));

        public event Action<IProcess> Completed;

        public FightProcess(Fight fight, IProcessor processor, ICollisionDetector collisionDetector, IPathFinder pathFinder, IPriorityTargetDetector priorityTargetDetector)
        {
            _fight = fight ?? throw new ArgumentNullException(nameof(fight));
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
            _collisionDetector = collisionDetector;
            _pathFinder = pathFinder;
            _priorityTargetDetector = priorityTargetDetector ?? throw new ArgumentNullException(nameof(priorityTargetDetector));
        }

        public void Process(TimeSpan delta)
        {
            if (_stopRequired)
            {
                Completed?.Invoke(this);
                return;
            }
            
            _timeLimiter.Do(() =>
            {
                foreach (var fighter in _fight.Fighters.Where(f => f.IsAlive).Where(f => !f.ManualManaged))
                {
                    var priorityTarget = _priorityTargetDetector.GetPriorityTarget(fighter);
                    if (fighter is ISkilledExt skilledExt)
                        skilledExt.Target = priorityTarget;
                    else
                        throw new NotImplementedException();

                    if (fighter.Target == null)
                        continue;

                    var distance = fighter.Position.DistanceTo(priorityTarget.Bounds);
                    var maxDistance = fighter.GetMaxSkillDistance();
                    if (distance > maxDistance)
                    {
                        if (!_processor.Get<MoveProcess>(mp => mp.PhysicalObject == fighter).Any())
                        {
                            var p = fighter.Position.GetPointAtLineTo(priorityTarget.Position, distance - maxDistance);
                            _processor.Add(new MoveProcess(fighter, p, _collisionDetector, _pathFinder));
                        }
                    }
                    else
                        _fight.UseSkill(fighter, priorityTarget);
                }
            });
        }

        public void Stop()
        {
            _stopRequired = true;
        }
    }
}
