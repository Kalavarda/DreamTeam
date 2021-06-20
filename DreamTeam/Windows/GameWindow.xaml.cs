using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DreamTeam.Controls;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;

namespace DreamTeam.Windows
{
    public partial class GameWindow
    {
        private readonly Game _game;
        private readonly ControlFactory _controlFactory = new ControlFactory();
        private readonly IDictionary<IPhysicalObject, UIElement> _objectControls = new Dictionary<IPhysicalObject, UIElement>();

        public GameWindow()
        {
            InitializeComponent();
        }

        public GameWindow(Game game): this()
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));

            foreach (var hero in game.Team.Heroes)
            {
                var control = _controlFactory.CreateControl(hero);
                _cnvLife.Children.Add(control);
                _objectControls.Add(hero, control);
            }

            foreach (var pair in _objectControls)
            {
                pair.Key.PositionChanged += PositionChanged;
                PositionChanged(pair.Key);
            }

            _scaleTransform.ScaleX = Settings.Default.Scale;
            _scaleTransform.ScaleY = Settings.Default.Scale;
        }

        private void PositionChanged(IPhysicalObject obj)
        {
            var control = _objectControls[obj];
            Canvas.SetLeft(control, obj.Position.X - obj.Radius);
            Canvas.SetTop(control, obj.Position.Y - obj.Radius);
        }
    }
}
