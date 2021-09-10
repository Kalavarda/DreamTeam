using System;
using Kalavarda.Primitives;

namespace DreamTeam.Models.Abstract
{
    public interface ICreature
    {
        Fractions Fraction { get; }

        RangeF HP { get; }

        bool IsAlive { get; }

        bool IsDead { get; }

        event Action<ICreature> Died;
    }
}
