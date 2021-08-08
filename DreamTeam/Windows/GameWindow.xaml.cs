using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DreamTeam.Models;
using DreamTeam.Models.Abstract;
using DreamTeam.Processes;

namespace DreamTeam.Windows
{
    public partial class GameWindow
    {
        private readonly GameContext _gameContext;
        private readonly DragAndDropController _dragAndDropController;
        private FightsStatisticsWindow _statisticsWindow;

        public GameWindow()
        {
            InitializeComponent();

            _dragAndDropController = new DragAndDropController(_grd, _translateTransform);
        }

        public GameWindow(GameContext gameContext) : this()
        {
            _gameContext = gameContext ?? throw new ArgumentNullException(nameof(gameContext));

            foreach (var mob in gameContext.Game.Environment.Mobs)
                _lrEnvironment.Add(mob);

            foreach (var hero in gameContext.Game.Team.Heroes)
                _lrDynamic.Add(hero);

            Loaded += GameWindow_Loaded;
        }

        private void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _translateTransform.X = _cnv.RenderSize.Width / 2;
            _translateTransform.Y = _cnv.RenderSize.Height / 2;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var hero = _gameContext.Game.Selected as IPhysicalObject;
            if (hero == null)
                return;

            if (e.ClickCount == 2)
            {
                var pos = e.GetPosition(_cnv);
                var x = (float)pos.X / Settings.Default.Scale;
                var y = (float)pos.Y / Settings.Default.Scale;
                var process = new MoveProcess(hero, new PointF(x, y), _gameContext.CollisionDetector, _gameContext.PathFinder);
                _gameContext.Processor.Add(process);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Handled)
                return;

            switch (e.Key)
            {
                case Key.S:
                    if (_statisticsWindow == null)
                        _statisticsWindow = new FightsStatisticsWindow(_gameContext.FightsHistory) { Owner = this };
                    _statisticsWindow.Show();
                    e.Handled = true;
                    break;
            }
        }
    }

    public class DragAndDropController
    {
        private readonly UIElement _uiElement;
        private readonly TranslateTransform _translateTransform;
        private Point _startPosition;
        private Point _startTranslate;

        public DragAndDropController(UIElement uiElement, TranslateTransform translateTransform)
        {
            _uiElement = uiElement ?? throw new ArgumentNullException(nameof(uiElement));
            _translateTransform = translateTransform ?? throw new ArgumentNullException(nameof(translateTransform));

            _uiElement.MouseDown += UiElement_MouseDown;
            _uiElement.MouseMove += UiElement_MouseMove;
            _uiElement.MouseUp += UiElement_MouseUp;
        }

        private void UiElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Middle)
                return;

            _startPosition = e.GetPosition(_uiElement);
            _uiElement.CaptureMouse();

            _startTranslate = new System.Windows.Point(_translateTransform.X, _translateTransform.Y);
        }

        private void UiElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_uiElement.IsMouseCaptured)
                return;

            var position = e.GetPosition(_uiElement);
            var dx = position.X - _startPosition.X;
            var dy = position.Y - _startPosition.Y;
            _translateTransform.X = _startTranslate.X + dx;
            _translateTransform.Y = _startTranslate.Y + dy;
        }

        private void UiElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Middle)
                return;

            _uiElement.ReleaseMouseCapture();
        }
    }
}
