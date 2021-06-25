using System;

namespace DreamTeam.Models
{
    public class TimeLimiter
    {
        private DateTime _lastTime = DateTime.MinValue;

        public TimeSpan Interval { get; }

        public TimeSpan Remain
        {
            get
            {
                var nextTime = _lastTime + Interval;
                return nextTime <= DateTime.Now
                    ? TimeSpan.Zero
                    : nextTime - DateTime.Now;
            }
        }

        public TimeLimiter(TimeSpan interval)
        {
            Interval = interval;
        }

        public void Do(Action action)
        {
            if (DateTime.Now - _lastTime < Interval)
                return;

            action();
            _lastTime = DateTime.Now;
        }
    }
}
