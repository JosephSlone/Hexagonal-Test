using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Numerics;


namespace GraphicsTest1
{
    public partial class MainWindow : Window
    {
        float canvasX = 2400;
        float canvasY = 2400;

        float hexSize = 35;

        Dictionary<Vector3, HexCell> hexGrid = new Dictionary<Vector3, HexCell>();

        public MainWindow()
        {
            InitializeComponent();

            mainCanvas.Height = canvasY; mainCanvas.Width = canvasX;


            DrawHexGrid();
        }

        private void DrawHexGrid()
        {
            
            float width = 2 * hexSize;
            float height = MathF.Sqrt(3) * hexSize;

            float horizontalOffset = 1.5f * hexSize;
            float verticalOffset = height;

            SolidColorBrush stroke = System.Windows.Media.Brushes.Gray;
            SolidColorBrush fill = System.Windows.Media.Brushes.Aqua;

            SolidColorBrush highLightStroke = System.Windows.Media.Brushes.Black;
            SolidColorBrush black = System.Windows.Media.Brushes.Black;
            SolidColorBrush highLightfill = System.Windows.Media.Brushes.Transparent;

            Point point = ToCanvasCoords(ToGridCoordinates(new Point(0, 0)));
            DrawHexagon(point, hexSize, stroke, fill);
            DrawCross(point, 10, black);

            point = ToCanvasCoords(ToGridCoordinates(new Point(0, 1)));
            DrawHexagon(point, hexSize, stroke, fill);
            DrawCross(point, 10, black);


        }

        private Point ToCanvasCoords(Point point)
        {
            point.X += canvasX / 2.0f;
            point.Y += canvasY / 2.0f;

            return point;
        }

        private Point FromCanvasCoords(Point point)
        {
            point.X -= canvasX / 2.0f;
            point.Y -= canvasY / 2.0f;

            return point;
        }



        private Point ToGridCoordinates(Point point)
        {
            Point coords = new()
            {
                X = hexSize * point.X * 0.75f,
                Y = MathF.Sqrt(3) / 2 * hexSize * (point.X / 2 + point.Y)
            };

            return coords;
        }

        private Point FromGridCoordinates(Point point)
        {
            return point;
        }

        private void DrawHexagon(Point center, float r, SolidColorBrush stroke, SolidColorBrush fill)
        {
            Polygon polygon = new Polygon();
            polygon.Stroke = stroke;
            polygon.StrokeThickness = 2;

            PointCollection points = new PointCollection
            {
                FlatHexCorner(center, r, 0),
                FlatHexCorner(center, r, 1),
                FlatHexCorner(center, r, 2),
                FlatHexCorner(center, r, 3),
                FlatHexCorner(center, r, 4),
                FlatHexCorner(center, r, 5),
                FlatHexCorner(center, r, 6)
            };

            polygon.Points = points;
            polygon.Fill = fill;
            mainCanvas.Children.Add(polygon);

        }

        private Point FlatHexCorner(Point center, float size, int i)
        {
            Point point = new Point();

            float angle_deg = 60 * i;
            float angle_rad = float.Pi/180.0f * angle_deg;

            point.X = center.X + size * MathF.Cos(angle_rad);
            point.Y = center.Y + size * MathF.Sin(angle_rad);
            return point;
        }

        private void DrawLine(Point startPoint, Point endPoint)
        {
            var newLine = new Line
            {
                Stroke = System.Windows.Media.Brushes.LightBlue,
                StrokeThickness = 1,
                X1 = startPoint.X, 
                Y1 = startPoint.Y,
                X2 = endPoint.X,
                Y2 = endPoint.Y
            };

            mainCanvas.Children.Add(newLine);
        }
        private void DrawSquare(int i, int j, int r)
        {
            var square = new Rectangle()
            {
                Stroke = System.Windows.Media.Brushes.Gold,
                StrokeThickness = 0.75f,
                Width = r * 2,
                Height = r * 2,
            };
            mainCanvas.Children.Add(square);
            square.SetValue(Canvas.LeftProperty, (double)(i - r));
            square.SetValue(Canvas.TopProperty, (double)(j - r));
        }

        private void DrawCircle(int i, int j, int r)
        {
            var circle = new Ellipse
            {
                Stroke = System.Windows.Media.Brushes.Blue,
                StrokeThickness = 0.75f,
                Width = r*2,
                Height = r*2,
            };
            mainCanvas.Children.Add(circle);
            circle.SetValue(Canvas.LeftProperty, (double)(i - r));
            circle.SetValue(Canvas.TopProperty, (double)(j - r));
        }

        private void DrawCross(Point point, float r, SolidColorBrush? brush = null)
        {
            if (brush is null)
            {
                brush = System.Windows.Media.Brushes.Black;
            }

            DrawHorizontalLine(point.X-r, point.Y, r*2, brush);
            DrawVerticalLine(point.X, point.Y-r, r*2, brush);
        }

        private void DrawHorizontalLine(double x, double y, float length, SolidColorBrush brush)
        {
            var newLine = new Line
            {
                Stroke = brush,
                StrokeThickness = 1,
            };

            newLine.X1 = x;
            newLine.Y1 = y;
            newLine.X2 = x + length;
            newLine.Y2 = y;

            mainCanvas.Children.Add(newLine);
        }

        private void DrawVerticalLine(double x, double y, float length, SolidColorBrush brush)
        {
            var newLine = new Line
            {
                Stroke = brush,
                StrokeThickness = 1,
            };

            newLine.X1 = x;
            newLine.Y1 = y;
            newLine.X2 = x;
            newLine.Y2 = y+length;

            mainCanvas.Children.Add(newLine);
        }

        private void MainCanvasMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MouseState.Text = "Mouse Entered Canvas";
        }

        private void MainCanvasMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MouseState.Text = "Mouse Left Canvas";
            MouseStateMovement.Text = "";
        }

        private void MainCanvasMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point p = FromCanvasCoords(e.GetPosition(mainCanvas));
            string pointText = p.ToString();
            MouseStateMovement.Text = pointText;
        }
    }
}
