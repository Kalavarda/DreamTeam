using System;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public class TimeLimiter : ITimeLimiter
    {
        private DateTime _lastTime = DateTime.MinValue;

        public TimeSpan Interval { get; set; }

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

        public T Do<T>(Func<T> action)
        {
            if (DateTime.Now - _lastTime < Interval)
                return default;

            var result = action();
            _lastTime = DateTime.Now;
            return result;
        }
    }
}
