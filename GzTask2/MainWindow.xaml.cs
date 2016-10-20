using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KGG;

namespace GzTask2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            button.IsEnabled = false;
            Draw();
            button.IsEnabled = true;
        }

        private void Draw()
        {
            double a, b, c, d;
            try
            {
                a = TextBoxA.ReadNumber();
                b = TextBoxB.ReadNumber();
                c = TextBoxC.ReadNumber();
                d = TextBoxD.ReadNumber();
            }
            catch (FormatException)
            {
                MessageBox.Show("Text must be double");
                return;
            }
            kggCanvas.Clear();
            var width = kggCanvas.MapWidth/2;
            var height = kggCanvas.MapHeight/2;
            foreach (var point in new Args(a, b, c, d).Points(-width, width))
            {
                var formatedPoint = new Vector2(width + point.X, height - point.Y);
                kggCanvas.DrawPoint(formatedPoint, KggCanvas.Color.Black);
            }
            kggCanvas.Update();
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
}