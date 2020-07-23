using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UserControls
{
    /// <summary>
    /// InfiniteMoveControl.xaml 的交互逻辑
    /// </summary>
    public partial class InfiniteMoveControl : UserControl
    {
        public static readonly DependencyProperty PointProperty = DependencyProperty.Register(
            "Point", typeof(Point), typeof(InfiniteMoveControl), new PropertyMetadata(default(Point), OnPointChanged));

        private static void OnPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (InfiniteMoveControl) d;
            control.Point = (Point) e.NewValue;
        }

        public Point Point
        {
            get { return (Point) GetValue(PointProperty); }
            set { SetValue(PointProperty, value); }
        }

        private Point? _lastMousePoint;
        private Point _lastPoint;

        public InfiniteMoveControl()
        {
            InitializeComponent();

            MouseLeftButtonDown += OnMouseLeftButtonDown;
            MouseMove += OnMouseMove;
            MouseLeftButtonUp += OnMouseLeftButtonUp;
            MouseLeave += OnMouseLeave;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            _lastMousePoint = null;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _lastMousePoint = null;
            ReleaseMouseCapture();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_lastMousePoint == null) return;
            var ptr = e.GetPosition(this) - _lastMousePoint.Value;
            var x = ptr.X + _lastPoint.X;
            var y = ptr.Y + _lastPoint.Y;

            Point = new Point(x, y);
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _lastMousePoint = e.GetPosition(this);
            _lastPoint = Point;
            CaptureMouse();
        }
    }
}