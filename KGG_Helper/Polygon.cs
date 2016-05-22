using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG
{ 
    public class Polygon
    {
        public KGG.KggCanvas.Color Color;
        public List<Vector2Ext> Points { get; private set; }

        private List<Segment> _segments;
        public List<Segment> Segments => 
            _segments ?? 
            (_segments = Points.Select((x, i) => new Segment(x, Points[(i + 1)%Points.Count]))
            .ToList());

        public Polygon()
        {
            Points = new List<Vector2Ext>();
        }
        public Polygon(List<Vector2Ext> points, KggCanvas.Color color)
        {
            Color = color;
            Points = points;
        }

        public Polygon CutOff(Polygon secondPolygon, KggCanvas.Color red)
        {
            return this;
        }
        public Polygon CutOffOld(Polygon polygon, KggCanvas.Color color)
        {
            Points.ForEach(x => x.ContainsOnAnotherPoly = polygon.Contain(x));
            polygon.Points.ForEach(x => x.ContainsOnAnotherPoly = Contain(x));
            var result = new Polygon {Color = color};
            foreach (var segment in Segments)
            {
                Vector2Ext point = null;
                if (!segment.From.ContainsOnAnotherPoly)
                {
                    result.Points.Add(segment.From);
                    if (segment.To.ContainsOnAnotherPoly)
                    {
                        if (polygon.CrossPoint(segment, ref point))
                            result.Points.Add(point);
                    }
                }
                else if (!segment.To.ContainsOnAnotherPoly)
                {
                    result.Points.AddRange(polygon.Points.Where(x=>x.ContainsOnAnotherPoly).Reverse());
                    if (polygon.CrossPoint(segment, ref point))
                        result.Points.Add(point);
                }
            }
            result.Points = result.Points.Distinct().ToList();
            return result;
        }

        private bool CrossPoint(Segment segment, ref Vector2Ext vector)
        {
            foreach (var s in Segments)
            {
                if (s != null && s.TryCrossPoint(segment, out vector))
                    return true;
            }
            return false;
        }

        public bool Contain(Vector2 point)
        {
            return Segments.All(x => x.PlaceOfPoint(point) > 0);
        }

        public override string ToString() => Points.Skip(1)
            .Aggregate("["+Points.First(),
            (current, point) => current + "," + point) + "]";

    }
}
