using System.Windows;
using System.Windows.Controls;
using DreamTeam.Models;
using DreamTeam.UserControls;

namespace DreamTeam.Controls
{
    public partial class SelectionHudControl
    {
        private Game _game;
        
        private readonly float _scale = Settings.Default.Scale; // кэш, не забыть обновлять

        public Game Game
        {
            get => _game;
            set
            {
                if (_game == value)
                    return;

                if (_game != null)
                    _game.Team.SelectedHeroChanged -= Team_SelectedHeroChanged;

                _game = value;

                if (_game != null)
                {
                    _game.Team.SelectedHeroChanged += Team_SelectedHeroChanged;
                    Team_SelectedHeroChanged(null, _game.Team.SelectedHero);
                }
            }
        }

        private void Team_SelectedHeroChanged(Hero oldValue, Hero newValue)
        {
            if (oldValue != null)
                oldValue.PositionChanged -= Hero_PositionChanged;

            if (newValue == null)
            {
                _heroSelection.Visibility = Visibility.Collapsed;
                return;
            }

            newValue.PositionChanged += Hero_PositionChanged;

            _heroSelection.Visibility = Visibility.Visible;
            _heroSelection.Width = newValue.Bounds.Width * _scale + 40;
            _heroSelection.Height = newValue.Bounds.Height * _scale + 40;
            Hero_PositionChanged(newValue);
        }

        private void Hero_PositionChanged(Models.Abstract.IPhysicalObject hero)
        {
            this.Do(() =>
            {
                Canvas.SetLeft(_heroSelection, hero.Position.X * _scale - _heroSelection.Width / 2);
                Canvas.SetTop(_heroSelection, hero.Position.Y * _scale - _heroSelection.Height / 2);
            });
        }

        public SelectionHudControl()
        {
            InitializeComponent();
        }
    }
}
