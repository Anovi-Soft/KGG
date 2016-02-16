using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG
{
    public class Triangle
    {

        public Triangle(Vector3 a, Vector3 b, Vector3 c)
        {
            A = a;
            B = b;
            C = c;
        }
        public Triangle(Vector3 a, Vector3 b, Vector3 c, Brush color)
            : this(a, b, c)
        {
            Color = color;
        }

        public Brush Color;
        public Vector3 A { get; }
        public Vector3 B { get; }
        public Vector3 C { get; }
        public double ZZ => (A.ZZ + B.ZZ + C.ZZ) / 3;

        /// <summary>
        /// Split Triangle to 2^n triangles
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<Triangle> Triangulation(uint n)
        {
            if (n==0)
                return new List<Triangle> {this};

            var tri = new List<Triangle>(2);
            var d = new Vector3((A.X + C.X) / 2, (A.Y + C.Y) / 2, (A.Z + C.Z) / 2);
            tri.AddRange(new Triangle(A, d, B, Color).Triangulation(n - 1));
            tri.AddRange(new Triangle(B, d, C, Color).Triangulation(n - 1));
            return tri;
        } 
    }
}
