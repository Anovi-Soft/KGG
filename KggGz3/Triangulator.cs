using System;
using System.Collections.Generic;
using System.Linq;
using KGG;

namespace KggGz3
{
    public class Triangulator
    {
        public static IEnumerable<Triangle> Triangulate(IEnumerable<Vector2> polygon)
        {
            var poly = polygon.ToList();
            var angleFinder = new AngleFinder(poly);
            for (var i = 0; poly.Count > 3; i++)
                if (angleFinder.IsNormalCorner(i))
                {
                    var triangle = new Triangle(poly.SafeGet(i-1), poly.SafeGet(i), poly.SafeGet(i+1));
                    if (poly.Count(x=>triangle.Contains(x)) == 3)
                        yield return triangle;
                }
        }

        public static IEnumerable<Triangle> SameSquareTriangulation(IEnumerable<Triangle> sourceTriangles, double coef = 0.1, int minTriamgulation = 0)
        {
            var unsorted = sourceTriangles.SelectMany(x => x.Triangulate(minTriamgulation)).ToList();
            var sorted = new SortedList<double, Triangle>(unsorted.ToDictionary(x=>x.Square, x=>x));
            while (Math.Abs(sorted.Last().Key - sorted.First().Key) > coef)
            {
                var max = sorted.Last();
                var indexOfKey = sorted.IndexOfKey(max.Key);
                sorted.RemoveAt(indexOfKey);
                foreach (var triangle in max.Value.Triangulate(1))
                {
                    sorted.Add(triangle.Square, triangle);
                }
            }
            return sorted.Values;
        }
    }
}