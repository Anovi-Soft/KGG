using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG
{
    public class Cube
    {

        private Rectangle[] rectangles = new Rectangle[6];
        public Cube(Vector3 a, Vector3 b, KggCanvas.Color[] color)
        {
            rectangles[0] = new Rectangle(new Vector3(a.X, a.Y, a.Z), new Vector3(a.X, a.Y, b.Z),
                new Vector3(a.X, b.Y, b.Z), new Vector3(a.X, b.Y, a.Z)) {Color = color[0]};
            rectangles[1] = new Rectangle(new Vector3(a.X, b.Y, a.Z), new Vector3(a.X, b.Y, b.Z),
                new Vector3(b.X, b.Y, b.Z), new Vector3(b.X, b.Y, a.Z)) {Color = color[1]};
            rectangles[2] = new Rectangle(new Vector3(a.X, a.Y, b.Z), new Vector3(a.X, b.Y, b.Z),
                new Vector3(b.X, b.Y, b.Z), new Vector3(b.X, a.Y, b.Z)) {Color = color[2]};
            rectangles[3] = new Rectangle(new Vector3(b.X, b.Y, b.Z), new Vector3(b.X, b.Y, a.Z),
                new Vector3(b.X, a.Y, a.Z), new Vector3(b.X, a.Y, b.Z)) {Color = color[3]};
            rectangles[4] = new Rectangle(new Vector3(b.X, a.Y, b.Z), new Vector3(b.X, a.Y, a.Z),
                new Vector3(a.X, a.Y, a.Z), new Vector3(a.X, a.Y, b.Z)) {Color = color[4]};
            rectangles[5] = new Rectangle(new Vector3(b.X, b.Y, a.Z), new Vector3(b.X, a.Y, a.Z),
                new Vector3(a.X, a.Y, a.Z), new Vector3(a.X, b.Y, a.Z)) {Color = color[5]};
        }

        /// <summary>
        /// Split Cube to 6*2^(n+1) triangles
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<Triangle> Triangulation(uint n)
        {
            var tri = new List<Triangle>(6);
            foreach (var a in rectangles)
                tri.AddRange(a.Triangulation(n));
            return tri;
        }
    }
}
