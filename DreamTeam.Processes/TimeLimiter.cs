using System;

namespace DreamTeam.Processes
{
    public class TimeLimiter
    {
        private DateTime _lastTime = DateTime.MinValue;

        public TimeSpan Interval { get; }

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
