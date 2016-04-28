using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public double X { get; }
        public double Y { get;  }

        public static Vector2 operator-(Vector2 a, Vector2 b) =>
            new Vector2(a.X - b.X, a.Y - b.Y);

        public double Length =>
            Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

        public override string ToString() => $"({X};{Y})";
        
        public static bool TryParse(string text, out Vector3 result)
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

        public static Vector3 Parse(string text)
        {
            var splt = text.Split(';').Select((a) => a == "" ? 0 : double.Parse(a));
            if (splt.Count() == 2)
                return new Vector3();
            throw new FormatException();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Vector2;
            if (obj == null) return false;
            return Equals(other);
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
    }
}
