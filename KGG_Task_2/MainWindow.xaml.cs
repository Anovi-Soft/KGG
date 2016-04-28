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
        KggCanvas.Color color = KggCanvas.Color.Black;
        int zoom = 10;
        double a, b, c, d;
        Vector2[] neighborTop = { new Vector2(0, 1), new Vector2(1,1), new Vector2(1, 0) };
        Vector2[] neighborButtom = { new Vector2(0, -1), new Vector2(1, -1), new Vector2(1, 0) };
        private Vector2[] neighborRight = {new Vector2(1, 0)};
        Vector2 previousPoint;
        Vector2 currentPoint;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
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
                kggCanvas.DrawPoint((int)currentPoint.X, (int)currentPoint.Y + kggCanvas.MapHeight/2, color);
                await AsyncUpdateCurrentPoint();
                kggCanvas.Update();
            }
            button.IsEnabled = true;
        }


        private Task AsyncUpdateCurrentPoint() =>
            Task.Run(() => UpdateCurrentPoint());

        private void UpdateCurrentPoint()
        {
            Vector2[] neighbor;
            var sign = Functions.Sign_dF_Task2_X(currentPoint.X, a, b, c, d);
            //if (sign == 0)
            //    neighbor = neighborRight;
            //else
                neighbor = sign < 0 ? neighborButtom : neighborTop;

            var y = Functions.F_Task2(currentPoint.X, a, b, c, d);
            var result = neighbor
                .Select(z => new Vector2(z.X + currentPoint.X, z.Y + currentPoint.Y))
                //.Where(x => x.Y != (previousPoint.Y < currentPoint.Y ? -1 : 1))
                .OrderBy(x => Math.Abs(Functions.F_Task2(x.X,a, b, c, d) - x.Y))
                .First(x=> !x.Equals(previousPoint));
            previousPoint = currentPoint;
            currentPoint = result;
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

        //void Tmp()
        //{
        //    var inputText = "cbabc"; 
        //    var subStrings = new List<string> {"aba", "bab", "cc"};
        //    var pattern = $"{string.Join("|", subStrings)}";
        //    var regex = new Regex(pattern);
        //    var result = regex.Replace(inputText, "");
        //}
    }
}
