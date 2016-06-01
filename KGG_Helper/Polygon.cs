using System.Collections.Generic;
using System.Linq;
using KGG_Helper;

namespace KGG
{ 
    public class Polygon
    {
        public KGG.KggCanvas.Color Color;
        public List<Vector2> Points { get; private set; }

        private List<Segment> _segments;
        public List<Segment> Segments => 
            _segments ?? 
            (_segments = Points.Select((x, i) => new Segment(x, Points[(i + 1)%Points.Count]))
            .ToList());

        public Polygon()
        {
            Points = new List<Vector2>();
        }
        public Polygon(List<Vector2> points, KggCanvas.Color color)
        {
            Color = color;
            Points = points;
        }

        public IEnumerable<Polygon> Cut(Polygon other)
        {
            var oth = this & other;
            if (oth.Points.Count < 3)
            {
                yield return this;
                yield break;
            }
            var points = AddCommon(Points, oth).Distinct().ToList();
            var otherPoints = oth.Points.Select(x => new
                Vector2Mark
                {
                    Point = x,
                    Marked = false
                }).ToList();
            otherPoints.Reverse();
            var currentPoints = points;
            var concurentPoints = otherPoints;
            while (points.Any(x=>x.Marked))
            {
                var result = new List<Vector2>();
                var i = points.IndexFirst(x => x.Marked);
                var start = currentPoints[i];
                do
                {
                    var current = currentPoints[i];
                    result.Add(current.Point);
                    current.Marked = false;
                    var j = concurentPoints.IndexFirst(x => x.Point.Equals(current.Point));
                    if (j >= 0)
                    {
                        i = j;
                        var tmp = currentPoints;
                        currentPoints = concurentPoints;
                        concurentPoints = tmp;
                        result.Add(currentPoints[i].Point);
                    }
                    i = (i + 1) % currentPoints.Count;
                    //TODO find issue with one point in polygon

                } while (currentPoints[i].Point != start.Point);
                result = result.NeighborsDistinct().ToList();
                if (result.Count>2)
                    yield return new Polygon(result, Color);
                currentPoints = points;
                concurentPoints = otherPoints;
            }
        }

        private IEnumerable<Vector2Mark> AddCommon(List<Vector2> points, Polygon other)
        {
            for (int i = 0; i < points.Count; i++)
            {
                yield return new Vector2Mark
                {
                    Point = points[i],
                    Marked = !other.ContainExt(points[i])
                };
                var segment = new Segment(points[i], points[(i+1)%points.Count]);
                var toAdd = other.Points.Where(x => segment.Contains(x)).ToList();
                foreach (var point in toAdd)
                    yield return new Vector2Mark
                    {
                        Point = point,
                        Marked = false
                    };
            }
        }

        private bool ContainExt(Vector2 vector2)
        {
            return Contain(vector2) ||
                Points.Any(vector2.Equals);
        }

        public static Polygon operator & (Polygon a, Polygon b)
        {
            b = new Polygon(b.Points.ToList(), b.Color);
            b.Points.Add(b.Points.First());
            for (int i = 0; i < b.Points.Count - 1; i++)
            {
                a = Intersection(a, new Segment(b.Points[i], b.Points[i + 1]));
                if (a.Points.Count == 0)
                    return a;
            }
            a.Points = a.Points.Distinct().ToList();
            return a;
        }
        public static Polygon Intersection(Polygon a, Segment b)
        {
            a = new Polygon(a.Points.ToList(), a.Color);
            var result = new List<Vector2>();
            a.Points.Add(a.Points.First());
            for (int i = 0; i < a.Points.Count - 1; i++)
            {
                var start = a.Points[i];
                var end = a.Points[i + 1];
                var s = b.PlaceOfPoint(start) <= 0; //справа
                var e = b.PlaceOfPoint(end) <= 0;
                if (s)
                    result.Add(start);
                if (s ^ e)
                    result.Add(b.CrossingPoint(new Segment(start, end)));
            }
            return new Polygon(result, a.Color);
        }
        
        public bool Contain(Vector2 point)
        {
            return Segments.All(x => x.PlaceOfPoint(point) >= 0);
        }

        public override string ToString() => Points.Skip(1)
            .Aggregate("["+Points.First(),
            (current, point) => current + "," + point) + "]";

    }

    public class Vector2Mark
    {
        public Vector2 Point { get; set; }
        public bool Marked { get; set; }
        public override bool Equals(object obj) => 
            obj is Vector2Mark && Equals((Vector2Mark)obj);

        protected bool Equals(Vector2Mark other) => 
            Equals(Point, other.Point) && Marked == other.Marked;

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Point?.GetHashCode() ?? 0)*397) ^ Marked.GetHashCode();
            }
        }
    }
}
