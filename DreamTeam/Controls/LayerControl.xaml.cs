using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DreamTeam.Models.Abstract;
using DreamTeam.UserControls;

namespace DreamTeam.Controls
{
    public partial class LayerControl
    {
        private readonly ControlFactory _controlFactory = new ControlFactory();
        private readonly IDictionary<IPhysicalObject, UIElement> _objectControls = new Dictionary<IPhysicalObject, UIElement>();

        public LayerControl()
        {
            InitializeComponent();

            _scaleTransform.ScaleX = Settings.Default.Scale;
            _scaleTransform.ScaleY = Settings.Default.Scale;
        }

        public void Add(IPhysicalObject physicalObject)
        {
            if (physicalObject == null) throw new ArgumentNullException(nameof(physicalObject));

            var control = _controlFactory.CreateControl(physicalObject);
            _cnv.Children.Add(control);
            _objectControls.Add(physicalObject, control);

            physicalObject.PositionChanged += PositionChanged;
            PositionChanged(physicalObject);
        }

        public void Remove(IPhysicalObject physicalObject)
        {
            if (physicalObject == null) throw new ArgumentNullException(nameof(physicalObject));

            _cnv.Children.Remove(_objectControls[physicalObject]);
            _objectControls.Remove(physicalObject);

            physicalObject.PositionChanged -= PositionChanged;
        }

        private void PositionChanged(IPhysicalObject obj)
        {
            this.Do(() =>
            {
                var control = _objectControls[obj];
                Canvas.SetLeft(control, obj.Position.X - obj.Bounds.Width / 2);
                Canvas.SetTop(control, obj.Position.Y - obj.Bounds.Height / 2);
            });
        }
    }
}
