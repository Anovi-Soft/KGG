using System;
using System.Collections.Generic;
using System.Linq;

namespace KGG
{
    public class Triangle
    {
        public Triangle(Vector3 a, Vector3 b, Vector3 c)
        {
            A = a;
            B = b;
            C = c;
        }
        public Triangle(Vector3 a, Vector3 b, Vector3 c, KggCanvas.Color color)
            : this(a, b, c)
        {
            Color = color;
        }

        public KggCanvas.Color Color;
        public Vector3 A { get; }
        public Vector3 B { get; }
        public Vector3 C { get; }
        public double ZZ => (A.ZZ + B.ZZ + C.ZZ) / 3;
        public Vector3[] Points => new[] {A, B, C};
        /// <summary>
        /// Split Triangle to 2^n triangles
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IEnumerable<Triangle> Triangulation(uint n)
        {
            if (n==0)
                return new [] {this};

            var bestVariant = new[]
            {
                Tuple.Create(A, B, C),
                Tuple.Create(B, C, A),
                Tuple.Create(C, A, B)
            }.OrderBy(x=> (x.Item1 - x.Item2).Length)
                .Last();
            var middle = bestVariant.Item1 + (bestVariant.Item2 - bestVariant.Item1)/2;

            var tri = new Triangle(bestVariant.Item1, middle, bestVariant.Item3, Color).Triangulation(n - 1)
                .Concat(new Triangle(bestVariant.Item2, middle, bestVariant.Item3, Color).Triangulation(n - 1));

            return tri;
        } 
    }
}
