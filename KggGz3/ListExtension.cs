using System.Collections.Generic;
using System.Linq;

namespace KggGz3
{
    public static class ListExtension
    {
        public static T SafeGet<T>(this List<T> list, int index) =>
            list[(index + list.Count)%list.Count];

        public static T Pop<T>(this List<T> self)
        {
            var result = self.First();
            self.RemoveAt(0);
            return result;
        }
    }
}