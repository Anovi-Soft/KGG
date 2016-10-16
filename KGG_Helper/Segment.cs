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
            return new Segment(From, new Vector2Ext(To.X / coefficient, To.Y / coefficient));
        }

        private double? length = null;
        public double Length => (length ?? (length = SolveLength())).Value;

        private double SolveLength() => Math.Sqrt(Math.Pow((From.X - To.X), 2) + Math.Pow((From.Y - To.Y), 2));

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
        public bool TryCrossPoint(Segment other, out Vector2Ext point)
        {
            var v1 = (other.To.X - other.From.X) * (From.Y - other.From.Y) - (other.To.Y - other.From.Y) * (From.X - other.From.X);
            var v2 = (other.To.X - other.From.X) * (To.Y - other.From.Y) - (other.To.Y - other.From.Y) * (To.X - other.From.X);
            var v3 = (To.X - From.X) * (other.From.Y - From.Y) - (To.Y - From.Y) * (other.From.X - From.X);
            var v4 = (To.X - From.X) * (other.To.Y - From.Y) - (To.Y - From.Y) * (other.To.X - From.X);
            if ((v1 * v2 >= 0) || (v3 * v4 >= 0))
            {
                point = null;
                return false;
            }
            var x = -((From.X * To.Y - To.X * From.Y) * (other.To.X - other.From.X) - (other.From.X * other.To.Y - other.To.X * other.From.Y) * (To.X - From.X))
                / ((From.Y - To.Y) * (other.To.X - other.From.X) - (other.From.Y - other.To.Y) * (To.X - From.X));
            if (double.IsNaN(x))
            {
                point = null;
                return false;
            }
            var y = ((other.From.Y - other.To.Y) * (-x) - (other.From.X * other.To.Y - other.To.X * other.From.Y)) / (other.To.X - other.From.X);
            if ((y - From.Y) * (To.X - From.X) - (x - From.X) * (To.Y - From.Y) > 0.01)
            {
                point = null;
                return false;
            }
            point =  new Vector2Ext(x, y);
            return true;
        }

        public override string ToString() => $"[{From};{To}]";
        

    }
}
