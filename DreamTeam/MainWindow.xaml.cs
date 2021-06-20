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
            var gameWindow = new GameWindow(((App)Application.Current).Game) { Owner = this };
            gameWindow.ShowDialog();
        }
    }
}
