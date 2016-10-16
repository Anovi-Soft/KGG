using System;
using System.Collections.Generic;
using System.Linq;
using KGG;

namespace KggGz3
{
    internal static class TriangleGraphHelper
    {
        public static IEnumerable<FromTo> BuildEdges(IEnumerable<MarkedEdgeTriangle> triangles)
        {
            return triangles.SelectMany(x => BuildTrianlgeEdges(triangles, x));
        }

        private static IEnumerable<FromTo> BuildTrianlgeEdges(IEnumerable<MarkedEdgeTriangle> triangles, MarkedEdgeTriangle triangle)
        {
            return triangle.Edges.Select(x => new FromTo(triangle, SingleSameEdge(triangles, x)));
        }

        private static MarkedEdgeTriangle SingleSameEdge(IEnumerable<MarkedEdgeTriangle> triangles, Segment edge)
        {
            return triangles.Single(x => x.Edges.Any(y => y.Equals(edge)));
        }

        public static IEnumerable<MarkedEdgeTriangle> OrderByRadius(IEnumerable<FromTo> edges, List<MarkedEdgeTriangle> vertexes, out FromTo cuncurent)
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

            var e = vertexes.ToDictionary(x=>x, x => vertexes.Where(y=>x!=y).Max(y => d[Key(x, y)]));
            var center = vertexes.OrderBy(x => e[x]).First();

            cuncurent = d.OrderBy(x => x.Value).Last().Key;

            return vertexes.OrderBy(x => d[Key(x, center)]);
        }

        private static FromTo Key(MarkedEdgeTriangle a, MarkedEdgeTriangle b) => new FromTo(a,b);
    }
}