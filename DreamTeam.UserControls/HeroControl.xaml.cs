using System.Windows.Input;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;

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
            Hero = hero;

            Width = hero.Radius * 2;
            Height = hero.Radius * 2;

            _txt.Text = hero.Class.ToString()[0].ToString();

            hero.SelectedChanged += Hero_SelectedChanged;
            Hero_SelectedChanged(hero);
        }

        private void Hero_SelectedChanged(ISelectable selectable)
        {
            _ellipse.Opacity = Hero.IsSelected ? 0.75 : 0.25;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Hero.IsSelected = !Hero.IsSelected;
        }
    }
}
