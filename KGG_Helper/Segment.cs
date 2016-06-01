using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace KGG
{
    public class Segment
    {
        public Segment(Vector2 from, Vector2 to)
        {
            From = from;
            To = to;
        }
        public Vector2 From { get; }
        public Vector2 To { get; }
        
        /// <summary>
        /// Resize segment. Point <see cref="From"/> fixed.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public Segment ResizeTo(double length)
        {
            var coefficient = Length / length;
            return new Segment(From, new Vector2(To.X / coefficient, To.Y / coefficient));
        }

        private double length = -1;
        public double Length
        {
            get
            {
                if (length == -1)
                    length = Math.Sqrt(Math.Pow((From.X - To.X), 2) + Math.Pow((From.Y - To.Y), 2));
                return length;
            }
        }
        /// <summary>
        /// If value negative, point to the right of the line.
        /// If value positive, point to the left of the line.
        /// If value equal Zero, point on line.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double PlaceOfPoint(Vector2 point)
        {
            var a = From.Y - To.Y;
            var b = -(From.X - To.X);
            var c = -(a * From.X + b * From.Y);
            return a * point.X + b * point.Y + c;
        }

        /// <summary>
        /// Try cross two segments. 
        /// </summary>
        /// <param name="other"></param>
        /// <param name="point">Resulted point</param>
        /// <returns>True if then lines intersect</returns>
        public bool TryCrossPoint(Segment other, out Vector2 point)
        {
            point = CrossingPoint(other);
            return point != null;
        }

        public override string ToString() => $"[{From};{To}]";

        public Vector2 CrossingPoint(Segment other)
        {
            var x = -((From.X * To.Y - To.X * From.Y) * (other.To.X - other.From.X) - (other.From.X * other.To.Y - other.To.X * other.From.Y) * (To.X - From.X)) / ((From.Y - To.Y) * (other.To.X - other.From.X) - (other.From.Y - other.To.Y) * (To.X - From.X));
            if (double.IsNaN(x))
                return null;
            var y = ((other.From.Y - other.To.Y) * (-x) - (other.From.X * other.To.Y - other.To.X * other.From.Y)) / (other.To.X - other.From.X);
            return new Vector2(x, y);
        }

        public bool Contains(Vector2 point) =>
            In(From.X, point.X, To.X) &&
            In(From.X, point.X, To.X) &&
            Math.Abs(PlaceOfPoint(point)) < 0.001;

        private bool In(double a, double value, double b) =>
            Math.Min(a, b) <= value &&
            Math.Max(a, b) >= value;
    }
}
