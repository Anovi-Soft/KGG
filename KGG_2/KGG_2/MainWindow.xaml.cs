using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading; 
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KGG_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int canvasSize = 600;
        private int step = 1;
        private double a, b, c, d;
        private double E = 1.41421;
        private bool reverse;
        public MainWindow()
        {
            InitializeComponent();
            TA.Focus();
        }
        private void DrawPoint(Vector point)
        {
            double x = Math.Round(point.X * step + canvasSize / 2);
            double y = Math.Round(-point.Y * step + canvasSize / 2);
            Point _point = new Point(x, y);
            Ellipse elipse = new Ellipse();

            elipse.Width = 1;
            elipse.Height = 1;

            elipse.StrokeThickness = 1;
            elipse.Stroke = Brushes.Black;
            elipse.Margin = new Thickness(_point.X, _point.Y, 0, 0);

            canvas.Children.Add(elipse);
        }
        private Vector GetCentreHyperbola()
        {
            return new Vector(0, d - b * c);
        }

        private double GetDistanceFromCentreToTop()
        {
            return E * Math.Sqrt(Math.Abs(a * c));
        }


        private Vector[] GetNeighbors(Vector point, bool secondBranch = false)
        {
            return new Vector[] 
            { 
                new Vector(point.X+(secondBranch? -1.0 : 1.0)/step, point.Y),
                new Vector(point.X, point.Y +(secondBranch ^ reverse ? 1.0 : -1.0)/step),
                new Vector(point.X+(secondBranch? -1.0 : 1.0)/step, point.Y +(secondBranch ^ reverse ? 1.0 : -1.0)/step)
            }; 
        }
        private double CheckFunction(Vector point, Vector F1, Vector F2, double distance)
        {
            return Math.Abs(Math.Abs(new Segment(point, F1).Length() - new Segment(point, F2).Length()) - 2 * distance);
        }
        private double GetYFromX(double x)
        {
            return c * (a / x - b) + d;
        }
        private Vector GetBestNeighbor(Vector point, Vector F1, Vector F2, double distance, bool secondBranch = false)
        {
            var neighbors = GetNeighbors(point, secondBranch);
            var idOfSmallest = 0;
            var smallestValue = CheckFunction(neighbors[0], F1, F2, distance);
            for (int i = 1; i < neighbors.Length; i++)
            {
                var potencialSmallest = CheckFunction(neighbors[i], F1, F2, distance);
                if (potencialSmallest < smallestValue)
                {
                    smallestValue = potencialSmallest;
                    idOfSmallest = i;
                }
            }
            return neighbors[idOfSmallest];            
        }

        private bool InCanvas(Vector point)
        {
            return -canvasSize / 2 / step <= point.X && point.X <= canvasSize / 2 / step &&
                -canvasSize / 2 / step <= point.Y && point.Y <= canvasSize / 2 / step;
        }
        private int Sign(double value)
        {
            return (int)((value) / Math.Abs(value));
        }

        private void BDraw_Click(object sender = null, RoutedEventArgs e = null)
        {
            canvas.Children.Clear();
            try
            {
                a = Convert.ToDouble(TA.Text);
                b = Convert.ToDouble(TB.Text);
                c = Convert.ToDouble(TC.Text);
                d = Convert.ToDouble(TD.Text);
                reverse = a<0 ^ c<0;
            }
            catch (FormatException)
            {
                MessageBox.Show("Text must be Double");
                return;
            }
            var centreHyperbola = GetCentreHyperbola();
            var distance = GetDistanceFromCentreToTop();
            var sign = Sign(a * c);
            var F1 = new Vector(-E * sign * Math.Sqrt(Math.Abs(a * c)), -E * Math.Sqrt(Math.Abs(a * c)) - b * c + d);
            var F2 = new Vector(E * sign * Math.Sqrt(Math.Abs(a * c)), E * Math.Sqrt(Math.Abs(a * c)) - b * c + d);
            var top1 = new Segment(centreHyperbola, F1).ResizeTo(distance).End;
            var top2 = new Segment(centreHyperbola, F2).ResizeTo(distance).End;

            var X = -canvasSize / 2 / step;
            Vector stepPoint = new Vector(X, GetYFromX(X));
            while (InCanvas(stepPoint))
            {
                DrawPoint(stepPoint);
                stepPoint = GetBestNeighbor(stepPoint, F1, F2, distance);
            }

            stepPoint = new Vector(-X - 1.0 / step, GetYFromX(-X));
            while (InCanvas(stepPoint))
            {
                DrawPoint(stepPoint);
                stepPoint = GetBestNeighbor(stepPoint, F1, F2, distance, true);
            }

        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                BDraw_Click();
        }
    }
}
