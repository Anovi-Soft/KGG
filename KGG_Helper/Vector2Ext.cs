using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KGG;

namespace KGG
{
    public class Vector2Ext:Vector2
    {
        public Vector2Ext()
        {
        }

        public Vector2Ext(double x, double y) : base(x, y)
        {
        }

        public bool ContainsOnAnotherPoly;

        public override string ToString()
        {
            return base.ToString() + (ContainsOnAnotherPoly ? "Принадлежит" : "");
        }
    }
}
