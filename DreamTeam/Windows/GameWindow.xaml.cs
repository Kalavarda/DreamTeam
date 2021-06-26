using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DreamTeam.Processes;
using Point = DreamTeam.Models.Point;

namespace DreamTeam.Windows
{
    public partial class GameWindow
    {
        private readonly GameContext _gameContext;
        private readonly DragAndDropController _dragAndDropController;

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
            var hero = _gameContext.Game.Team.Heroes.FirstOrDefault(h => h.IsSelected);
            if (hero == null)
                return;

            if (e.ClickCount == 2)
            {
                var pos = e.GetPosition(_cnv);
                var x = (float)pos.X / Settings.Default.Scale;
                var y = (float)pos.Y / Settings.Default.Scale;
                var process = new MoveProcess(hero, new Point(x, y), _gameContext.CollisionDetector);
                _gameContext.Processor.Add(process);
            }
        }
    }

    public class DragAndDropController
    {
        private readonly UIElement _uiElement;
        private readonly TranslateTransform _translateTransform;
        private System.Windows.Point _startPosition;
        private System.Windows.Point _startTranslate;

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
