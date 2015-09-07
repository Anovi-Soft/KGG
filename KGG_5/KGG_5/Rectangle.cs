using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KGG_5
{
    class Rectangle
    {
        private Vector a, b, c, d;
        public Brush brush;
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
        public Vector D
        {
            get { return d; }
        }
        public void SetBrush(Brush brush)
        {
            this.brush = brush;
        }
        public Rectangle(Vector a, Vector b, Vector c, Vector d, Brush brush = null)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.brush = brush;
        }
        public List<Figure> Triangulation(uint n)
        {
            List<Figure> tri = new List<Figure>(2);
            tri.AddRange(new Triangle(a, b, c).Triangulation(n));
            tri.AddRange(new Triangle(a, d, c).Triangulation(n));
            foreach (var z in tri)  z.SetBrush(brush);
            return tri;
        }
        public string ToString()
        {
            return String.Format("{0}    {1}    {2}    {3}", a.ToString(), b.ToString(), c.ToString(), d.ToString());
        }
    }
}
