using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopManager
{
    /// <summary>
    /// Interaction logic for ScatterExample.xaml
    /// </summary>
    public partial class ScatterExample : UserControl
    {
        private ScottPlot.Plot plt { get; set; }        
        private List<Data> DataList { get; set; }
        private List<Data> NeuronsList { get; set; }
        public class Data
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
        public ScatterExample()
        {
            InitializeComponent();
            NeuronsList = new List<Data>();
            DataList = new List<Data>();
            var r = new Random();


            var plt = new ScottPlot.Plot(400, 300);           

            for (var i = 0; i < 10; i++)
            {
                var x = 0;
                var y = 0;
                do
                {
                    x = r.Next(0, 1000);
                    y = r.Next(0, 1000);
                } while (DataList.Any(v => Math.Sqrt(Math.Pow(v.X - x, 2) + Math.Pow(v.Y - y, 2)) <= Int32.Parse(Density_min.Text)));
               

                wpfPlot1.plt.PlotPoint(x, y, System.Drawing.Color.Blue);
                DataList.Add(new Data() { X=x,Y=y});

                var r2 = new Random();
                for (var j = 0; j < 10; j++)
                {
                    var r3 = new Random();
                    var stepx = r3.Next(Int32.Parse(Step_Range_min.Text), Int32.Parse(Step_Range_max.Text));
                    var stepy = r3.Next(Int32.Parse(Step_Range_min.Text), Int32.Parse(Step_Range_max.Text));
                    var sx = r2.Next(x - stepx, x + stepx);
                    var sy = r2.Next(y - stepy, y + stepy);
                    wpfPlot1.plt.PlotPoint(sx, sy, System.Drawing.Color.Blue);
                    DataList.Add(new Data() { X = sx, Y = sy });

                }

                x = 0;
                y = 0;

                do
                {
                    x = r.Next(0, 1000);
                    y = r.Next(0, 1000);
                } while (NeuronsList.Any(v => Math.Sqrt(Math.Pow(v.X - x, 2) + Math.Pow(v.Y - y, 2)) <= Int32.Parse(Density_min.Text)));

                wpfPlot1.plt.PlotPoint(x, y, System.Drawing.Color.Red);
                NeuronsList.Add(new Data() { X = x, Y = y });

            }
            wpfPlot1.Render();

        }


        private void RandomizeNeuronsOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            NeuronsList = new List<Data>();
            wpfPlot1.plt.Clear();
            DataList.ForEach(x => wpfPlot1.plt.PlotPoint(x.X, x.Y, System.Drawing.Color.Blue));
            var x = 0;
            var y = 0;
            for (var i = 0; i < 10; i++)
            {
                do
                {
                    x = r.Next(0, 1000);
                    y = r.Next(0, 1000);
                } while (NeuronsList.Any(v => Math.Sqrt(Math.Pow(v.X - x, 2) + Math.Pow(v.Y - y, 2)) <= Int32.Parse(Density_min.Text)));

                wpfPlot1.plt.PlotPoint(x, y, System.Drawing.Color.Red);
                NeuronsList.Add(new Data() { X = x, Y = y });
            }
            wpfPlot1.Render();
        }


            private void RandomizeOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();

            DataList = new List<Data>();
            wpfPlot1.plt.Clear();
            NeuronsList.ForEach(x => wpfPlot1.plt.PlotPoint(x.X, x.Y, System.Drawing.Color.Red));
            for (var i = 0; i < 10; i++)
            {
                var x = 0;
                var y = 0;
                do
                {
                    x = r.Next(0, 1000);
                    y = r.Next(0, 1000);
                } while (DataList.Any(v => Math.Sqrt(Math.Pow(v.X - x, 2) + Math.Pow(v.Y - y, 2)) <= Int32.Parse(Density_min.Text)));



                wpfPlot1.plt.PlotPoint(x, y, System.Drawing.Color.Blue);
                DataList.Add(new Data() { X = x, Y = y });

                var r2 = new Random();
                for (var j = 0; j < 10; j++)
                {
                    var r3 = new Random();
                    var stepx = r3.Next(Int32.Parse(Step_Range_min.Text), Int32.Parse(Step_Range_max.Text));
                    var stepy = r3.Next(Int32.Parse(Step_Range_min.Text), Int32.Parse(Step_Range_max.Text));
                    var sx = r2.Next(x - stepx, x + stepx);
                    var sy = r2.Next(y - stepy, y + stepy);
                    wpfPlot1.plt.PlotPoint(sx, sy, System.Drawing.Color.Blue);
                    DataList.Add(new Data() { X = sx, Y = sy });

                }

            }
            wpfPlot1.Render();
        }

        private void ScatterSeries_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
