using System;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public class RelationDetector: IRelationDetector
    {
        public Relation GetRelationTo(ICreature @from, ICreature to)
        {
            if (@from == null) throw new ArgumentNullException(nameof(@from));
            if (to == null) throw new ArgumentNullException(nameof(to));

            if (@from.Fraction == to.Fraction)
                return Relation.Friendly;

            // TODO: сделать нормально
            switch (@from.Fraction)
            {
                case Fractions.Animals:
                    return Relation.Enemy;

                case Fractions.Heroes:
                    return Relation.Enemy;

                default:
                    throw new NotImplementedException(@from.Fraction.ToString());
            }
        }
    }
}
