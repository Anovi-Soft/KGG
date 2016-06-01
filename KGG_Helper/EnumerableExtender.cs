using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGG_Helper
{
    public static class EnumerableExtender
    {
        public static int IndexFirst<T>(this IEnumerable<T> array, Func<T, bool> func)
        {
            var enumerator = array.GetEnumerator();
            for(var i=0; enumerator.MoveNext(); i++)
                if (func(enumerator.Current))
                    return i;
            return -1;
        }

        public static IEnumerable<T> NeighborsDistinct<T>(this IEnumerable<T> array) =>
            array.First().Equals(array.Last()) ? array.Skip(1) : array;
        private static IEnumerable<T> NeighborsDistinctHelper<T>(this IEnumerable<T> array) 
        {
            var enumerator = array.GetEnumerator();
            var prev = default(T);
            while (enumerator.MoveNext())
            {
                if (!enumerator.Current.Equals(prev))
                    yield return enumerator.Current;
                prev = enumerator.Current;
            }
        }
    }
}
