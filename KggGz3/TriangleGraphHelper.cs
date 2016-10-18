using System;
using System.Collections.Generic;
using System.Linq;
using KGG;

namespace KggGz3
{
    internal static class TriangleGraphHelper
    {
        public static IEnumerable<FromTo> BuildEdges(IEnumerable<Triangle> triangles)
        {
            return triangles.SelectMany(x => BuildTrianlgeEdges(triangles, x))
                .Where(x=>x.From != null && x.To != null);
        }

        private static IEnumerable<FromTo> BuildTrianlgeEdges(IEnumerable<Triangle> triangles, Triangle triangle)
        {
            return triangle.Edges.Select(x => new FromTo(triangle, SingleSameEdge(triangles.Where(y=>y!=triangle), x)));
        }

        private static Triangle SingleSameEdge(IEnumerable<Triangle> triangles, Segment edge)
        {
            return triangles.SingleOrDefault(x => x.Edges.Any(y => y.Equals(edge)));
        }

        public static IEnumerable<Triangle> OrderByRadius(IEnumerable<FromTo> edges, List<Triangle> vertexes, out FromTo cuncurent)
        {
            var d = edges.ToDictionary(edge => edge, edge => 1);
            foreach (var a in vertexes)
                foreach (var b in vertexes)
                    foreach (var c in vertexes)
                    {
                        var ac = Key(a, c);
                        var cb = Key(c, b);
                        if (a == b || !d.ContainsKey(ac) || !d.ContainsKey(cb))
                            continue;
                        var left = d[ac];
                        var right= d[cb];
                        var ab = Key(a, b);
                        if (!d.ContainsKey(ab))
                            d.Add(ab, left + right);
                        else
                            d[ab] = Math.Min(left + right, d[ab]);
                    }

            var e = vertexes.ToDictionary(x=>x, x => vertexes.Max(y =>
            {
                int result;
                return d.TryGetValue(Key(x, y), out result) ? result : int.MinValue;
            }));
            var center = vertexes.OrderBy(x => e[x]).First();

            cuncurent = d.OrderBy(x => x.Value).Last().Key;

            return vertexes.OrderByDescending(x =>
            {
                int result;
                return d.TryGetValue(Key(x, center), out result) ? result : 0;
            });
        }

        private static FromTo Key(Triangle a, Triangle b) => new FromTo(a,b);
    }
}