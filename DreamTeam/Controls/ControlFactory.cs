using System;
using System.Windows;
using DreamTeam.Models;

namespace DreamTeam.Controls
{
    public class ControlFactory
    {
        public UIElement CreateControl(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            if (obj is Hero hero)
                return new HeroControl(hero);

            throw new NotImplementedException();
        }
    }
}
