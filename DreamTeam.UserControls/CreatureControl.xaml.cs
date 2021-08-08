using DreamTeam.Models.Abstract;

namespace DreamTeam.UserControls
{
    public partial class CreatureControl
    {
        private ICreature _creature;

        public ICreature Creature
        {
            get => _creature;
            set
            {
                if (_creature == value)
                    return;

                _creature = value;

                _hpControl.Range = _creature?.HP;
                _name.Text = _creature?.ToString();
            }
        }

        public CreatureControl()
        {
            InitializeComponent();
        }
    }
}
