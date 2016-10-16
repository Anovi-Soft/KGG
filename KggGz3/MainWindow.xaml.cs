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
        private Vector2[] polygon;

        public MainWindow()
        {
            polygon = File.ReadAllLines("input.txt")
                .Select(x => x.Split(" \r\t".ToCharArray()))
                .Where(x => x.Length != 2)
                .Select(x => x.Select(double.Parse))
                .Select(x => new Vector2(x.First(), x.Last()))
                .ToArray();
            InitializeComponent();
        }
        

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
