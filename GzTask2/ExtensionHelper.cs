using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using KGG;

namespace GzTask2
{
    internal static class ExtensionHelper
    {
        public static readonly int[] AllPoints = {1,2,3,4,6,7,8,9};
        private static Dictionary<int,Vector2> Map = new Dictionary<int, Vector2>
        {
            {7, new Vector2(-1,+1) }, {8, new Vector2(0,+1) }, {9, new Vector2(+1,+1) },
            {4, new Vector2(-1, 0) },                          {6, new Vector2(+1, 0) },
            {1, new Vector2(-1,-1) }, {2, new Vector2(0,-1) }, {3, new Vector2(+1,-1) }
        };

        private static IEnumerable<Vector2> GetNeighbours(this IEnumerable<int> points, Vector2 parent) =>
            points.Select(x => Map[x] + parent);

        public static IEnumerable<Vector2> GetNeighbours(this Vector2 parent, IEnumerable<int> points) =>
            points.GetNeighbours(parent);
        
        public static double ReadNumber(this TextBox textBox) =>
            Convert.ToDouble(textBox.Text.Replace(".", ","));

        public static bool NotIn<T>(this T obj, HashSet<T> set) => !In(obj, set);
        public static bool In<T>(this T obj, HashSet<T> set) =>
            obj != null && set.Contains(obj);
    }
}