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

        public void DrawPoint(Vector2 point, Color color) =>
            DrawPoint((int) Math.Round(point.X), (int)Math.Round(point.Y), color);

        public void DrawPoint(int x, int y, Color color) =>
            DrawPoint(x, y, (byte[])color);

        public void DrawPoint(int x, int y) =>
            DrawPoint(x, y, Color.Black);
        
        public void DrawLine(int x1, int y1, int x2, int y2, int color) => 
            bitmap.DrawLine(x1, y1, x2, y2, color);

        public void DrawLine(Vector2 from, Vector2 to, Color color) =>
            DrawLine(   (int)Math.Round(@from.X), (int)Math.Round(@from.Y),
                        (int)Math.Round(to.X), (int)Math.Round(to.Y), color);

        public void DrawLine(Segment segment, Color color) =>
            DrawLine(segment.From, segment.To, color);

        public void DrawTriangle(Triangle triangle)
        {
            var halfWidth = MapWidth / 2;
            var halfHeight = MapHeight / 2;
            var x1 = (int)triangle.A.XX;
            var y1 = (int)triangle.A.YY;
            var x2 = (int)triangle.B.XX;
            var y2 = (int)triangle.B.YY;
            var x3 = (int)triangle.C.XX;
            var y3 = (int)triangle.C.YY;
            bitmap.FillTriangle(
                x1 + halfWidth,
                y1 + halfHeight,
                x2 + halfWidth,
                y2 + halfHeight,
                x3 + halfWidth,
                y3 + halfHeight,
                triangle.Color);
        }

        public void DrawPolygon(Polygon polygon) =>
            DrawPoint(polygon.Points, polygon.Color);

        private void DrawPoint(List<Vector2Ext> points, Color color)
        {
            bitmap.FillPolygon(
                points.Concat(new List<Vector2Ext> {points.First()})
                .SelectMany(x=>x.AsArray()).ToArray(), color);
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
            public static implicit operator byte[] (Color color) => new[]
            {
                color.B,
                color.G,
                color.R,
                color.A
            };

            public static implicit operator int(Color color) =>
                color.A << 24 |
                color.R << 16 |
                color.G << 8 |
                color.B << 0;

            public static Color Black = new Color( 0, 0, 0);
            public static Color White = new Color( 255, 255, 255);
            public static Color Gray = new Color(128, 128, 128);

            public static Color Blue = new Color(255, 0, 0);
            public static Color Green = new Color(0, 255, 0);
            public static Color Red = new Color(0, 0, 255);

            public static Color Yelow = new Color(0, 255, 255);
            public static Color Pink = new Color(255, 0, 255);
            public static Color Aqua = new Color(255, 255, 0);

            public Color Alpha(byte a)=>
                new Color(B, G, R, a);
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
