using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG
{
    public class Cube
    {
        private readonly Rectangle[] _rectangles;
        public Cube(Vector3 a, Vector3 g, KggCanvas.Color[] color)
        {
            var b = new Vector3(a.X, g.Y, a.Z);
            var c = new Vector3(g.X, g.Y, a.Z);
            var d = new Vector3(g.X, a.Y, a.Z);
            var e = new Vector3(a.X, a.Y, g.Z);
            var f = new Vector3(a.X, g.Y, g.Z);
            var h = new Vector3(g.X, a.Y, g.Z);

            _rectangles = new []
            {
                new Rectangle(a, b, c, d, color[0]),
                new Rectangle(a, e, h, d, color[1]),
                new Rectangle(a, b, f, e, color[2]),
                new Rectangle(g, c, b, f, color[3]),
                new Rectangle(g, h, d, c, color[4]),
                new Rectangle(g, f, e, h, color[5])
            };
        }

        /// <summary>
        /// Split Cube to 6*2^(n+1) triangles
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IEnumerable<Triangle> Triangulation(uint n)
        {
            return _rectangles
                .AsParallel()
                .SelectMany(x => x.Triangulation(n));
            //var tri = new List<Triangle>((int) (12 * Math.Pow(2, n)));
            //foreach (var a in _rectangles)
            //    tri.AddRange(a.Triangulation(n));
            //return tri;
        }
    }
}
