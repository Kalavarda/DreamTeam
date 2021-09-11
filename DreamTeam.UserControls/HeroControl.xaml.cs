using System;
using System.Windows.Media;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.WPF;

namespace DreamTeam.UserControls
{
    public partial class HeroControl
    {
        public Hero Hero { get; }

        public HeroControl()
        {
            InitializeComponent();
        }

        public HeroControl(Hero hero) : this()
        {
            Hero = hero ?? throw new ArgumentNullException(nameof(hero));

            if (hero.Bounds is RoundBounds round)
            {
                Width = round.Radius * 2;
                Height = round.Radius * 2;
            }
            else
                throw new NotImplementedException();

            _txt.Text = hero.Class.ToString()[0].ToString();

            switch (hero.Class)
            {
                case HeroClass.Tank:
                    _ellipse.Fill = Brushes.Orange;
                    break;
                
                case HeroClass.Healer:
                    _ellipse.Fill = Brushes.Green;
                    break;
                
                case HeroClass.Support:
                    _ellipse.Fill = Brushes.Yellow;
                    break;
                
                case HeroClass.RangeDD:
                    _ellipse.Fill = Brushes.Cyan;
                    break;
                
                case HeroClass.MeleeDD:
                    _ellipse.Fill = Brushes.Maroon;
                    break;
                
                default:
                    throw new NotImplementedException();
            }

            hero.SelectedChanged += Hero_SelectedChanged;
            Hero_SelectedChanged(hero);

            hero.Died += Hero_Died;

            hero.Direction.Changed += Direction_Changed;
            Direction_Changed();
        }

        private void Direction_Changed()
        {
            this.Do(() =>
            {
            //    _rot.Angle = Hero.Direction.ValueInDegrees;
            });
        }

        private void Hero_Died(ICreature creature)
        {
            this.Do(() =>
            {
                Opacity = 0.25f;
            });
        }

        private void Hero_SelectedChanged(ISelectable selectable)
        {
            //_ellipse.Opacity = Hero.IsSelected ? 0.75 : 0.25;
            _ellipse.Opacity = 0.75;
        }
    }
}
