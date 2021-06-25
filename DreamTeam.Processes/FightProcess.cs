using System;
using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using DreamTeam.Utils.Abstract;

namespace DreamTeam.Processes
{
    public class FightProcess: IProcess
    {
        private bool _stopRequired;

        private readonly Fight _fight;
        private readonly IProcessor _processor;
        private static readonly IProcess[] NoProcesses = new IProcess[0];
        private readonly TimeLimiter _timeLimiter = new TimeLimiter(TimeSpan.FromSeconds(0.5f));

        public event Action<IProcess> Completed;

        public FightProcess(Fight fight, IProcessor processor)
        {
            _fight = fight ?? throw new ArgumentNullException(nameof(fight));
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
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
                foreach (var fighter in _fight.Fighters)
                {
                    var aggroLeader = _fight.GetAggroLeaderFor(fighter);

                    var p1 = ((IPhysicalObject)fighter).Position;
                    var p2 = ((IPhysicalObject)aggroLeader).Position;
                    var distance = p1.DistanceTo(p2);
                    var maxDistance = ((ISkilled)fighter).GetMaxSkillDistance();
                    if (distance > maxDistance)
                    {
                        if (!_processor.Get<MoveProcess>(mp => mp.PhysicalObject == fighter).Any())
                        {
                            var p = p1.GetPointAtLineTo(p2, maxDistance);
                            _processor.Add(new MoveProcess((IPhysicalObject) fighter, p));
                        }
                    }
                    else
                        fighter.Attack(aggroLeader);
                }

                // TODO: условие завершения
            });
        }

        public IReadOnlyCollection<IProcess> GetIncompatibleProcesses(IReadOnlyCollection<IProcess> processes)
        {
            return NoProcesses;
        }

        public void Stop()
        {
            _stopRequired = true;
        }
    }
}
