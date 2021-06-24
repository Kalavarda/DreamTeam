namespace DreamTeam.Models.Mobs
{
    public class Bug: MobBase
    {
        public override float Radius => 0.05f;

        public override float Speed { get; } = 5000f / 3600f;


        public override Fractions Fraction => Fractions.Animals;
    }
}
