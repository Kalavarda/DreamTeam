using System;
using System.Collections.Generic;
using System.Linq;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Models
{
    public class FightStatistics : IFightStatisticsExt
    {
        private readonly List<ChangeExt> _changes = new List<ChangeExt>();

        public IReadOnlyCollection<ChangeExt> Changes => _changes;

        public enum Mode
        {
            DPS,
            HPS
        }

        // TODO: replace with Record
        public class AggregatedData
        {
            public AggregatedData(string skill, uint count, float perSecond, float total, float percent)
            {
                Skill = skill;
                Count = count;
                PerSecond = perSecond;
                Total = total;
                Percent = percent;
            }

            public string Skill { get; }

            public uint Count { get; }

            public float PerSecond { get; }

            public float Total { get; }

            public float Percent { get; }
        }

        public event Action Changed;

        public void Add(ChangeExt changeExt)
        {
            _changes.Add(changeExt);
            Changed?.Invoke();
        }

        public static IReadOnlyCollection<AggregatedData> Aggregate(Mode mode, IFight fight, IFighter fighter)
        {
            switch (mode)
            {
                case Mode.DPS:
                    return AggregateDps(fight, fighter);

                case Mode.HPS:
                    return AggregateHps(fight, fighter);

                default:
                    throw new NotImplementedException();
            }
        }

        internal static IReadOnlyCollection<AggregatedData> AggregateDps(IFight fight, IFighter fighter)
        {
            // TODO: сделать нормально

            var result = new List<AggregatedData>();

            var changes = fight.Statistics.Changes
                .Where(ch => ch.Source == fighter)
                .Where(ch => ch.HpDiff < 0)
                .ToArray();

            if (changes.Length == 0)
                return result;

            var t1 = changes.Min(ch => ch.Time);
            var t2 = changes.Max(ch => ch.Time);
            var totalSeconds = (float)(t2 - t1).TotalSeconds;

            var dict = changes
                .GroupBy(ch => ch.Skill)
                .ToDictionary(gr => gr.Key, gr => gr.Sum(ch => -ch.HpDiff));

            var total = dict.Values.Sum();

            foreach (var pair in dict)
            {
                var perSecond = totalSeconds > 0
                    ? pair.Value / totalSeconds
                    : 0;
                var count = (uint)changes.Count(ch => ch.Skill == pair.Key);
                result.Add(new AggregatedData(pair.Key.Name, count, perSecond, pair.Value, 100 * pair.Value / total));
            }

            return result;
        }

        internal static IReadOnlyCollection<AggregatedData> AggregateHps(IFight fight, IFighter fighter)
        {
            // TODO: сделать нормально

            var result = new List<AggregatedData>();

            var changes = fight.Statistics.Changes
                .Where(ch => ch.Source == fighter)
                .Where(ch => ch.HpDiff > 0)
                .ToArray();

            if (changes.Length == 0)
                return result;

            var t1 = changes.Min(ch => ch.Time);
            var t2 = changes.Max(ch => ch.Time);
            var totalSeconds = (float)(t2 - t1).TotalSeconds;

            var dict = changes
                .GroupBy(ch => ch.Skill)
                .ToDictionary(gr => gr.Key, gr => gr.Sum(ch => ch.HpDiff));

            var total = dict.Values.Sum();

            foreach (var pair in dict)
            {
                var perSecond = totalSeconds > 0
                    ? pair.Value / totalSeconds
                    : 0;
                var count = (uint)changes.Count(ch => ch.Skill == pair.Key);
                result.Add(new AggregatedData(pair.Key.Name, count, perSecond, pair.Value, 100 * pair.Value / total));
            }

            return result;
        }
    }
}
