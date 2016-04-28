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

namespace KGG_Task_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly KggCanvas.Color _color = KggCanvas.Color.Black;
        private readonly Vector2[] _neighborTop = { new Vector2(0, 1), new Vector2(1,1), new Vector2(1, 0) };
        private readonly Vector2[] _neighborButtom = { new Vector2(0, -1), new Vector2(1, -1), new Vector2(1, 0) };
        Vector2 previousPoint;
        Vector2 currentPoint;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            double a, b, c, d;
            var button = (Button) sender;
            button.IsEnabled = false;
            kggCanvas.Clear();
            try
            {
                a = Convert.ToDouble(textBoxA.Text.Replace('.', ','));
                b = Convert.ToDouble(textBoxB.Text.Replace('.', ','));
                c = Convert.ToDouble(textBoxC.Text.Replace('.', ','));
                d = Convert.ToDouble(textBoxD.Text.Replace('.', ','));
            }
            catch (FormatException)
            {
                MessageBox.Show("Text must be Double");
                return;
            }
            currentPoint = previousPoint = new Vector2(0, (int)Functions.F_Task2(0, a, b, c, d));
            while (currentPoint.X < kggCanvas.MapWidth)
            {
                kggCanvas.DrawPoint((int)currentPoint.X, (int)currentPoint.Y + kggCanvas.MapHeight/2, _color);
                await AsyncUpdateCurrentPoint(a, b, c, d);
                kggCanvas.Update();
            }
            button.IsEnabled = true;
        }
        
        private Task AsyncUpdateCurrentPoint(double a, double b, double c, double d) =>
            Task.Run(() => UpdateCurrentPoint(a, b, c, d));

        private void UpdateCurrentPoint(double a, double b, double c, double d)
        {
            var sign = Functions.Sign_dF_Task2_X(currentPoint.X, a, b, c, d);
            
            previousPoint = currentPoint;
            currentPoint = (sign < 0 ? _neighborButtom : _neighborTop)
                .Select(z => new Vector2(z.X + currentPoint.X, z.Y + currentPoint.Y))
                .OrderBy(x => Math.Abs(Functions.F_Task2(x.X, a, b, c, d) - x.Y))
                .First(x => !x.Equals(previousPoint));
        }

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
}
