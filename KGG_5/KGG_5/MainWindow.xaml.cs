using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace KGG_5
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void DrawFigure(Figure figure)
        {
            var polygon = new Polygon();
            figure.Vertex()
                .Select(a => new Point(300 + a.XX * 50, 300 + a.YY * 50))
                .ToList<Point>()
                .ForEach(polygon.Points.Add);
            polygon.Stroke = figure.Color();
            polygon.Fill = figure.Color();
            polygon.StrokeThickness = 0.35;
            canvas.Children.Add(polygon);   

        }
        public MainWindow()
        {
            InitializeComponent();
            List<Figure> a = new Cube(new Vector(0, -2, -2), new Vector(2, 0, 0), new Brush[]{
                Brushes.OrangeRed, 
                Brushes.Green,
                Brushes.Blue,
                Brushes.Black, 
                Brushes.Violet, 
                Brushes.Yellow})
                .Triangulation(10);

            //a.AddRange( new Cube(new Vector(-0.5, 1.5, -0.75), new Vector(-1.5, 0.5, 0.25), new Brush[]{
            //    Brushes.OrangeRed, 
            //    Brushes.Green,
            //    Brushes.Blue,
            //    Brushes.Black, 
            //    Brushes.Violet, 
            //    Brushes.Yellow})
            //    .Triangulation(10));
            //a.AddRange(new Thor(new Vector(0, 0, -0.2),
            //    1,
            //    0.25,
            //    (Color)ColorConverter.ConvertFromString("#FFFF0000"),
            //    (Color)ColorConverter.ConvertFromString("#FF008000"))
            //    .Parts(90, 36));
            a.AddRange(new Thor(new Vector(2.75, 0.5, -0.5),
                1.2,
                0.3,
                (Color)ColorConverter.ConvertFromString("#FF80FF80"),
                (Color)ColorConverter.ConvertFromString("#FF800080"))
                .Parts(120, 60));
            a.Sort((t1, t2) => t1.ZZ().CompareTo(t2.ZZ()));
            a.ForEach(DrawFigure);
        }
    }
}
