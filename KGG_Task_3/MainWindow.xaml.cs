﻿using System;
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
using KGG;

namespace KGG_Task_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int size = 20;
        public MainWindow()
        {
            InitializeComponent();
            UpdateCanvas();
        }

        public void UpdateCanvas()
        {
            var firstPolygon = GetPolygon(GetFirstPoints(), KggCanvas.Color.Green);
            var secondPolygon = GetPolygon(GetSecondPoints(), KggCanvas.Color.Aqua);
            var result = firstPolygon.CutOff(secondPolygon, KggCanvas.Color.Red);
            kggCanvas.Clear();
            //kggCanvas.DrawPolygon(firstPolygon);
            //kggCanvas.DrawPolygon(secondPolygon);
            kggCanvas.DrawPolygon(result);
            kggCanvas.Update();
        }

        private Polygon GetPolygon(List<Vector2> points, KggCanvas.Color color = null)
        {
            return new Polygon(points
                .Select(x => new Vector2Ext(kggCanvas.Width / 2 + x.X * size, kggCanvas.Height / 2 - x.Y * size) )
                .ToList()
                ,color);
        }

        private List<Vector2> GetFirstPoints()
        {
            return new List<Vector2>
            {
                new Vector2(-4, 0),
                new Vector2(-2, 4),
                new Vector2(2, 4),
                new Vector2(4, 0),
                new Vector2(2, -4),
                new Vector2(-2, -4)
            };
        }
        private List<Vector2> GetSecondPoints()
        {
            return new List<Vector2>
            {
                new Vector2(5, 5),
                new Vector2(5, -2),
                new Vector2(-5, -2),
                new Vector2(-5, 5),
            };
        }
    }
}
