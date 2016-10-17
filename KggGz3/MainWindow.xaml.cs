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
        private Vector2[] polygon;

        public MainWindow()
        {
            polygon = GetInputLines()
                .Select(x=>x.Replace(",", "."))
                .Select(x => x.Split(" \r\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                .Where(x => x.Length != 2)
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
                "1 1",
                "1 0",
                "0 0",
                "0 1"
            };


        private void kggCanvas_Initialized(object sender, EventArgs e)
        {
            var vertexes = Triangulator.Triangulate(polygon)
                .Use(x => Triangulator.SameSquareTriangulation(x, 0.1, 1))
                .Select(x => new MarkedEdgeTriangle(x))
                .ToList();
            var edges = TriangleGraphHelper.BuildEdges(vertexes).ToList();
            FromTo cuncurent;
            vertexes = TriangleGraphHelper.OrderByRadius(edges, vertexes, out cuncurent).ToList();
            var graphSpliter = new GraphSpliter(vertexes, edges, cuncurent);
            var triangles = graphSpliter.Split();
        }
    }
}
