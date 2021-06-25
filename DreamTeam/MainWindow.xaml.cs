using System.ComponentModel;
using System.Windows;
using DreamTeam.Windows;

namespace DreamTeam
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var gameContext = ((App)Application.Current).GameContext;

            var gameWindow = new GameWindow(gameContext) { Owner = this };
            gameWindow.ShowDialog();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var gameContext = ((App)Application.Current).GameContext;

            gameContext.Dispose();
            base.OnClosing(e);
        }
    }
}
