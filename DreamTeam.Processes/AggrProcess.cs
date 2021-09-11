using System;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Process;
using Environment = DreamTeam.Models.Environment;

namespace DreamTeam.Processes
{
    public class AggrProcess: IProcess
    {
        private readonly Environment _environment;
        private readonly HeroTeam _team;
        private readonly IFightManager _fightManager;
        private readonly TimeLimiter _timeLimiter = new TimeLimiter(TimeSpan.FromSeconds(1));

        public AggrProcess(Environment environment, HeroTeam team, IFightManager fightManager)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _team = team ?? throw new ArgumentNullException(nameof(team));
            _fightManager = fightManager ?? throw new ArgumentNullException(nameof(fightManager));
        }

        public event Action<IProcess> Completed;

        public void Process(TimeSpan delta)
        {
            _timeLimiter.Do(() =>
            {
                foreach (var mob in _environment.Mobs)
                    foreach (var hero in _team.Heroes)
                    {
                        var dist = mob.Position.DistanceTo(hero.Position);
                        if (dist < 2.5f) // TODO
                        {
                            mob.Target = hero;
                            _fightManager.Attack(mob);
                        }
                    }
            });
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
