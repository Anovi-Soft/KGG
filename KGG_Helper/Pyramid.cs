using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG
{
    public class Pyramid
    {
        private readonly Triangle[] _triangles = new Triangle[6];
        public Pyramid(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 top, KggCanvas.Color[] color)
        {
            _triangles[0] = new Triangle(a, b, c) { Color = color[0] };
            _triangles[1] = new Triangle(c, d, a) { Color = color[0] };
            _triangles[2] = new Triangle(a, top, b) { Color = color[1] };
            _triangles[3] = new Triangle(b, top, c) { Color = color[2] };
            _triangles[4] = new Triangle(c, top, b) { Color = color[3] };
            _triangles[5] = new Triangle(d, top, a) { Color = color[4] };
        }
        
        /// <summary>
        /// Split Pyramid to 6*2^(n+1) _triangles
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<Triangle> Triangulation(uint n)
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
