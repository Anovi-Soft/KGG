using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KGG_5
{
    class Thor
    {
        private Vector centerR1;
        private double r1, r2;
        private Color front, back;
        public Thor (Vector center, double r1, double r2, Color front, Color back)
        {
            this.front = front;
            this.back = back;
            this.centerR1 = center;
            this.r1 = r1;
            this.r2 = r2;
        }
        private List<Vector> PointsR2 (int m)
        {
            List<Vector> result = new List<Vector>();
            Vector centerR2 = new Vector(centerR1.X, centerR1.Y + r1, centerR1.Z);
            for (double a = 0; a <= 360; a += 360/m )
                result.Add(
                    new Vector(
                        centerR2.X,
                        centerR2.Y + Math.Cos(Math.PI / 180 * a) * r2,
                        centerR2.Z + Math.Sin(Math.PI / 180 * a) * r2
                    ));
            return result;
        }
        private List<Vector> PointsR1(int n, Vector point)
        {
            double newRadius = point.Y - centerR1.Y;
            List<Vector> result = new List<Vector>();
            for (double a = 0; a <= 360; a += 360 / n)
                result.Add(
                    new Vector(
                        centerR1.X + Math.Sin(Math.PI / 180 * a) * newRadius,
                        centerR1.Y + Math.Cos(Math.PI / 180 * a) * newRadius,
                        point.Z
                    ));

            return result;
        }
        public List<Figure> Parts(int n, int m)
        {
            List<Figure> result = new List<Figure>();
            var examplePoints = PointsR2(m);
            for (int i = 0; i < examplePoints.Count-1; i++ )
            {
                var start = PointsR1(n, examplePoints[i]);
                var end = PointsR1(n, examplePoints[i+1]);
                for (int j = 0; j < start.Count-1; j++)
                    result.AddRange(
                        new Rectangle(start[j], start[j+1], end[j], end[j+1])
                        .Triangulation(0));
            }

            result.Sort((t1, t2) => t1.ZZ().CompareTo(t2.ZZ()));
            double minZ = result.First().ZZ(),
                maxZ = result.Last().ZZ(),
                sizeZ = maxZ - minZ;
            foreach (var fig in result)
            {
                double coefFront = (fig.ZZ() - minZ) / sizeZ,
                    coefBack = 1.0 - coefFront;
                var color = new Color();
                color.A = (byte)(front.A * coefFront + back.A * coefBack);
                color.R = (byte)(front.R * coefFront + back.R * coefBack);
                color.G = (byte)(front.G * coefFront + back.G * coefBack);
                color.B = (byte)(front.B * coefFront + back.B * coefBack);
                fig.SetBrush(new SolidColorBrush(color));

            }


            return result;
        }
    }
}
