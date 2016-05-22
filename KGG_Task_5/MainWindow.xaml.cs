using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using KGG;
using Polygon = System.Windows.Shapes.Polygon;

namespace KGG_Task_5
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

        private void TextBoxUInt(object sender, TextChangedEventArgs e) =>
            GuiUtil.TextBoxValue(sender as TextBox, e, s =>
            {
                uint a;
                return uint.TryParse(s, out a);
            });

        private void TextBoxVector3(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            var countSpliter = tb.Text.Count(a => a == ';');
            while (countSpliter < 2)
            {
                tb.Text += ";";
                countSpliter = tb.Text.Count(a => a == ';');
            }
            if (countSpliter > 2)
            {
                var textChange = e.Changes.First();
                var iAddedLength = textChange.AddedLength;
                var iOffset = textChange.Offset;
                tb.Text = tb.Text.Remove(iOffset, iAddedLength);
                return;
            }
            GuiUtil.TextBoxValue(tb , e, s =>
            {
                Vector3 a;
                return Vector3.TryParse(s, out a);
            });
        }
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            button.IsEnabled = false;

            kggCanvas.Clear();

            var triLvl = uint.Parse(triLevel.Text);

            var from = Vector3.Parse(cubeFrom.Text);
            var to = Vector3.Parse(cubeTo.Text);
            var cube = new Cube(from, to,
                new[]
                {
                    KggCanvas.Color.Blue,
                    KggCanvas.Color.Red,
                    KggCanvas.Color.Green,
                    KggCanvas.Color.Yelow,
                    KggCanvas.Color.Aqua,
                    KggCanvas.Color.Pink
                });

            var a = Vector3.Parse(pyrA.Text);
            var b = Vector3.Parse(pyrB.Text);
            var c = Vector3.Parse(pyrC.Text);
            var d = Vector3.Parse(pyrD.Text);
            var up = Vector3.Parse(pyrUp.Text);
            var pyramid = new Pyramid(a, b, c, d, up,
                new []
                {
                    KggCanvas.Color.Blue,
                    KggCanvas.Color.Red,
                    KggCanvas.Color.Green,
                    KggCanvas.Color.Yelow,
                    KggCanvas.Color.Aqua,
                });

            var triangles = cube.Triangulation(triLvl)
                .Concat(pyramid.Triangulation(triLvl)) 
                .OrderBy(x=>x.ZZ)
                .ToList();
            triangles.ForEach(kggCanvas.DrawTriangle);

            kggCanvas.Update();


            button.IsEnabled = true;
            //var triangle = new Triangle(new Vector3(120, 200, 20), new Vector3(160, 250, 20), new Vector3(200, 200, 20))
            //{
            //    Color = KggCanvas.Color.Blue
            //};

            //triangle.Triangulation(uint.Parse(triLevel.Text)).ForEach(kggCanvas.DrawTriangle);
        }
    }
}
