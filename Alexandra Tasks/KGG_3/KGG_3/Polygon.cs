using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG_3
{
    class Polygon
    {
        List<Vector> points;
        public Polygon(List<Vector> points)
        {
            this.points = points;
        }
        public bool ContainPoint(Vector point)
        {
            float end = (new Vector(points.Last(), points.First())) * (new Vector(points.Last(), point));
            for (int i = 0; i < points.Count-1; i++ )
            {
                float step = (new Vector(points[i], points[i+1])) * (new Vector(points[i], point));
                if (end * step < 0)
                    return false;
            }
            return true;
        }
        public static Polygon Intersection(Polygon a, Polygon b)
        {
            b.points.Add(b.points.First());
            for (int i = 0; i < b.points.Count - 1; i++)
                a = Intersection(a, b.points[i], b.points[i + 1]);
            return a; 
        }
        public static Polygon Intersection(Polygon a, Vector b1, Vector b2)
        {
            List<Vector> result = new List<Vector>();
            a.points.Add(a.points.Last());
            for (int i = 0; i < a.points.Count - 1; i++)
            {
                Vector start = a.points[i];
                Vector end = a.points[i + 1];
                bool s = Vector.PlacePoint(b1, b2, start) >= 0;
                bool e = Vector.PlacePoint(b1, b2, end) >= 0;
                if (s)
                    result.Add(start);
                if (s ^ e)
                    result.Add(Vector.CrossingPoint(start, end, b1, b2));
            }
            return new Polygon(result);
        }
    }
}
