using System;
using System.Collections.Generic;

namespace KggGz3
{
    public class FakeKeyComparer<TKey>:IComparer<TKey> where TKey : IComparable
    {
        public int Compare(TKey x, TKey y)
        {
            var result = x.CompareTo(y);
            return result == 0 ? 1 : result;
        }
    }
}