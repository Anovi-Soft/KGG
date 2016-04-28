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
            var triLvl = uint.Parse(triLevel.Text);
            var allTriangles = new List<Triangle>();
            var cube = new Cube(Vector3.Parse(cubeFrom.Text),
                Vector3.Parse(cubeTo.Text),
                new[]
                {
                    KggCanvas.Color.Blue,
                    KggCanvas.Color.Red,
                    KggCanvas.Color.Green,
                    KggCanvas.Color.Blue,
                    KggCanvas.Color.Red,
                    KggCanvas.Color.Green
                });
            allTriangles.AddRange(cube.Triangulation(triLvl));

            allTriangles.Sort((a,b) => a.ZZ.CompareTo(b.ZZ));
            allTriangles.ForEach(kggCanvas.DrawTriangle);
            kggCanvas.Update();
            //var triangle = new Triangle(new Vector3(120, 200, 20), new Vector3(160, 250, 20), new Vector3(200, 200, 20))
            //{
            //    Color = KggCanvas.Color.Blue
            //};

            //triangle.Triangulation(uint.Parse(triLevel.Text)).ForEach(kggCanvas.DrawTriangle);
        }
    }
}
