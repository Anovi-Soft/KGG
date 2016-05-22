using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG
{
    public class Pyramid
    {
        private readonly List<Triangle> _triangles;
        public Pyramid(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 e, KggCanvas.Color[] color)
        {
            var ad = MiddlePoint(a, d);
            var ab = MiddlePoint(a, b);
            var bc = MiddlePoint(b, c);
            var cd = MiddlePoint(c, d);
            var centre = MiddlePoint(a, c);

            _triangles = new List<Triangle>
            {
                new Triangle(d, ad, e, color[0]),
                new Triangle(a, ad, e, color[0]),
                new Triangle(a, ab, e, color[1]),
                new Triangle(b, ab, e, color[1]),
                new Triangle(b, bc, e, color[2]),
                new Triangle(c, bc, e, color[2]),
                new Triangle(c, cd, e, color[3]),
                new Triangle(d, cd, e, color[3]),
                new Triangle(b, centre, a, color[4]),
                new Triangle(d, centre, a, color[4]),
                new Triangle(b, centre, c, color[4]),
                new Triangle(d, centre, c, color[4])
            };
        }

        private Vector3 MiddlePoint(Vector3 a, Vector3 b) => a + (b - a)/2;

        /// <summary>
        /// Split Pyramid to 6*2^(n+1) _triangles
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IEnumerable<Triangle> Triangulation(uint n)
        {
            var tri = new List<Triangle>(12);
            foreach (var triangle in _triangles)
            {
                tri.AddRange(triangle.Triangulation(n));
            }
            return tri;
        }

    }
}
