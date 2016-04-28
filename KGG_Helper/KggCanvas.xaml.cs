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

namespace KGG
{
    /// <summary>
    /// Interaction logic for KggCanvas.xaml
    /// </summary>
    public partial class KggCanvas : UserControl
    {
        private WriteableBitmap bitmap;
        public int MapWidth => (int)image.Width;
        public int MapHeight => (int)image.Height;
        
        public KggCanvas()
        {
            InitializeComponent();
            Clear();
            image.Source = bitmap;
        }
        public void Clear()
        {
            bitmap = new WriteableBitmap(MapWidth, MapHeight, 96, 96, PixelFormats.Bgra32, null);
        }

        public void SaveAs(string path)
        {
            throw new NotSupportedException();
        }
        public void DrawPoint(int x, int y, byte[] color)
        {
            if (1 >= x || x >= MapWidth - 1 || 1 >= y || y >= MapHeight - 1) return;
            bitmap.WritePixels(new Int32Rect(x, y, 1, 1), color, 4, 0);
        }

        public void DrawPoint(Vector2 point, Color color)
        {
            DrawPoint((int)point.X, (int)point.Y, color);
        }
        public void DrawPoint(int x, int y, Color color)
        {
            DrawPoint(x, y, (byte[])color);
        }
        public void DrawPoint(int x, int y)
        {
            DrawPoint(x, y, Color.Black);
        }

        public void DrawTriangle(Triangle triangle)
        {
            var points = (new[] { triangle.A, triangle.B, triangle.C }).ToList();
            points.Sort((p1, p2) => p1.YY.CompareTo(p2.YY));
            var A = points[0];
            var B = points[1];
            var C = points[2];

            for (var sy = A.YY; sy <= C.YY; sy += 1)
            {
                var x1 = A.XX + (sy - A.YY) * (C.XX - A.XX) / (C.YY - A.YY);
                double x2;
                if (sy < B.YY)
                {
                    x2 = A.XX + (sy - A.YY) * (B.XX - A.XX) / (B.YY - A.YY);
                }
                else
                {
                    if (C.YY == B.YY)
                        x2 = B.XX;
                    else
                        x2 = B.XX + (sy - B.YY) * (C.XX - B.XX) / (C.YY - B.YY);
                }
                if (x1 > x2)
                    Swap(ref x1, ref x2);
                drawHorizontalLine(sy, x1, x2, triangle.Color);
            }
        }

        private void drawHorizontalLine(double y, double x1, double x2, Color color)
        {
            for (var i = (int)x1; i <= x2; i++)
                DrawPoint(i, (int)y, color);
        }

        private void Swap<T>(ref T x1, ref T x2)
        {
            var x = x1;
            x1 = x2;
            x2 = x;
        }


        public void Update()
        {
            image.Source = bitmap;
        }

        public void SaveTo(string path)
        {
            
        }
        public class Color
        {
            public Color(byte b, byte g, byte r, byte a = 255)
            {
                B = b;
                G = g;
                R = r;
                A = a;
            }
            public byte B;
            public byte G;
            public byte R;
            public byte A;
            public static implicit operator byte[](Color color) => new[]
            {
                color.B,
                color.G,
                color.R,
                color.A
            };

            public static Color Black = new Color( 0, 0, 0);
            public static Color White = new Color( 255, 255, 255);
            public static Color Gray = new Color(128, 128, 128);
            public static Color Blue = new Color( 255, 0, 0);
            public static Color Green = new Color( 0, 255, 0);
            public static Color Red = new Color(0, 0, 255);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var user = sender as UserControl;
            if (user == null) return;
            user.Width = 900;
            user.Height = 600;
        }
    }
}
