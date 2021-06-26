using System;

namespace DreamTeam.Models
{
    public class RangeF
    {
        private float _value;

        public float Min { get; } = default;

        public float Max { get; } = default;

        public float Value
        {
            get => _value;
            set
            {
                if (value > Max)
                    value = Max;

                if (value < Min)
                    value = Min;

                if (MathF.Abs(_value - value) < 0.001f)
                    return;

                _value = value;
                ValueChanged?.Invoke(this);
            }
        }

        public event Action<RangeF> ValueChanged;

        public RangeF(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}
