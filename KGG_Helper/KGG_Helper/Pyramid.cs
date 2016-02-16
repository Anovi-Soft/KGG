using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG
{
    public class Pyramid
    {
        private Triangle[] triangles = new Triangle[6];
        public Pyramid(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 top, Brush[] color)
        {
            triangles[0] = new Triangle(a, b, c) { Color = color[0] };
            triangles[1] = new Triangle(c, d, a) { Color = color[0] };
            triangles[2] = new Triangle(a, top, b) { Color = color[1] };
            triangles[3] = new Triangle(b, top, c) { Color = color[2] };
            triangles[4] = new Triangle(c, top, b) { Color = color[3] };
            triangles[5] = new Triangle(d, top, a) { Color = color[4] };
        }
        
        /// <summary>
        /// Split Pyramid to 2^(n+1) triangles
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<Triangle> Triangulation(uint n)
        {
            var tri = new List<Triangle>(12);
            foreach (var triangle in triangles)
            {
                tri.AddRange(triangle.Triangulation(n));
            }
            return tri;
        }

    }
}
