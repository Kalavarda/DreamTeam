namespace DreamTeam.Models
{
    public class RangeF
    {
        public float Min { get; } = default;

        public float Max { get; } = default;

        public float Value { get; } = default;

        public RangeF(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}
