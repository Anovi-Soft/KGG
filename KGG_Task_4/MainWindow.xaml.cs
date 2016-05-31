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
using KGG;
using KGG_Helper;

namespace KGG_Task_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateWindow();
        }

        private void UpdateWindowNew()
        {
            var width = (int)kggCanvas.Width - 50;
            var height = (int)kggCanvas.Height - 50;
            var min = new Vector2(-4,-4);
            var max = new Vector2(4,4);
            var linesCount = 120;
            var stepsCount = width*2;
            Func<double, double, double> func = (x, y) => Math.Sin(x * x * y);
            var points = GetPoints(linesCount, stepsCount, min, max, func)
                .ToList();
            var maxX = points.Max(x => x.XX);
            var minX = points.Min(x => x.XX);
            var maxY = points.Max(x => x.YY);
            var minY = points.Min(x => x.YY);

        }

        public static IEnumerable<Vector3I> GetPoints(int linesCount, int stepsCount,
            Vector2 min, Vector2 max,
            Func<double, double, double> func) =>
                RangeFormat(linesCount, min, max)
                .SelectMany(x => RangeFormat(stepsCount, min, max)
                    .Select(y => new Vector3I(x,y,func(x,y))));
        

        private static IEnumerable<double> RangeFormat(int n, Vector2 min, Vector2 max) => 
            Enumerable.Range(0, n)
            .Select(x => Format(min.X, max.X, x, n));

        public static double Format(double min, double max, int i, double n) =>
            min + i*(max - min)/n;
        public void UpdateWindow()
        {
            var mx = (int)kggCanvas.Width - 50;
            var my = (int)kggCanvas.Height - 50;
            double x, y, z,
                xx, yy,
                maxx, maxy, minx, miny,
                size = 4,
                x1 = size, x2 = -size, y1 = -size, y2 = size;
            int i, j, n = 30, m = mx * 10;
            int[] top = new int[mx+1],
                bottom = new int[mx+1];
            minx = 10000; maxx = -minx;
            miny = minx; maxy = maxx;


            for (i = 0; i <= n; ++i)
            {
                x = x2 + i * (x1 - x2) / n;

                for (j = 0; j <= m; ++j)
                {
                    y = y2 + j * (y1 - y2) / m;
                    z = f(x, y);
                    xx = coord_x(x, y, z);
                    yy = coord_y(x, y, z);
                    if (xx > maxx)
                        maxx = xx;
                    if (yy > maxy)
                        maxy = yy;
                    if (xx < minx)
                        minx = xx;
                    if (yy < miny)
                        miny = yy;
                }
            }


            for (i = 0; i <= mx; ++i)
            {
                top[i] = my;
                bottom[i] = 0;
            }

            for (i = 0; i <= n; ++i)
            {
                x = x2 + i * (x1 - x2) / n;
                for (j = 0; j <= m; ++j)
                {
                    y = y2 + j * (y1 - y2) / m;
                    z = f(x, y);
                    xx = coord_x(x, y, z);
                    yy = coord_y(x, y, z);

                    xx = (xx - minx) /
                        (maxx - minx) * mx;
                    yy = (yy - miny) /
                        (maxy - miny) * my;
                    int intxx = (int)(xx);
                    if (yy > bottom[intxx])
                    {
                        kggCanvas.DrawPoint(new Vector2(xx, yy), KggCanvas.Color.Pink);
                        bottom[intxx] = (int)(yy);
                    }
                    if (yy < top[intxx])
                    {
                        kggCanvas.DrawPoint(new Vector2(xx, yy), KggCanvas.Color.Blue);
                        top[intxx] = (int)(yy);
                    }
                }
            }

            kggCanvas.Update();
        }

        //функция z=f(x,y)
        double f(double x, double y) =>
            Math.Sin(x * x * y);

        // используем изометрию
        double coord_x(double x, double y, double z) => (y - x) * Math.Sqrt(3.0) / 2;

        double coord_y(double x, double y, double z) => (x + y) / 2 - z;
    }

    public static class ExtendIEnumerable
    {
    }
}
