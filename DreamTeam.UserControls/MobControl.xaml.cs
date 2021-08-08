using System;
using System.Windows;
using System.Windows.Controls;
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

            if (mob.Bounds is RoundBounds round)
            {
                Width = round.Radius * 2;
                Height = round.Radius * 2;
            }
            else
                throw new NotImplementedException();

            _hpControl.Width = Math.Max(Width, 0.5);
            _hpControl.Height = Math.Max(Height / 10, 0.05);
            Canvas.SetTop(_hpControl, -_hpControl.Height);
            Canvas.SetLeft(_hpControl, -_hpControl.Width / 2 + Width / 2);
            _hpControl.Range = mob.HP;

            Loaded += MobControl_Loaded;
            Unloaded += MobControl_Unloaded;
        }

        private void MobControl_Loaded(object sender, RoutedEventArgs e)
        {
            Mob.HP.ValueChanged += HP_ValueChanged;
            HP_ValueChanged(Mob.HP);
        }

        private void HP_ValueChanged(RangeF hp)
        {
            this.Do(() =>
            {
                _hpControl.Visibility = Mob.HP.Value < Mob.HP.Max ? Visibility.Visible : Visibility.Collapsed;
            });
        }

        private void MobControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Mob.HP.ValueChanged -= HP_ValueChanged;
        }
    }
}
