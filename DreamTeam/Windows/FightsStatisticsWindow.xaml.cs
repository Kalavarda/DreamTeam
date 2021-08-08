using System;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Windows
{
    public partial class FightsStatisticsWindow
    {
        public FightsStatisticsWindow()
        {
            InitializeComponent();
        }

        public FightsStatisticsWindow(IFightsHistory fightsHistory): this()
        {
            if (fightsHistory == null) throw new ArgumentNullException(nameof(fightsHistory));

            _statisticsControl.FightsHistory = fightsHistory;
        }
    }
}
