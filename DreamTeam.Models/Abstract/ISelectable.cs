using System;

namespace DreamTeam.Models.Abstract
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }

        event Action<ISelectable> SelectedChanged;
    }
}
