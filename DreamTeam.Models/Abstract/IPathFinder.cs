using System;
using System.Collections.Generic;
using Kalavarda.Primitives.Geometry;

namespace DreamTeam.Models.Abstract
{
    public interface IPathFinder
    {
        /// <summary>
        /// Ищет свободный путь от <see cref="from"/> до <see cref="to"/> для объекта <see cref="Bounds"/>
        /// </summary>
        Path FindPath(PointF from, PointF to, BoundsF bounds);
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
