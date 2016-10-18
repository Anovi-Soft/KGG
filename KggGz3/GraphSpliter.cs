using System;
using System.Collections.Generic;
using System.Linq;
using KGG;

namespace KggGz3
{
    public class GraphSpliter
    {
        private readonly List<Triangle> vertexesLeft;
        private readonly List<Triangle> vertexesRight;
        private List<Triangle> CurrentVertexes => isLeftStep ? vertexesLeft : vertexesRight;
        private readonly Dictionary<Triangle, HashSet<Triangle>> neib;

        private readonly List<Triangle> leftEdges;
        private readonly List<Triangle> rightEdges;
        private bool isLeftStep;
        private List<Triangle> CurrentEdges => isLeftStep ? leftEdges : rightEdges;

        public GraphSpliter(List<Triangle> vertexesLeft, List<Triangle> vertexesRight, List<FromTo> edges, FromTo cuncurent)
        {
            this.vertexesLeft = vertexesLeft;
            this.vertexesRight = vertexesRight;
            neib = edges.ToLookup(x => x.From)
                .ToDictionary(x => x.Key, x => new HashSet<Triangle>(x.Select(y => y.To)));
            leftEdges = new List<Triangle> {cuncurent.From};
            rightEdges = new List<Triangle> {cuncurent.To};
            vertexesLeft.Remove(cuncurent.From);
            vertexesRight.Remove(cuncurent.From);
            vertexesLeft.Remove(cuncurent.To);
            vertexesRight.Remove(cuncurent.To);
        }

        public IEnumerable<List<Triangle>> Split()
        {
            while (vertexesLeft.Any() && rightEdges.Any())
            {
                var triangle = CurrentVertexes
                    .FirstOrDefault(x => CurrentEdges.Any(y =>
                {
                    HashSet<Triangle> set;
                    return neib.TryGetValue(y, out set) && set.Contains(x);
                }));
                if (triangle != null)
                {
                    CurrentEdges.Add(triangle);
                    vertexesLeft.Remove(triangle);
                    vertexesRight.Remove(triangle);
                }
                isLeftStep = !isLeftStep;
            }
            yield return leftEdges;
            yield return rightEdges;
        }
    }
}