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
using KGG;

namespace KGG_Task_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int size = 20;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void UpdateCanvas()
        {
            var firstPolygon = GetPolygon(GetFirstPoints(), KggCanvas.Color.Green);
            var secondPolygon = GetPolygon(GetSecondPoints(), KggCanvas.Color.Aqua);
            var result = firstPolygon.Cut(secondPolygon);

            kggCanvas.Clear();
            foreach (var poly in result)
            {
                poly.Color = KggCanvas.Color.Red;
                kggCanvas.DrawPolygon(poly, size);
            }
            kggCanvas.DrawPolygonLines(firstPolygon,size);
            kggCanvas.DrawPolygonLines(secondPolygon, size);
            kggCanvas.Update();
        }

        private Polygon GetPolygon(List<Vector2> points, KggCanvas.Color color = null)
        {
            return new Polygon(points.ToList()
                ,color);
        }

        private List<Vector2> GetFirstPoints() => new List<Vector2>()
        {
            new Vector2(0,0),
            new Vector2(3,2),
            new Vector2(6,0),
            new Vector2(3,-2)
        };
        private List<Vector2> GetSecondPoints() => new List<Vector2>
        {
            new Vector2(4, 3),
            new Vector2(7, 3),
            new Vector2(7, -3),
            new Vector2(4, -3)
        };

        private void Window_Initialized(object sender, EventArgs e)
        {
            UpdateCanvas();
        }
    }
}
