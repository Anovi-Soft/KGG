using System;
using System.Collections.Generic;
using System.Linq;
using KGG;

namespace KggGz3
{
    public class Triangle
    {
        public Vector2 A { get; set; }
        public Vector2 B { get; set; }
        public Vector2 C { get; set; }

        public KggCanvas.Color Color { get; set; }

        private double? square = null;
        public double Square => (square ?? (square = SolveSquare())).Value;

        private double SolveSquare()
        {
            var p = Edges.Sum(x => x.Length)/2;
            var d = Edges.Aggregate(p, (x, segment) => x * (p - segment.Length));
            return Math.Sqrt(d);
        }

        public Segment[] Edges => new[]
        {
            new Segment(A, B),
            new Segment(B, C),
            new Segment(C, A)
        };

        public Triangle()
        {
        }

        public Triangle(Vector2 a, Vector2 b, Vector2 c, KggCanvas.Color color = null)
        {
            A = a;
            B = b;
            C = c;
            Color = color;
        }

        public bool Contains(Vector2 vector2)
        {
            return Edges.All(x=>PointPosition(x.From, x.To, vector2) <= 0);
        }


        private static double PointPosition(Vector2 from, Vector2 to, Vector2 point)
        {
            var a = from.Y - to.Y;
            var b = -(from.X - to.X);
            var c = -(a * from.X + b * from.Y);
            return a * point.X + b * point.Y + c;
        }

        public IEnumerable<Triangle> Triangulate(int n)
        {
            if (n == 0)
                return new[] { this };

            var bestVariant = new[]
            {
                Tuple.Create(A, B, C),
                Tuple.Create(B, C, A),
                Tuple.Create(C, A, B)
            }.OrderBy(x => (x.Item1 - x.Item2).Length)
            .Last();
            var middle = bestVariant.Item1 + (bestVariant.Item2 - bestVariant.Item1) / 2;

            var tri = new Triangle(bestVariant.Item1, middle, bestVariant.Item3, Color).Triangulate(n - 1)
                .Concat(new Triangle(bestVariant.Item2, middle, bestVariant.Item3, Color).Triangulate(n - 1));

            return tri;
        }

        public override string ToString()
        {
            return $"{nameof(A)}: {A}, {nameof(B)}: {B}, {nameof(C)}: {C}";
        }
    }
}