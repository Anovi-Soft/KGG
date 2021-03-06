﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG
{
    class Rectangle
    {
        public KggCanvas.Color Color;

        public Rectangle(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public Rectangle(Vector3 a, Vector3 b, Vector3 c, Vector3 d, KggCanvas.Color color)
            : this(a, b, c, d)
        {
            Color = color;
        }

        public Vector3 A { get; }
        public Vector3 B { get; }
        public Vector3 C { get; }
        public Vector3 D { get; }

        /// <summary>
        /// Split Rectangle to 2^(n+1) triangles
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IEnumerable<Triangle> Triangulation(uint n)
        {
            var triangles = new Triangle(A, B, C).Triangulation(n)
                .Concat(new Triangle(C, D, A).Triangulation(n));
            foreach (var z in triangles)
                z.Color = Color;

            return triangles;
        }
        public override string ToString() =>
             $"{A}    {B}    {C}    {D}";

    }
}
