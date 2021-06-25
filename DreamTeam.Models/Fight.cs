using System;
using System.Collections.Generic;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public class Fight
    {
        private readonly List<IFighter> _fighters = new List<IFighter>();

        public IReadOnlyCollection<IFighter> Fighters => _fighters;

        public Fight(IFighter source, IFighter target)
        {
            Add(source);
            Add(target);
        }

        public void Add(IFighter fighter)
        {
            if (fighter == null) throw new ArgumentNullException(nameof(fighter));
            _fighters.Add(fighter);
        }
    }
}
