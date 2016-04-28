using System;
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

namespace KGG_Task_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            kggCanvas.PixelSize = 10;
            kggCanvas.Arrows = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 500; i += 20)
            {
                kggCanvas.DrawPoint(i + 1, 300);
                kggCanvas.PixelSize *= 2;
                kggCanvas.Update();
            }
        }
    }
}
