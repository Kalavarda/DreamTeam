using DreamTeam.Models;

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
        }
    }
}
