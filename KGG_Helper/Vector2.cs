using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KGG_Helper;

namespace KGG
{
    public class Vector2
    {
        public Vector2()
        {
        }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public static Vector2 operator-(Vector2 a, Vector2 b) =>
            new Vector2(a.X - b.X, a.Y - b.Y);

        public double Length =>
            Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

        public override string ToString() => $"({X};{Y})";
        
        public static bool TryParse(string text, out Vector2 result)
        {
            try
            {
                result = Parse(text);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        public int[] AsArray() => new[] {(int) X, (int) Y};

        public Vector2 AsIntVector() =>
            new Vector2((int)X, (int)Y);

        public static Vector2 Parse(string text)
        {
            var splt = text.Split(';').Select((a) => a == "" ? 0 : double.Parse(a));
            if (splt.Count() == 2)
                return new Vector2(splt.First(), splt.Last());
            throw new FormatException();
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 && Equals((Vector2) obj);
        }

        protected bool Equals(Vector2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode()*397) ^ Y.GetHashCode();
            }
        }

        public static implicit operator PolarPoint(Vector2 vector) =>
            new PolarPoint
            {
                R = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y),
                Fi = Math.Atan(vector.Y / vector.X)
            };
    }
}
