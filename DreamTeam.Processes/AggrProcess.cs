using System;
using System.Collections.Generic;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using DreamTeam.Utils;
using DreamTeam.Utils.Abstract;
using Environment = DreamTeam.Models.Environment;

namespace DreamTeam.Processes
{
    public class AggrProcess: IProcess
    {
        private readonly Environment _environment;
        private readonly Team _team;
        private readonly IFightManager _fightManager;
        private static readonly IProcess[] NoProcesses = new IProcess[0];
        private readonly TimeLimiter _timeLimiter = new TimeLimiter(TimeSpan.FromSeconds(1));

        public AggrProcess(Environment environment, Team team, IFightManager fightManager)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _team = team ?? throw new ArgumentNullException(nameof(team));
            _fightManager = fightManager ?? throw new ArgumentNullException(nameof(fightManager));
        }

        public event Action<IProcess> Finish;

        public void Process(TimeSpan delta)
        {
            _timeLimiter.Do(() =>
            {
                foreach(var mob in _environment.Mobs)
                foreach (var hero in _team.Heroes)
                {
                    var dist = mob.Position.DistanceTo(hero.Position);
                    if (dist < 2.5f)
                        _fightManager.Atack(mob, hero);
                }
            });
        }

        public IReadOnlyCollection<IProcess> GetIncompatibleProcesses(IReadOnlyCollection<IProcess> processes)
        {
            return NoProcesses;
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
