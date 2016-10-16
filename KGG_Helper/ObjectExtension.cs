using System;

namespace KGG
{
    public static class ObjectExtension
    {
        public static T2 Use<T1, T2>(this T1 obj, Func<T1, T2> factory) =>
            factory(obj);
    }
}