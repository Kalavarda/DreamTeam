using DreamTeam.Models;

namespace DreamTeam.UserControls
{
    public partial class MobControl
    {
        public MobBase Mob { get; }

        public MobControl()
        {
            InitializeComponent();
        }

        public MobControl(MobBase mob) : this()
        {
            Mob = mob;

            Width = mob.Radius * 2;
            Height = mob.Radius * 2;
        }
    }
}
