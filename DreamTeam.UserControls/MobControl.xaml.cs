using System;
using DreamTeam.Models;
using Kalavarda.Primitives.Geometry;

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

            if (mob.Bounds is RoundBounds round)
            {
                Width = round.Radius * 2;
                Height = round.Radius * 2;
            }
            else
                throw new NotImplementedException();
        }
    }
}
