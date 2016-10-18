using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using KGG;

namespace KggGz3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string InputTxt = "input.txt";
        private const int Coef = 10;
        private Vector2[] polygon;
        private readonly KggCanvas.Color[] colors = {KggCanvas.Color.Green, KggCanvas.Color.Red};

        public MainWindow()
        {
            polygon = GetInputLines()
                .Select(x => x.Replace(".", ","))
                .Select(x => x.Split(" \r\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                .Where(x => x.Length == 2)
                .Select(x => x.Select(double.Parse))
                .Select(x => new Vector2(x.First(), x.Last()))
                .ToArray();
            InitializeComponent();
        }

        private static string[] GetInputLines() =>
            File.Exists(InputTxt)
                ? File.ReadAllLines(InputTxt)
                : new[]
                {
                    "10 10",
                    "0 0",
                    "10 -10",
                    "-10 -10",
                    "-10 10"
                };


        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                Solve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void Solve()
        {
            var vertexes = Triangulator.Triangulate(polygon)
                .Use(x => Triangulator.SameSquareTriangulation(x, 0.3, 2))
                .ToList();

            //foreach (var t in vertexes.Select(x => AddColor(x, colors.First())))
            //    kggCanvas.DrawTriangle(t.A, t.B, t.C, t.Color, Coef);

            var edges = TriangleGraphHelper.BuildEdges(vertexes).ToList();
            var temp = TriangleGraphHelper.OrderByRadius(edges, vertexes).ToList();
            var graphSpliter = new GraphSpliter(temp[0].Value.ToList(), temp[1].Value.ToList(), edges, new FromTo(temp[0].Key, temp[1].Key));
            var trianglesGrops = graphSpliter.Split().Zip(colors, (list, color) => list.Select(x => AddColor(x, color)));

            var triangles = trianglesGrops.SelectMany(x => x).ToList();
            foreach (var t in triangles)
                kggCanvas.DrawTriangle(t.A, t.B, t.C, t.Color, Coef);
        }
        
        private static Triangle AddColor(Triangle triangle, KggCanvas.Color color)
        {
            triangle.Color = color;
            return triangle;
        }
    }
}
