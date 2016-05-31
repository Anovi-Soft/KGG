using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG
{
    public static class Functions
    {
        public static double F_xPowTwo(double x) =>
            Math.Pow(x, 2);

        public static double F_Task2(double x, double a, double b, double c, double d) =>
            a * Math.Sin(b * x + c) + d;

        public static int Sign_dF_Task2_X(double x, double a, double b, double c, double d)
        {
            var result = a * b * Math.Cos(b * x + c);
            //var eps = Math.Abs(a*b/7);
            return  Math.Sign(result);
        }

        //public static double F_Task2(double x, double a, double b, double c, double d) => 
        //    a*Math.Sin(b*x)*Math.Cos(c) + a*Math.Cos(b*x)*Math.Sin(c) + d;

        //public static int Sign_dF_Task2_X(double x, double a, double b, double c, double d)
        //{
        //    var result = a*b*(Math.Cos(c)*Math.Cos(b*x) - Math.Sin(c)*Math.Sin(b*x));
        //    return Math.Abs(result) < 0.0001
        //            ? 0
        //            : Math.Sign(result);
        //}
    }
}
