namespace DreamTeam.Models.Abstract
{
    public interface ICreature
    {
        Fractions Fraction { get; }

        RangeF HP { get; }
    }
}
