using System.Linq;
using System.Windows.Controls;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using Kalavarda.Primitives.WPF;

namespace DreamTeam.UserControls
{
    public partial class FightStatisticsControl
    {
        private IFight _fight;
        private FightStatistics.Mode _mode;
        private IFighter _hero;

        public IFight Fight
        {
            get => _fight;
            set
            {
                if (Fight == value)
                    return;

                if (_fight != null)
                    _fight.Statistics.Changed -= Statistics_Changed;

                _fight = value;

                _fight.Statistics.Changed += Statistics_Changed;

                RefreshData();
            }
        }

        private void Statistics_Changed()
        {
            this.Do(RefreshData);
        }

        public FightStatistics.Mode Mode
        {
            get => _mode;
            set
            {
                if (_mode == value)
                    return;

                _mode = value;

                RefreshData();
            }
        }

        public IFighter Hero
        {
            get => _hero;
            set
            {
                if (_hero == value)
                    return;

                _hero = value;

                RefreshData();
            }
        }

        public FightStatisticsControl()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            if (Fight == null || Hero == null)
            {
                _dg.ItemsSource = null;
                return;
            }

            _dg.ItemsSource = FightStatistics.Aggregate(Mode, Fight, Hero);
        }
    }
}
