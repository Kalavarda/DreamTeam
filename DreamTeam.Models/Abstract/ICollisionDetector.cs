using System.Collections.Generic;

namespace DreamTeam.Models.Abstract
{
    public interface ICollisionDetector
    {
        /// <summary>
        /// Проверяет - столкнулся ли указанный <see cref="bounds"/> с чем-нибудь другим
        /// </summary>
        bool HasCollision(Bounds bounds, IReadOnlyCollection<Bounds> ignoreBounds = null);
    }
}
