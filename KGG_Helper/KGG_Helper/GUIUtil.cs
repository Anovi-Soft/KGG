using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KGG
{
    public static class GuiUtil
    {
        public delegate bool Parser<T>(string s, out T i);
        public static void TextBoxValue<T>(TextBox tb, TextChangedEventArgs e, Parser<T> parser)
        {
            T o;
            if (parser(tb.Text, out o)) return;
            var textChange = e.Changes.First();
            var iAddedLength = textChange.AddedLength;
            var iOffset = textChange.Offset;
            tb.Text = tb.Text.Remove(iOffset, iAddedLength);
        }
    }
}
