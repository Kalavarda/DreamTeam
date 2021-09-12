using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using DreamTeam.UserControls;
using Kalavarda.Primitives;
using Kalavarda.Primitives.WPF;
using Kalavarda.Primitives.WPF.Controls;

namespace DreamTeam.Controls
{
    public partial class HpBarsHudControl
    {
        private Game _game;

        private readonly IDictionary<IPhysicalObject, RangeControl> _dict = new Dictionary<IPhysicalObject, RangeControl>();

        public Game Game
        {
            get => _game;
            set
            {
                if (_game == value)
                    return;

                _game = value;

                if (_game == null)
                    return;

                foreach (var hero in _game.Team.Heroes)
                {
                    var hpControl = new RangeControl { Range = hero.HP, Width = 50, Height = 5, MainBrush = Brushes.Green };
                    _canvas.Children.Add(hpControl);
                    _dict.Add(hero, hpControl);

                    hero.PositionChanged += OnPositionChanged;
                    OnPositionChanged(hero);
                }

                _game.Environment.MobAdded += Environment_MobAdded;
                foreach (var mob in _game.Environment.Mobs)
                    Environment_MobAdded(mob);
            }
        }

        private void Environment_MobAdded(MobBase mob)
        {
            var hpControl = new RangeControl { Range = mob.HP, Width = 50, Height = 5, MainBrush = Brushes.Maroon };
            _canvas.Children.Add(hpControl);
            _dict.Add(mob, hpControl);

            mob.HP.ValueChanged += HP_ValueChanged;
            HP_ValueChanged(mob.HP);

            mob.PositionChanged += OnPositionChanged;
            OnPositionChanged(mob);
        }

        private void HP_ValueChanged(RangeF hp)
        {
            this.Do(() =>
            {
                var mob = Game.Environment.Mobs.First(m => m.HP == hp); // TODO: optimize
                var hpControl = _dict[mob];
                hpControl.Visibility = hp.Value < hp.Max && hp.Value > hp.Min ? Visibility.Visible : Visibility.Collapsed;
            });
        }

        private void OnPositionChanged(IPhysicalObject obj)
        {
            this.Do(() =>
            {
                var control = _dict[obj];
                Canvas.SetLeft(control, obj.Position.X * Settings.Default.Scale - control.Width / 2);
                Canvas.SetTop(control, obj.Position.Y * Settings.Default.Scale - control.Width / 2 - control.Height);
            });
        }

        public HpBarsHudControl()
        {
            InitializeComponent();
        }
    }
}
