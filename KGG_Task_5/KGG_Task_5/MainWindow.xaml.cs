using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KGG;
using Polygon = System.Windows.Shapes.Polygon;

namespace KGG_Task_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBoxUInt(object sender, TextChangedEventArgs e) =>
            GuiUtil.TextBoxValue<uint>(sender as TextBox, e, uint.TryParse);

        private void TextBoxVector3(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            var countSpliter = tb.Text.Count(a => a == ';');
            while (countSpliter < 2)
            {
                tb.Text += ";";
                countSpliter = tb.Text.Count(a => a == ';');
            }
            if (countSpliter > 2)
            {
                var textChange = e.Changes.First();
                var iAddedLength = textChange.AddedLength;
                var iOffset = textChange.Offset;
                tb.Text = tb.Text.Remove(iOffset, iAddedLength);
                return;
            }
            GuiUtil.TextBoxValue<Vector3>(tb , e, Vector3.TryParse);
        }

        public void DrawFigure(Triangle triangle)
        {
            var polygon = new Polygon();
            new[]{triangle.A, triangle.B, triangle.C}
                .Select(a => new Point(300 + a.XX * 50, 300 + a.YY * 50))
                .ToList<Point>()
                .ForEach(polygon.Points.Add);
            polygon.Fill = polygon.Stroke = triangle.Color;
            polygon.StrokeThickness = 0.35;
            canvas.Children.Add(polygon);

        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var triLvl = uint.Parse(triLevel.Text);
            var allTriangles = new List<Triangle>();
            var cube = new Cube(Vector3.Parse(cubeFrom.Text),
                Vector3.Parse(cubeTo.Text),
                new Brush[]{
                    Brushes.OrangeRed,
                    Brushes.Green,
                    Brushes.Blue,
                    Brushes.Chartreuse,
                    Brushes.Violet,
                    Brushes.Yellow});
            allTriangles.AddRange(cube.Triangulation(triLvl));

            allTriangles.Sort((a,b) => a.ZZ.CompareTo(b.ZZ));
            allTriangles.ForEach(DrawFigure);
            //var triangle = new Triangle(new Vector3(120, 200, 20), new Vector3(160, 250, 20), new Vector3(200, 200, 20))
            //{
            //    Color = KggCanvas.Color.Blue
            //};

            //triangle.Triangulation(uint.Parse(triLevel.Text)).ForEach(kggCanvas.DrawTriangle);
        }
    }
}
