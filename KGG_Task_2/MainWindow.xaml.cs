using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KGG;
using KGG_Helper;

namespace KGG_Task_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly KggCanvas.Color _color = KggCanvas.Color.Black;
        private readonly Vector2[] _neighborTop = { new Vector2(0, 1), new Vector2(1,1), new Vector2(1, 0), new Vector2(1, -1) };
        private readonly Vector2[] _neighborButtom = { new Vector2(0, -1), new Vector2(1, -1), new Vector2(1, 0), new Vector2(1, 1) };
        private PointHelp PreviousPoint;
        double a, b, c, d;

        private Vector2[] Neighboors =>
            PreviousPoint.Sign < 0
                ? _neighborButtom
                : _neighborTop;
            
        //Vector2 currentPoint;
        //Vector2 currentRealPoint;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            button.IsEnabled = false;
            kggCanvas.Clear();
            try
            {
                a = textBoxA.ReadDouble();
                b = textBoxB.ReadDouble();
                c = textBoxC.ReadDouble();
                d = textBoxD.ReadDouble();
            }
            catch (FormatException)
            {
                MessageBox.Show("Text must be Double");
                return;
            }

            var tops = Enumerable.Range(0, kggCanvas.MapWidth)
                .Select(x => new PointHelp
                {
                    Point = new Vector2(x, Functions.F_Task2(x,a,b,c,d)).AsIntVector(),
                    Sign = Functions.Sign_dF_Task2_X(x, a, b, c, d)
                });
            tops = Sub(tops);
            PreviousPoint = tops.First();
            tops = tops.Skip(1)
                .Concat(new[] 
                {
                    new PointHelp
                    {
                        Point = new Vector2(kggCanvas.MapWidth, Functions.F_Task2(kggCanvas.MapWidth,a,b,c,d)).AsIntVector(),
                        Sign = Functions.Sign_dF_Task2_X(kggCanvas.MapWidth, a, b, c, d)
                    }
                });
            foreach (var top in tops)
            {
                MoveDraw(PreviousPoint, top);
                PreviousPoint = top;
            }

            kggCanvas.Update();
            

            //currentRealPoint = new Vector2(0, Functions.F_Task2(0, a, b, c, d));
            //currentPoint = currentRealPoint.AsIntVector();
            //while (currentPoint.X < kggCanvas.MapWidth)
            //{
            //    kggCanvas.DrawPoint((int)currentPoint.X, (int)currentPoint.Y + kggCanvas.MapHeight/2, _color);
            //    await AsyncUpdateCurrentPoint(a, b, c, d);
            //    kggCanvas.Update();
            //}
            
            button.IsEnabled = true;
        }

        private void MoveDraw(PointHelp previousPoint, PointHelp top)
        {
            var point = previousPoint.Point;
            do
            {
                kggCanvas.DrawPoint((int) point.X, (int) point.Y + kggCanvas.MapHeight/2, _color);
                point = Neighboors
                    .Select(z => new Vector2(z.X + point.X, z.Y + point.Y))
                    .OrderBy(x => Math.Abs(Functions.F_Task2(x.X, a, b, c, d) - x.Y))
                    .First()
                    .AsIntVector();
            } while (point.X < top.Point.X);
        }

        private IEnumerable<PointHelp> Sub(IEnumerable<PointHelp> tops)
        {
            var enumerator = tops.GetEnumerator();
            enumerator.MoveNext();
            var sign = enumerator.Current.Sign;
            yield return enumerator.Current;
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Sign == sign) continue;
                sign = enumerator.Current.Sign;
                yield return enumerator.Current;
            }
        }

        private Task AsyncUpdateCurrentPoint(double a, double b, double c, double d) =>
            Task.Run(() => UpdateCurrentPoint(a, b, c, d));

        private void UpdateCurrentPoint(double a, double b, double c, double d)
        {
            
        }
        //private void UpdateCurrentPoint(double a, double b, double c, double d)
        //{
        //    var PreviousPoint = Functions.Sign_dF_Task2_X(currentPoint.X, a, b, c, d);

        //    var neighbor =
        //        //PreviousPoint == 0
        //        //? new Vector2[] { new Vector2(1, 0) } :
        //        PreviousPoint < 0
        //        ? _neighborButtom
        //        : _neighborTop;

        //    currentRealPoint = neighbor
        //        .Select(z => new Vector2(z.X + currentRealPoint.X, z.Y + currentRealPoint.Y))
        //        .OrderBy(x => Math.Abs(Functions.F_Task2(x.X, a, b, c, d) - x.Y))
        //        .First();
        //    currentPoint = currentRealPoint.AsIntVector();
        //}

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            GuiUtil.TextBoxValue((TextBox)sender, e, GuiUtil.ParserDouble);
        }

        private new void LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && !textBox.Text.Trim().Any())
                textBox.Text = "0";
        }
    }

    class PointHelp
    {
        public Vector2 Point { get; set; }
        public int Sign { get; set; }
    }
}
