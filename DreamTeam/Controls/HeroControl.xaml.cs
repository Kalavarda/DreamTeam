using DreamTeam.Models;

namespace DreamTeam.Controls
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
        }

        private void HeroPositionChanged()
        {
        }
    }
}
