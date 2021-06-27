using System;
using System.Collections.Generic;

namespace DreamTeam.Models.Abstract
{
    public interface IPathFinder
    {
        Path FindPath(PointF from, PointF to);
    }

    public class Path
    {
        public PointF From { get; }

        public IReadOnlyCollection<PointF> Points { get; }

        public Path(PointF @from, IReadOnlyCollection<PointF> points)
        {
            From = @from ?? throw new ArgumentNullException(nameof(@from));
            Points = points ?? throw new ArgumentNullException(nameof(points));
        }
    }
}
