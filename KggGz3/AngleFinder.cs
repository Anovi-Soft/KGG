using System;
using System.Collections.Generic;
using KGG;

namespace KggGz3
{
    public class AngleFinder
    {
        private readonly List<Vector2> edges;

        public AngleFinder(List<Vector2> edges)
        {
            this.edges = edges;
        }

        public bool IsNormalCorner(int i)
        {
            return Angle(edges.SafeGet(i - 1), edges.SafeGet(i), edges.SafeGet(i + 1)) < 180;
        }

        private static double Angle(Vector2 a, Vector2 b, Vector2 c)
        {
            var x1 = a.X - b.X;
            var x2 = c.X - b.X;
            var y1 = a.Y - b.Y;
            var y2 = c.Y - b.Y;
            var d1 = Math.Sqrt(x1 * x1 + y1 * y1);
            var d2 = Math.Sqrt(x2 * x2 + y2 * y2);
            var angleRad = Math.Acos((x1 * x2 + y1 * y2) / (d1 * d2));
            return angleRad * 180 / Math.PI;
        }
    }
}