using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KGG_5
{
    class Cube
    {
        private Rectangle[] rectangles = new Rectangle[6];
        public Cube(Vector a, Vector b, Brush[] brush)
        {
            rectangles[0] = new Rectangle(new Vector(a.X, a.Y, a.Z), new Vector(a.X, a.Y, b.Z), new Vector(a.X, b.Y, b.Z), new Vector(a.X, b.Y, a.Z), brush[0]);
            rectangles[1] = new Rectangle(new Vector(a.X, b.Y, a.Z), new Vector(a.X, b.Y, b.Z), new Vector(b.X, b.Y, b.Z), new Vector(b.X, b.Y, a.Z), brush[1]);
            rectangles[2] = new Rectangle(new Vector(a.X, a.Y, b.Z), new Vector(a.X, b.Y, b.Z), new Vector(b.X, b.Y, b.Z), new Vector(b.X, a.Y, b.Z), brush[2]);
            rectangles[3] = new Rectangle(new Vector(b.X, b.Y, b.Z), new Vector(b.X, b.Y, a.Z), new Vector(b.X, a.Y, a.Z), new Vector(b.X, a.Y, b.Z), brush[3]);
            rectangles[4] = new Rectangle(new Vector(b.X, a.Y, b.Z), new Vector(b.X, a.Y, a.Z), new Vector(a.X, a.Y, a.Z), new Vector(a.X, a.Y, b.Z), brush[4]);
            rectangles[5] = new Rectangle(new Vector(b.X, b.Y, a.Z), new Vector(b.X, a.Y, a.Z), new Vector(a.X, a.Y, a.Z), new Vector(a.X, b.Y, a.Z), brush[5]);  
        }
        public List<Figure> Triangulation(uint n)
        {
            List<Figure> tri = new List<Figure>(12);
            foreach (var a in rectangles)
                tri.AddRange(a.Triangulation(n));
            return tri;
        }
    }
}
