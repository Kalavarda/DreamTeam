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
        private readonly IRelationDetector _relationDetector;
        private static readonly IProcess[] NoProcesses = new IProcess[0];

        public event Action<IProcess> Completed;

        public FightProcess(Fight fight, IRelationDetector relationDetector)
        {
            _fight = fight ?? throw new ArgumentNullException(nameof(fight));
            _relationDetector = relationDetector ?? throw new ArgumentNullException(nameof(relationDetector));
        }

        public void Process(TimeSpan delta)
        {
            if (_stopRequired)
            {
                Completed?.Invoke(this);
                return;
            }

            foreach (var fighter in _fight.Fighters)
            foreach (var enemy in _fight.Fighters.Where(f => _relationDetector.GetRelationTo(fighter, f) == Relation.Enemy))
            {
                // TODO: проверить расстояние
                fighter.Attack(enemy);
            }

            // TODO: условие завершения
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
