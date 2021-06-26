using System;
using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using DreamTeam.Processes;
using DreamTeam.Utils.Abstract;

namespace DreamTeam
{
    public class FightManager: IFightManager
    {
        private readonly IProcessor _processor;
        private readonly List<Fight> _fights = new List<Fight>();
        private readonly IRelationDetector _relationDetector;
        private readonly ICollisionDetector _collisionDetector;

        public FightManager(IProcessor processor, IRelationDetector relationDetector, ICollisionDetector collisionDetector)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
            _relationDetector = relationDetector ?? throw new ArgumentNullException(nameof(relationDetector));
            _collisionDetector = collisionDetector ?? throw new ArgumentNullException(nameof(collisionDetector));
        }

        public void Attack(IFighter source, IFighter target)
        {
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

            var newFight = new Fight(source, target, _relationDetector);
            _fights.Add(newFight);
            _processor.Add(new FightProcess(newFight, _processor, _collisionDetector));
        }
    }
}
