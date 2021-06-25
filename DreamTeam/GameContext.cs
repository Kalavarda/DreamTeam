using System;
using System.Threading;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using DreamTeam.Processes;
using DreamTeam.Utils;

namespace DreamTeam
{
    public class GameContext: IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public IFightManager FightManager { get; } = new FightManager();

        public Processor Processor { get; }

        public Game Game { get; } = new Game();

        public GameContext()
        {
            Processor = new Processor(Settings.Default.MaxFPS, _cancellationTokenSource.Token);

            var aggrProcess = new AggrProcess(Game.Environment, Game.Team, FightManager);
            Processor.Add(aggrProcess);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
