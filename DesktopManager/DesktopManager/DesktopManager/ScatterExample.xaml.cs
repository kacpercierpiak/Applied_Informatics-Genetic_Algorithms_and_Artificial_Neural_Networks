using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public ScatterExample()
        {
            InitializeComponent();

            var r = new Random();
            ValuesA = new ChartValues<ObservablePoint>();
            
           

            for (var i = 0; i < 10; i++)
            {
                var x = 0;
                var y = 0;
                do
                {
                    x = r.Next(0, 1000);
                    y = r.Next(0, 1000);
                } while (ValuesA.Any(v => Math.Sqrt(Math.Pow(v.X - x, 2) + Math.Pow(v.Y - y, 2)) <= Int32.Parse(Density_min.Text)));

                ValuesA.Add(new ObservablePoint(x, y));
                var r2 = new Random();
                for (var j = 0; j < Int32.Parse(Element_qty.Text); j++)
                {
                    var r3 = new Random();
                    var stepx = r3.Next(Int32.Parse(Step_Range_min.Text), Int32.Parse(Step_Range_max.Text));
                    var stepy = r3.Next(Int32.Parse(Step_Range_min.Text), Int32.Parse(Step_Range_max.Text));
                    ValuesA.Add(new ObservablePoint(r2.Next(x - stepx, x + stepx), r2.Next(y - stepy, y + stepy)));
                }

            }

            DataContext = this;
        }
        public string MaxVal { get; set; } = "1000";
        public string MinVal { get; set; } = "0";
     
        public ChartValues<ObservablePoint> ValuesA { get; set; }


        private void RandomizeOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();

            var count = 0;
            for (var i = 0; i < Int32.Parse(Groups_qty.Text); i++)
            {
               
                var x = 0;
                var y = 0;
                int m = 0;
                do
                {
                    
                    x = r.Next(Int32.Parse(Range_min.Text), Int32.Parse(Range_max.Text));
                    y = r.Next(Int32.Parse(Range_min.Text), Int32.Parse(Range_max.Text));
                    m++;
                } while (m<10 && ValuesA.Any(v => Math.Sqrt(Math.Pow(v.X - x, 2) + Math.Pow(v.Y - y, 2)) <= Int32.Parse(Density_min.Text)));
                ValuesA[(i * Int32.Parse(Element_qty.Text)) +i].X = x;
                ValuesA[(i * Int32.Parse(Element_qty.Text)) +i].Y = y;
                Debug.Write("M");
                Debug.WriteLine((i * Int32.Parse(Element_qty.Text)) + i);
                count++;
                for (var j = i+1; j <= i+ Int32.Parse(Element_qty.Text); j++)
                {
                    count++;
                    Debug.WriteLine((i * Int32.Parse(Element_qty.Text)) + j*1);
                    var stepx = r.Next(Int32.Parse(Step_Range_min.Text), Int32.Parse(Step_Range_max.Text));
                    var stepy = r.Next(Int32.Parse(Step_Range_min.Text), Int32.Parse(Step_Range_max.Text));
                    
                    if (r.Next(-1, 2)<= 0)
                    {
                        ValuesA[(i * Int32.Parse(Element_qty.Text)) + j].X = x + stepx;
                        if(r.Next(-1, 2) <= 0)
                        ValuesA[(i * Int32.Parse(Element_qty.Text)) + j].Y = y + stepy;
                        else
                            ValuesA[(i * Int32.Parse(Element_qty.Text)) + j].Y = y - stepy;
                    }
                    else {
                        ValuesA[(i * Int32.Parse(Element_qty.Text)) + j].X = x - stepx;
                        if(r.Next(-1, 2) <= 0)
                        ValuesA[(i * Int32.Parse(Element_qty.Text)) + j].Y = y - stepy;
                        else
                            ValuesA[(i * Int32.Parse(Element_qty.Text)) + j].Y = y + stepy;
                    }                  
                  
                }
                

            }
            for (int i = ValuesA.Count-1; i>=count; i--)
                ValuesA.RemoveAt(i);
        }

        private void ScatterSeries_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
