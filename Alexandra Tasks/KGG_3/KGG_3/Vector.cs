using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG_3
{
    public class Vector
    {
        private float x, y;
        public float X
        {
            get { return x; }
        }
        public float Y
        {
            get { return y; }
        }
        public Vector(float x, float y) 
        {
            this.x = x;
            this.y = y;
        }
        public Vector(Vector a, Vector b)
        {
            this.x = b.x - a.x;
            this.y = b.y - a.y;
        }
        public static float operator *(Vector a, Vector b)
        {
            return a.x * b.y - a.y * b.x;
        }
        public static Vector CrossingPoint(Vector a1, Vector a2, Vector b1, Vector b2)
        {
            float x = -((a1.x * a2.y - a2.x * a1.y) * (b2.x - b1.x) - (b1.x * b2.y - b2.x * b1.y) * (a2.x - a1.x)) / ((a1.y - a2.y) * (b2.x - b1.x) - (b1.y - b2.y) * (a2.x - a1.x));
            if (x == float.NaN)
                return null;
            float y = ((b1.y - b2.y) * (-x) - (b1.x * b2.y - b2.x * b1.y)) / (b2.x - b1.x);
            return new Vector(x, y);
        }
        public static float PlacePoint(Vector start, Vector end, Vector point)
        {
            float a = start.y - end.y;
            float b = -(start.x - end.x);
            float c = -(a * start.x + b * start.y);
            return a * point.x + b * point.y + c;
        }

    }
}
