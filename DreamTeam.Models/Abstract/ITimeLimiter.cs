using System;

namespace DreamTeam.Models.Abstract
{
    public interface ITimeLimiter
    {
        TimeSpan Interval { get; set; }
        TimeSpan Remain { get; }
    }
}
