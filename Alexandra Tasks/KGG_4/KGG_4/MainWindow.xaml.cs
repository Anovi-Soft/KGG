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

namespace KGG_4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private void DrawPoint(Vector point, Brush brush)
        {
            //double x = Math.Round(point.X * step + canvasSize / 2);
            //double y = Math.Round(-point.Y * step + canvasSize / 2);
            Point _point = new Point(point.X+25, point.Y+25);
            Ellipse elipse = new Ellipse();

            elipse.Width = 1;
            elipse.Height = 1;

            elipse.StrokeThickness = 1;
            elipse.Stroke = brush;
            elipse.Margin = new Thickness(_point.X, _point.Y, 0, 0);

            canvas.Children.Add(elipse);
        }

        //функция z=f(x,y)
        double f(double x, double y)
        {
            return Math.Sqrt(Math.Pow(x, 2) - Math.Pow(y, 2) + 5); 
        }

        // используем изометрию
        double coord_x(double x, double y, double z)
        { return (y - x) * Math.Sqrt(3.0) / 2; }

        double coord_y(double x, double y, double z)
        { return (x + y) / 2 - z; }

        public MainWindow()
        {
            InitializeComponent();
            int mx = (int)canvas.Width - 50, my = (int)canvas.Height - 50;
	        double x,y,z,
                xx,yy,
                maxx,maxy,minx,miny, 
                size = 3.5,
                x1=size,x2=-size,y1=-size,y2=size;// Replace a,b,c,d
            int i, j, n=600, m=mx*2;
            int[] top = new int[mx], 
                bottom = new int[mx];
            minx=10000; maxx=-minx;
            miny=minx;   maxy=maxx;


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
                        DrawPoint(new Vector(xx, yy), Brushes.Violet);
                        bottom[intxx] = (int)(yy);
                    }
                    if (yy < top[intxx])
                    {
                        DrawPoint(new Vector(xx, yy), Brushes.DeepSkyBlue);
                        top[intxx] = (int)(yy);
                    }
                }
            }
        }
    }
}
