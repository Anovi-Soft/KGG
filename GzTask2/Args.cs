using System;
using System.Collections.Generic;
using System.Linq;
using KGG;

namespace GzTask2
{
    internal class Args
    {
        private readonly double a;
        private readonly double b;
        private readonly double c;
        private readonly double d;

        public Args(double a, double b, double c, double d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        public IEnumerable<Vector2> Points(double left, double right)
        {
            var leftPoints = PointsInternal(left, 1, 4, 7);
            var rightPoints = PointsInternal(right, 3, 6, 9);
            return leftPoints.Concat(rightPoints);
        }

        private IEnumerable<Vector2> PointsInternal(double startX, params int[] badPoints)
        {
            var upTargetThread = PointsThreadInternal(startX, GetDownY, badPoints, 1, 2, 3).GetEnumerator();
            var downTargetThread = PointsThreadInternal(startX, GetUpY, badPoints, 7, 8, 9).GetEnumerator();
            do
            {
                if (!downTargetThread.MoveNext() || !upTargetThread.MoveNext()) break;
                yield return downTargetThread.Current;
                yield return upTargetThread.Current;

            } while (downTargetThread.Current.Y > upTargetThread.Current.Y);
            downTargetThread.Dispose();
            upTargetThread.Dispose();
        }

        private IEnumerable<Vector2> PointsThreadInternal(double startX, Func<double, double> getY, int[] badPoints, params int[] anotherBadPoInts)
        {
            var totalBadPoints = new HashSet<int>(badPoints.Concat(anotherBadPoInts).Distinct());
            var points = ExtensionHelper.AllPoints.Where(x => !totalBadPoints.Contains(x)).ToList();
            var current = new Vector2((int)startX, (int)getY(startX));
            yield return current;
            while (true)
            {
                yield return current = current
                    .GetNeighbours(points)
                    .OrderBy(SolveAbs)
                    .First();
            }
        }

        private double Solve(double x, double y) => Pow(x - a)/Pow(b) - Pow(y - c)/Pow(d) - 1;
        private double SolveAbs(Vector2 point) => SolveAbs(point.X, point.Y);
        private double SolveAbs(double x, double y) => Math.Abs(Solve(x, y));
        private static double Pow(double value) => value*value;

        private double GetUpY(double x)  =>(Pow(b)*c + GetSameY(x))/Pow(b);
        private double GetDownY(double x)=>(Pow(b)*c - GetSameY(x))/Pow(b);

        private double GetSameY(double x) => 
            Math.Sqrt(Pow(a)*Pow(b)*Pow(d) -
                      2*a*Pow(b)*Pow(d)*x +
                      Pow(Pow(b))*-Pow(d) +
                      Pow(b)*Pow(d)*Pow(x));
        
    }
}