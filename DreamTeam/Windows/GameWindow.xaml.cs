using System;
using System.Windows;
using DreamTeam.Models;

namespace DreamTeam.Windows
{
    public partial class GameWindow
    {
        private readonly Game _game;

        public GameWindow()
        {
            InitializeComponent();
        }

        public GameWindow(Game game): this()
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));

            foreach (var hero in game.Team.Heroes)
                _lrDynamic.Add(hero);

            Loaded += GameWindow_Loaded;
        }

        private void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _translateTransform.X = _cnv.RenderSize.Width / 2;
            _translateTransform.Y = _cnv.RenderSize.Height / 2;
        }
    }
}
