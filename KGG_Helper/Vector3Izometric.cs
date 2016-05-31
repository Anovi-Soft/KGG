using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KGG;

namespace KGG_Helper
{
    public class Vector3I : Vector3
    {

        public Vector3I(double x, double y, double z)
            :base(x,y,z)
        {
        }

        public new double XX => (Y - X) * Math.Sqrt(3.0) / 2;

        public new double YY => (X + Y) / 2 - Z;
    }
}
