using System;
using System.Threading;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using DreamTeam.Processes;
using DreamTeam.Utils;
using DreamTeam.Utils.Abstract;

namespace DreamTeam
{
    public class GameContext: IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public IFightManager FightManager { get; }

        public IProcessor Processor { get; }

        public Game Game { get; } = new Game();

        public ICollisionDetector CollisionDetector { get; }

        public IPathFinder PathFinder { get; }

        public IFightsHistoryExt FightsHistory { get; } = new FightsHistory();

        public GameContext()
        {
            Processor = new MultiProcessor(Settings.Default.MaxFPS, _cancellationTokenSource.Token);
            CollisionDetector = new CollisionDetector(Game.Team, Game.Environment);
            PathFinder = new PathFinder(CollisionDetector);
            FightManager = new FightManager(Processor, new RelationDetector(), CollisionDetector, PathFinder, FightsHistory);

            var aggrProcess = new AggrProcess(Game.Environment, Game.Team, FightManager);
            Processor.Add(aggrProcess);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
