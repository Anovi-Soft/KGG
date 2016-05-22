using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using KGG;

namespace KGG_Task_1
{

    public partial class MainWindow : Window
    {
        private KggCanvas.Color color = KggCanvas.Color.Blue;
        private KggCanvas.Color colorAxis = KggCanvas.Color.Black;
        private double previousLeft = 0;
        private double previousRight = 10;
        private Func<double, double> previousFunc = Math.Sin;
        public MainWindow()
        {
            InitializeComponent();
            UpdateCanvas();
        }

        public void UpdateCanvas(double left, double right, Func<double, double> func)
        {
            UpdateCanvas(left, right);
        }
        public void UpdateCanvas(double left, double right)
        {
            previousLeft = Math.Min(left, right);
            previousRight = Math.Max(left, right);
            UpdateCanvas();
        }
        public void UpdateCanvas()
        {
            var width = (int) kggCanvas.Width;
            var height = (int) kggCanvas.Height - 20;
            var step = (previousRight - previousLeft) / width;
            var points = Enumerable.Range(0, width)
                .Select(x=>previousLeft + x * step)
                .Select(x => new Vector2(x, previousFunc(x)));

            var maxY = points.Max(p => p.Y);
            var minY = points.Min(p => p.Y);
            var realHeight = maxY - minY;
            var drawPoints = points.Select(p => new Vector2((p.X - previousLeft)/step,  height * (maxY - p.Y) / realHeight));


            kggCanvas.Clear();

            if (previousLeft <= 0 && previousRight >= 0)
            {
                var x = (int)(width - width * previousRight / (previousRight - previousLeft));
                kggCanvas.DrawLine(x, 0, x, height, colorAxis);
                kggCanvas.DrawLine(x-5, 10, x, 0, colorAxis);
                kggCanvas.DrawLine(x+5, 10, x, 0, colorAxis);


                var segmentsY = Enumerable.Range((int) Math.Ceiling(minY), (int) (Math.Floor(maxY) - Math.Ceiling(minY) + 1))
                    .Select(p => (int)(height*(maxY - p)/realHeight));
                foreach (var y in segmentsY)
                {
                    kggCanvas.DrawLine(x-5, y, x+5, y, colorAxis);
                }
            }
            if (minY <= 0.1 && maxY >= 0)
            {
                var y = (int)(height * maxY / realHeight);
                kggCanvas.DrawLine(0, y, width, y, colorAxis);
                kggCanvas.DrawLine(width - 10, y - 5, width, y, colorAxis);
                kggCanvas.DrawLine(width - 10, y + 5, width, y, colorAxis);

                var segmentsX = Enumerable.Range((int)Math.Ceiling(previousLeft), (int)(Math.Floor(previousRight) - Math.Ceiling(previousLeft) + 1))
                    .Select(p => (int)((p - previousLeft) / step));
                foreach (var x in segmentsX)
                {
                    kggCanvas.DrawLine(x, y-5, x, y+5, colorAxis);
                }
            }

            var previousPoint = drawPoints.First();
            foreach (var p in drawPoints.Skip(1))
            {
                kggCanvas.DrawLine(previousPoint, p, color);
                previousPoint = p;
            }
            kggCanvas.Update();
        }


        double? previousShiftX;
        private void kggCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            previousShiftX = e.GetPosition(this).X;
        }
        private void kggCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            previousShiftX = null;
        }

        private void kggCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (previousShiftX == null) return;
            var x = (double)(previousShiftX - e.GetPosition(this).X)* 0.001*(previousRight - previousLeft);
            previousLeft += x;
            previousRight += x;
            previousShiftX = e.GetPosition(this).X;
            textBoxLeft.Text = $"{previousLeft:N1}";
            textBoxRight.Text = $"{previousRight:N1}";
            UpdateCanvas();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            double left, right;
            if (!double.TryParse(textBoxLeft.Text, out left) ||
                !double.TryParse(textBoxRight.Text, out right))
            {
                MessageBox.Show("Text must be double", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            UpdateCanvas(left, right);
        }
    }
}
