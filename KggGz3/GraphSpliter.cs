using System;
using System.Collections.Generic;
using System.Linq;
using KGG;

namespace KggGz3
{
    public class GraphSpliter
    {
        private List<MarkedEdgeTriangle> vertexes;
        private Dictionary<MarkedEdgeTriangle, HashSet<MarkedEdgeTriangle>> neib;

        private List<MarkedEdgeTriangle> leftEdges;
        private List<MarkedEdgeTriangle> rightEdges;
        private bool isLeftStep;
        private List<MarkedEdgeTriangle> CurrentEdges => isLeftStep ? leftEdges : rightEdges;

        public GraphSpliter(List<MarkedEdgeTriangle> vertexes, List<FromTo> edges, FromTo cuncurent)
        {
            this.vertexes = vertexes;
            neib = edges.ToLookup(x => x.From)
                .ToDictionary(x => x.Key, x => new HashSet<MarkedEdgeTriangle>(x.Select(y => y.To)));
            leftEdges = new List<MarkedEdgeTriangle> {cuncurent.From};
            rightEdges = new List<MarkedEdgeTriangle> {cuncurent.To};
            vertexes.Remove(cuncurent.From);
            vertexes.Remove(cuncurent.To);
        }

        public Tuple<List<MarkedEdgeTriangle>, List<MarkedEdgeTriangle>> Split()
        {
            while (vertexes.Any())
            {
                var triangle = vertexes.FirstOrDefault(x => CurrentEdges.Any(y =>
                {
                    HashSet<MarkedEdgeTriangle> set;
                    return neib.TryGetValue(x, out set) && set.Contains(y);
                }));
                if (triangle != null)
                {
                    CurrentEdges.Add(triangle);
                    vertexes.Remove(triangle);
                }
                isLeftStep = !isLeftStep;
            }
            return Tuple.Create(leftEdges, rightEdges);
        }
    }
}