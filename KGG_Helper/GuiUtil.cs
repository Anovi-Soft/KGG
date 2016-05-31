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
        public static void TextBoxValue(TextBox tb, TextChangedEventArgs e, Func<string, bool> parser)
        {
            if (parser(tb.Text)) return;
            var textChange = e.Changes.First();
            var iAddedLength = textChange.AddedLength;
            var iOffset = textChange.Offset;
            tb.Text = tb.Text.Remove(iOffset, iAddedLength);
        }

        public static bool ParserDouble(string s)
        {
            s = s.Trim().Replace('.', ',');
            double result;
            return double.TryParse(s, out result) || s=="-" || s=="+";
        }

        public static bool ParserInt(string s)
        {
            s = s.Trim().Replace('.', ',');
            if (!s.Any()) return true;
            int result;
            return int.TryParse(s, out result);
        }

        public static double ReadDouble(this TextBox textBox) =>
            double.Parse(textBox.Text.Replace('.', ','));
    }
}
