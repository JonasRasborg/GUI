using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FerieApp
{
    public partial class DrawPackageList : Window
    {
        Point startPos;
        Point currPos;
        public DrawPackageList()
        {
            InitializeComponent();
        }

        private void keyDownEventHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.B:
                    Resources["myBrush"] = new SolidColorBrush(Colors.Black);
                    break;

                case Key.R:
                    Resources["myBrush"] = new SolidColorBrush(Colors.Red);
                    break;

                case Key.G:
                    Resources["myBrush"] = new SolidColorBrush(Colors.Green);
                    break;

                case Key.Y:
                    Resources["myBrush"] = new SolidColorBrush(Colors.Yellow);
                    break;
            }
        }

        private void mouseDownEventHandler(object sender, MouseButtonEventArgs e)
        {
            startPos = e.GetPosition(mainCanvas);

            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                var ellipse = new Ellipse();

                ellipse.StrokeThickness = 2;
                ellipse.Stroke = Brushes.Black;

                ellipse.Width = 1.0;
                ellipse.Height = 1.0;

                ellipse.Fill = (Brush)Resources["myBrush"];

                Canvas.SetLeft(ellipse, e.GetPosition(mainCanvas).X - (ellipse.Width / 2));
                Canvas.SetTop(ellipse, e.GetPosition(mainCanvas).Y - (ellipse.Height / 2));
                mainCanvas.Children.Add(ellipse);
            }

            if (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
            {
                var rectangle = new Rectangle();

                rectangle.StrokeThickness = 2;
                rectangle.Stroke = Brushes.Black;

                rectangle.Width = 1.0;
                rectangle.Height = 1.0;

                rectangle.Fill = (Brush)Resources["myBrush"];

                Canvas.SetLeft(rectangle, e.GetPosition(mainCanvas).X - (rectangle.Width / 2));
                Canvas.SetTop(rectangle, e.GetPosition(mainCanvas).Y - (rectangle.Height / 2));
                mainCanvas.Children.Add(rectangle);
            }

            if (Keyboard.Modifiers == 0)
            {
                Line l = new Line(); ;
                l.Stroke = Brushes.Black;
                l.StrokeThickness = 100.0;
                l.X1 = startPos.X;
                l.Y1 = currPos.Y;
                l.X2 = currPos.X + 1;
                l.Y2 = currPos.Y + 1;
                mainCanvas.Children.Add(l);
            }
        }

        private void mouseMoveEventHandler(object sender, MouseEventArgs e)
        {
            Shape r = (Shape)(e.Source as Ellipse) ?? e.Source as Rectangle;

            Line l = e.Source as Line;

            currPos = e.GetPosition(mainCanvas);

            if (r != null)
            {

                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    r.Width = e.GetPosition(r).X + e.GetPosition(r).X;
                    r.Height = e.GetPosition(r).Y + e.GetPosition(r).Y;

                    if (r.Width < 3.0)
                        r.MinWidth = 3.0;
                    if (r.Height < 3.0)
                        r.MinHeight = 3.0;
                }
            }

            if (l != null)
            {
                currPos = e.GetPosition(l);
                l.X2 = currPos.X;
                l.Y2 = currPos.Y;
            }
        }

        private void mouseUpEventHandler(object sender, MouseButtonEventArgs e)
        {
            Shape r = (Shape)(e.Source as Ellipse) ?? e.Source as Rectangle;

            if (r != null)
            {
                if (Mouse.LeftButton == MouseButtonState.Released)
                {
                    r.MinWidth = e.GetPosition(r).X + e.GetPosition(r).X;
                    r.MaxWidth = r.MinWidth;

                    r.MinHeight = e.GetPosition(r).Y + e.GetPosition(r).Y;
                    r.MaxHeight = r.MinHeight;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point pos = Mouse.GetPosition(this);
            stXCoordMouse.Text = pos.X.ToString();
            stYCoordMouse.Text = pos.Y.ToString();
        }
    }
}
