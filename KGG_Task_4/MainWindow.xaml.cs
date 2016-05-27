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

        public void UpdateWindow()
        {
            var mx = (int)kggCanvas.Width - 50;
            var my = (int)kggCanvas.Height - 50;
            double x, y, z,
                xx, yy,
                maxx, maxy, minx, miny,
                size = 4,
                x1 = size, x2 = -size, y1 = -size, y2 = size;// Replace a,b,c,d
            int i, j, n = 1200, m = mx * 2;
            int[] top = new int[mx],
                bottom = new int[mx];
            minx = 10000; maxx = -minx;
            miny = minx; maxy = maxx;


            for (i = 0; i <= n; ++i)
            {
                x = x2 + i * (x1 - x2) / n;

                for (j = 0; j <= n; ++j)
                {
                    y = y2 + j * (y1 - y2) / n;
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


            for (i = 0; i < mx; ++i)
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
                    if (intxx == mx)
                        continue;
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
            //Math.Exp(-Math.Pow((x - 4) * (x - 4) + (y - 4) * (y - 4), 2)/1000) + Math.Exp(-Math.Pow((x + 4) * (x + 4) + (y + 4) * (y + 4), 2)/1000) +
            //         0.1*Math.Exp(-Math.Pow((x + 4) * (x + 4) + (y + 4) * (y + 4), 2)) + 0.1*Math.Exp(-Math.Pow((x - 4) * (x - 4) + (y - 4) * (y - 4), 2));
            Math.Sin(x * x * y);

        // используем изометрию
        double coord_x(double x, double y, double z)
        { return (y - x) * Math.Sqrt(3.0) / 2; }

        double coord_y(double x, double y, double z)
        { return (x + y) / 2 - z; }
    }
}
