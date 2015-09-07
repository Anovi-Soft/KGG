using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG_3
{
    public class MyPolygon
    {
        private List<Vector> points;
        public List<Vector> Points 
        {
            get {return points; } 
        }
        public MyPolygon(List<Vector> points)
        {
            this.points = points;
        }
        
        public static MyPolygon Intersection(MyPolygon a, MyPolygon b)
        {
            b.points.Add(b.points.First());
            for (int i = 0; i < b.points.Count - 1; i++)
            {
                a = Intersection(a, new Segment(b.points[i], b.points[i + 1]));
                if (a.Points.Count == 0)
                    throw new DisjointPolygons();
            }
            return a; 
        }
        public static MyPolygon Intersection(MyPolygon a, Segment b)
        {
            List<Vector> result = new List<Vector>();
            a.points.Add(a.points.First());
            for (int i = 0; i < a.points.Count - 1; i++)
            {
                Vector start = a.points[i];
                Vector end = a.points[i + 1];
                bool s = b.PlaceOfPoint(start) <= 0; //справа
                bool e = b.PlaceOfPoint(end) <= 0; 
                if (s)
                    result.Add(start);
                if (s ^ e)
                    result.Add(b.CrossingPoint(new Segment(start, end)));
            }
            return new MyPolygon(result);
        }
    }
    
    public class DisjointPolygons : Exception {}
}
