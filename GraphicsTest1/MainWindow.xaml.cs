using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace GraphicsTest1
{
    public partial class MainWindow : Window
    {
        Dictionary<Point, HexCell> hexGrid = new Dictionary<Point, HexCell>();

        public MainWindow()
        {
            InitializeComponent();

            DrawHexGrid();
        }

        private void DrawHexGrid()
        {
            float size = 35;
            float width = 2 * size;
            float height = MathF.Sqrt(3) * size;

            float horizontalOffset = 1.5f * size;
            float verticalOffset = height;

            SolidColorBrush stroke = System.Windows.Media.Brushes.Gray;
            SolidColorBrush fill = System.Windows.Media.Brushes.Aqua;

            SolidColorBrush highLightStroke = System.Windows.Media.Brushes.Black;
            SolidColorBrush highLightfill = System.Windows.Media.Brushes.Transparent;

            for (int i = 0; i <= 20; ++i)
            {
                for (int j = 0; j <= 50; ++j)
                {
                    string msg = i.ToString() + ":" + j.ToString();
                    Debug.WriteLine(msg);

                    float x = ((float)i * horizontalOffset * 2.0f) + 50;
                    float y = ((float)j * verticalOffset / 2.0f) + 50;
                    
                    if(j % 2 == 0)
                    {
                        DrawHexagon(x, y, size, stroke, fill);
                    }
                    else
                    {
                        DrawHexagon(x + horizontalOffset, y, size, stroke, fill);
                    }                    
                }
            }
        }

        private void DrawHexagon(float i, float j, float r, SolidColorBrush stroke, SolidColorBrush fill)
        {
            Point center = new Point();

            center.X = i; center.Y = j;

            Polygon polygon = new Polygon();
            polygon.Stroke = stroke;
            polygon.StrokeThickness = 2;

            PointCollection points = new PointCollection();

            points.Add( FlatHexCorner(center, r, 0));
            points.Add(FlatHexCorner(center, r, 1));
            points.Add(FlatHexCorner(center, r, 2));
            points.Add(FlatHexCorner(center, r, 3));
            points.Add(FlatHexCorner(center, r, 4));
            points.Add(FlatHexCorner(center, r, 5));
            points.Add(FlatHexCorner(center, r, 6));

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

        private void DrawCross(float x, float y, float r)
        {
            DrawHorizontalLine(x-r, y, r*2);
            DrawVerticalLine(x, y-r, r*2);
        }

        private void DrawHorizontalLine(float x, float y, float length)
        {
            var newLine = new Line
            {
                Stroke = System.Windows.Media.Brushes.Black,
                StrokeThickness = 1,
            };

            newLine.X1 = x;
            newLine.Y1 = y;
            newLine.X2 = x + length;
            newLine.Y2 = y;

            mainCanvas.Children.Add(newLine);
        }

        private void DrawVerticalLine(float x, float y, float length)
        {
            var newLine = new Line
            {
                Stroke = System.Windows.Media.Brushes.Black,
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
            Point p = e.GetPosition(mainCanvas);
            string pointText = p.ToString();
            MouseStateMovement.Text = pointText;
        }
    }
}
