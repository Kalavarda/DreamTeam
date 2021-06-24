using System.ComponentModel;
using System.Threading;
using System.Windows;
using DreamTeam.Models;
using DreamTeam.Windows;

namespace DreamTeam
{
    public partial class MainWindow
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var game = ((App)Application.Current).Game;
            var processor = new Processor(_cancellationTokenSource.Token);

            var gameWindow = new GameWindow(game, processor) { Owner = this };
            gameWindow.ShowDialog();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _cancellationTokenSource.Cancel();

            base.OnClosing(e);
        }
    }
}
