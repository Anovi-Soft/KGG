using System;
using System.Collections.Generic;
using System.Linq;
using KGG;

namespace KggGz3
{
    public class GraphSpliter
    {
        private List<Triangle> vertexes;
        private Dictionary<Triangle, HashSet<Triangle>> neib;

        private List<Triangle> leftEdges;
        private List<Triangle> rightEdges;
        private bool isLeftStep;
        private List<Triangle> CurrentEdges => isLeftStep ? leftEdges : rightEdges;

        public GraphSpliter(List<Triangle> vertexes, List<FromTo> edges, FromTo cuncurent)
        {
            this.vertexes = vertexes;
            neib = edges.ToLookup(x => x.From)
                .ToDictionary(x => x.Key, x => new HashSet<Triangle>(x.Select(y => y.To)));
            leftEdges = new List<Triangle> {cuncurent.From};
            rightEdges = new List<Triangle> {cuncurent.To};
            vertexes.Remove(cuncurent.From);
            vertexes.Remove(cuncurent.To);
        }

        public IEnumerable<List<Triangle>> Split()
        {
            while (vertexes.Any())
            {
                var triangle = vertexes.FirstOrDefault(x => CurrentEdges.Any(y =>
                {
                    HashSet<Triangle> set;
                    return neib.TryGetValue(y, out set) && set.Contains(x);
                }));
                if (triangle != null)
                {
                    CurrentEdges.Add(triangle);
                    vertexes.Remove(triangle);
                }
                else
                {
                    var i = 0;
                }
                isLeftStep = !isLeftStep;
            }
            yield return leftEdges;
            yield return rightEdges;
        }
    }
}