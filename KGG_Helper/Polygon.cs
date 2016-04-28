using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG
{
    public class Polygon
    {
        public List<Vector2> Points {get; private set; }
        public Polygon(List<Vector2> points)
        {
            Points = points;
        }

        //public static Polygon Intersection(Polygon a, Polygon b)
        //{
        //    b.Points.Add(b.Points.First());
        //    for (int i = 0; i < b.Points.Count - 1; i++)
        //    {
        //        a = Intersection(a, new Segment(b.Points[i], b.Points[i + 1]));
        //        if (a.Points.Count == 0)
        //            throw new DisjointPolygons();
        //    }
        //    return a;
        //}
        //public static Polygon Intersection(Polygon a, Segment b)
        //{
        //    List<Vector2> result = new List<Vector2>();
        //    a.Points.Add(a.Points.First());
        //    for (int i = 0; i < a.Points.Count - 1; i++)
        //    {
        //        Vector2 start = a.Points[i];
        //        Vector2 end = a.Points[i + 1];
        //        bool s = b.PlaceOfPoint(start) <= 0; //справа
        //        bool e = b.PlaceOfPoint(end) <= 0;
        //        if (s)
        //            result.Add(start);
        //        if (s ^ e)
        //            result.Add(b.CrossingPoint(new Segment(start, end)));
        //    }
        //    return new Polygon(result);
        //}
        public override string ToString() => Points.Skip(1)
            .Aggregate("["+Points.First(),
            (current, point) => current + "," + point) + "]";
    }
}
