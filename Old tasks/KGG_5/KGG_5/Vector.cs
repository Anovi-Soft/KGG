using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG_5
{
    public class Vector
    {
        private double x, y, z;
        public double X
        {
            get { return x; }
        }
        public double Y
        {
            get { return y; }
        }
        public double Z
        {
            get { return z; }
        }
        public double XX
        {
            get { return -x / (2 * Math.Sqrt(2)) + y; }
        }
        public double YY
        {
            get { return x / (2 * Math.Sqrt(2)) - z; }
        }

        public double ZZ
        {
            get { return x; }
        }
        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public string ToString()
        {
            return String.Format("({0};{1};{2})", x, y, z);
        }

    }
}
