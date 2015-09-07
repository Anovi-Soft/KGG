using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KGG_5
{

    class Triangle : Figure
    {
        private Vector a, b, c;
        public Brush Brush;
        public Vector A
        {
            get { return a; }
        }
        public Vector B
        {
            get { return b; }
        }
        public Vector C
        {
            get { return c; }
        }
        public double ZZ()
        {
            return (a.ZZ+b.ZZ+c.ZZ)/3;
        }
        public Triangle(Vector a, Vector b, Vector c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        public void SetBrush(Brush brush)
        {
            this.Brush = brush;
        }
        public List<Figure> Triangulation(uint n)
        {
            if (n == 0)
                return new List<Figure>() { this };

            List<Figure> tri = new List<Figure>(2);
            Vector d = new Vector((a.X+c.X)/2, (a.Y+c.Y)/2, (a.Z+c.Z)/2);
            tri.AddRange(new Triangle(a, d, b).Triangulation(n-1));
            tri.AddRange(new Triangle(b, d, c).Triangulation(n-1));
            return tri;
        }
        public Vector[] Vertex ()
        {
            return new Vector[] {a,b,c};
        }
        public Brush Color()
        {
            return Brush;
        }
    }
}
