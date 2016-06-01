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

namespace KGG_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int canvasSize = 600;
        private int step = 40;
        public MainWindow()
        {
            InitializeComponent();
        }


        public void DrawPolygon(MyPolygon poly, Color fill)
        {
            var polygon = new Polygon();
            polygon.Points = new PointCollection(poly.Points.Select(a => new Point(Math.Round(a.X * step + canvasSize / 2),
                                                                                   Math.Round(-a.Y * step + canvasSize / 2))));
            polygon.Stroke = Brushes.Black;
            polygon.Fill = new SolidColorBrush(fill);
            polygon.StrokeThickness = 1;
            canvas.Children.Add(polygon);            
        }
        private void Window_Activated_1(object sender, EventArgs e)
        {
            var pol1 = new MyPolygon(
                new List<Vector>()
                {
                    new Vector(0,0),
                    new Vector(3,2),
                    new Vector(6,0),
                    new Vector(3,-2)
                });
            var pol2 = new MyPolygon(
                new List<Vector>()
                {
                    new Vector(4, 3),
                    new Vector(5, 3),
                    new Vector(5, -3),
                    new Vector(4, -3)
                });

            DrawPolygon(pol1, Colors.LightSalmon);
            DrawPolygon(pol2, Colors.HotPink);

            MyPolygon pol3;
            try
            {
                pol3 = MyPolygon.Intersection(pol1, pol2);
            }
            catch (DisjointPolygons)
            {
                return;
            }
            DrawPolygon(pol3, Colors.DarkBlue);
        }
    }
}
