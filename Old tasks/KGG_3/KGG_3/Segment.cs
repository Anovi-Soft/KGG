using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG_3
{
    public class Segment
    {
        private Vector begin, end;
        public Vector Begin
        {
            get { return begin; }
        }
        public Vector End
        {
            get { return end; }
        }
        public Segment(Vector begin, Vector end) 
        {
            this.begin = begin;
            this.end = end;
        }

        public Vector CrossingPoint(Segment other)
        {
            float x = -((begin.X * end.Y - end.X * begin.Y) * (other.End.X - other.Begin.X) - (other.Begin.X * other.End.Y - other.End.X * other.Begin.Y) * (end.X - begin.X)) / ((begin.Y - end.Y) * (other.End.X - other.Begin.X) - (other.Begin.Y - other.End.Y) * (end.X - begin.X));
            if (x == float.NaN)
                return null;
            float y = ((other.Begin.Y - other.End.Y) * (-x) - (other.Begin.X * other.End.Y - other.End.X * other.Begin.Y)) / (other.End.X - other.Begin.X);
            return new Vector(x, y);
        }
        public float PlaceOfPoint(Vector point)
        {
            float a = begin.Y - end.Y;
            float b = -(begin.X - end.X);
            float c = -(a * begin.X + b * begin.Y);
            return a * point.X + b * point.Y + c;
        }

    }
}
