﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KGG
{
    public class Vector3
    {
        public Vector3()
        {
        }

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public double XX => -X / (2 * Math.Sqrt(2)) + Y; 
        public double YY => X / (2 * Math.Sqrt(2)) - Z;
        public double ZZ => X;

        public override string ToString() => $"({X};{Y};{Z})";

        public static bool TryParse(string text, out Vector3 result)
        {
            try
            {
                result = Parse(text);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        public static Vector3 Parse(string text)
        {
            var splt = text.Split(';').Select((a)=> a == "" ? 0 : double.Parse(a));
            if (splt.Count()==3)
                return new Vector3();
            throw new FormatException();
        }
    }
}
