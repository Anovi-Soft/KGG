using System;
using System.Collections.Generic;
using System.Linq;
using KGG;

namespace KggGz3
{
    public class Triangulator
    {
        private List<Vector2> poly;

        public Triangulator(IEnumerable<Vector2> polygon)
        {
            this.poly = polygon.ToList();
        }

        public IEnumerable<Triangle> Triangulate()
        {
            var angleFinder = new AngleFinder(poly);
            for (var i = 0; poly.Count > 3; i++)
                if (angleFinder.IsSharpCorner(i))
                {
                    var triangle = new Triangle(poly[i-1], poly[i], poly[i+1]);
                    if (poly.Count(x=>triangle.Contains(x)) == 3)
                        yield return triangle;
                }
        }
    }

    public class Triangle
    {
        public Vector2 A { get; }
        public Vector2 B { get; }
        public Vector2 C { get; }

        public Triangle(Vector2 a, Vector2 b, Vector2 c)
        {
            A = a;
            B = b;
            C = c;
        }

        public bool Contains(Vector2 vector2)
        {
            throw new NotImplementedException();
        }
    }
    public static class ListExtension
    {
        public static T SafeGet<T>(this List<T> list, int index) =>
            list[(index + list.Count)%list.Count];
    }

    public class AngleFinder
    {
        private readonly List<Vector2> edges;

        public AngleFinder(List<Vector2> edges)
        {
            this.edges = edges;
        }

        public bool IsSharpCorner(int i)
        {
            throw new NotImplementedException();
        }
    }
}