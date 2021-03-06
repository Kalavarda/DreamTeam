using System;
using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using DreamTeam.Processes;
using DreamTeam.Utils;
using Kalavarda.Primitives.Process;

namespace DreamTeam
{
    public class FightManager: IFightManager
    {
        private readonly IProcessor _processor;
        private readonly List<Fight> _fights = new List<Fight>();
        private readonly IRelationDetector _relationDetector;
        private readonly ICollisionDetector _collisionDetector;
        private readonly IPathFinder _pathFinder;
        private readonly IFightsHistoryExt _fightsHistory;

        public FightManager(IProcessor processor, IRelationDetector relationDetector, ICollisionDetector collisionDetector, IPathFinder pathFinder, IFightsHistoryExt fightsHistory)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
            _relationDetector = relationDetector ?? throw new ArgumentNullException(nameof(relationDetector));
            _collisionDetector = collisionDetector;
            _pathFinder = pathFinder;
            _fightsHistory = fightsHistory ?? throw new ArgumentNullException(nameof(fightsHistory));
        }

        public void Attack(IFighter source)
        {
            var target = source.Target as IFighter;
            if (target == null)
                return;

            foreach (var fight in _fights)
            {
                if (fight.Fighters.Contains(source) && fight.Fighters.Contains(target))
                    return;

                if (fight.Fighters.Contains(source) && !fight.Fighters.Contains(target))
                {
                    fight.Add(target);
                    return;
                }

                if (!fight.Fighters.Contains(source) && fight.Fighters.Contains(target))
                {
                    fight.Add(source);
                    return;
                }
            }

            var newFight = new Fight(source, target);
            _fights.Add(newFight);
            _processor.Add(new FightProcess(newFight, _processor, _collisionDetector, _pathFinder, new PriorityTargetDetector(newFight, _relationDetector)));

            _fightsHistory.Add(newFight);
        }
    }
}
