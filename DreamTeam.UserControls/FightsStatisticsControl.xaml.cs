using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;

namespace DreamTeam.UserControls
{
    public partial class FightsStatisticsControl
    {
        private IFightsHistory _fightsHistory;

        public IFightsHistory FightsHistory
        {
            get => _fightsHistory;
            set
            {
                if (FightsHistory == value)
                    return;

                if (FightsHistory != null)
                    FightsHistory.Changed -= FightsHistory_Changed;

                _fightsHistory = value;

                if (FightsHistory != null)
                {
                    FightsHistory.Changed += FightsHistory_Changed;
                    FightsHistory_Changed();
                }
            }
        }

        public IFighter SelectedHero => _cbFighters.SelectedItem as IFighter;

        public IFight SelectedFight => _cbFights.SelectedItem as IFight;

        public FightStatistics.Mode SelectedMode => (FightStatistics.Mode)_cbMode.SelectedItem;

        private void FightsHistory_Changed()
        {
            this.Do(() =>
            {
                _cbFights.ItemsSource = FightsHistory.Fights; // TODO: sort by time
                if (SelectedFight == null)
                    _cbFights.SelectedItem = FightsHistory.Fights.LastOrDefault(); // TODO: sort by time

                var oldFighter = SelectedHero;
                var fighters = FightsHistory.Fights
                    .SelectMany(f => f.Fighters.OfType<Hero>())
                    .Distinct()
                    .ToArray();
                _cbFighters.ItemsSource = fighters;
                _cbFighters.SelectedItem = fighters.Contains(oldFighter) ? oldFighter : fighters.FirstOrDefault();
            });
        }

        public FightsStatisticsControl()
        {
            InitializeComponent();

            _fightControl.Visibility = Visibility.Collapsed;

            _cbMode.ItemsSource = new[]
            {
                FightStatistics.Mode.DPS,
                FightStatistics.Mode.HPS
            };
            _cbMode.SelectedItem = FightStatistics.Mode.DPS;
        }

        private void OnFightSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _fightControl.Fight = SelectedFight;
            _fightControl.Visibility = _fightControl.Fight != null ? Visibility.Visible : Visibility.Collapsed;
        }

        private void OnModeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _fightControl.Mode = SelectedMode;
        }

        private void OnHeroSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _fightControl.Hero = SelectedHero;
        }
    }
}
