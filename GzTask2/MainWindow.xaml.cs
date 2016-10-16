using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KGG;

namespace GzTask2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly KggCanvas.Color PointColor = KggCanvas.Color.Black;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            double a, b, c;
            var button = (Button) sender;
            button.IsEnabled = false;
            kggCanvas.Clear();
            try
            {
                a = Convert.ToDouble(textBoxA.Text.Replace('.', ','));
                b = Convert.ToDouble(textBoxB.Text.Replace('.', ','));
                c = Convert.ToDouble(textBoxC.Text.Replace('.', ','));
            }
            catch (FormatException)
            {
                MessageBox.Show("Text must be Double");
                return;
            }
            foreach (var point in new Solver(a,b,c).Take(10020))
            {
                var x = point.X + kggCanvas.MapWidth / 2f;
                var y = -point.Y + kggCanvas.MapHeight;
                kggCanvas.DrawPoint((int)x, (int)y, PointColor);
            }
            kggCanvas.Update();
            button.IsEnabled = true;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            GuiUtil.TextBoxValue((TextBox) sender, e, GuiUtil.ParserDouble);
        }

        private new void LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && !textBox.Text.Trim().Any())
                textBox.Text = "0";
        }
    }

    public class Solver : IEnumerable<Vector2>
    {
        private static readonly double D = Math.Sqrt(2) / 2;
        private readonly double a;
        private readonly double b;
        private readonly double c;
        private double x = 0.1;
        private double y = 0.1;

        public Solver(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Vector2> GetEnumerator()
        {
            while (true)
            {
                yield return CurrentReal();
                Shift();
            }
        }

        private void Shift()
        {
            var sd = (y - 1)*(y - 1) - 2*P*(x - 1);
            var sv = (y + 1)*(y + 1) - 2*P*x;
            var sh = y*y - 2*P*(x + 1);
            var eps = 0.0001;
            if (DivMod(sv, sh) < eps)
            {
                x++;
                if (DivMod(sd, sh) < eps)
                    y++;
            }
            else
            {
                y++;
                if (DivMod(sv, sd) > eps)
                    x++;
            }

        }

        private static double DivMod(double left, double right) =>
            Math.Abs(left) - Math.Abs(right);

        private double P => -(a + b)/4*c;

        private Vector2 CurrentReal() => new Vector2(RealX, RealY);

        private double RealX => D*(MiddleX - MiddleY);
        private double RealY => D*(MiddleX + MiddleY);
        private double MiddleX => x + D*(b - a)*(b - a)/(8*c*(a + b));
        private double MiddleY => y - D*(b-a)/(4*c);
    }
}