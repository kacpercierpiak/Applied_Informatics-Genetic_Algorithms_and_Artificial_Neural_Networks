using DesktopManager.Service;
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
        private DataGeneratorService dataService {get;set;}
        private WTAService wtaService { get; set; }
        private readonly int groupQty = 10;
        private readonly int elementQty = 20;


        public ScatterExample()
        {
            InitializeComponent();
            dataService = new DataGeneratorService();
            dataService.GenerateData(Int32.Parse(Range_min.Text), Int32.Parse(Range_max.Text), Int32.Parse(Step_Range_min.Text), Int32.Parse(Step_Range_max.Text), groupQty, elementQty, Int32.Parse(Density_min.Text));
            dataService.GenerateEmptyNeurons(groupQty);      
            GenerateChart(dataService.dataGroup, dataService.neurons, wpfPlot1);
           

        }

        private void GenerateChart(List<DataGroup> dataGroups, List<DataGroup> neurons, ScottPlot.WpfPlot wpfPlot)
        {

            dataGroups.ForEach(e => {
                wpfPlot.plt.PlotPoint(e.Position.X, e.Position.Y, System.Drawing.Color.Blue);
                e.SubPoints.ForEach(se => wpfPlot.plt.PlotPoint(se.X, se.Y, System.Drawing.Color.Blue));
            });

            neurons.ForEach(e => {
                wpfPlot.plt.PlotPoint(e.Position.X, e.Position.Y, System.Drawing.Color.Red);
            });
            wpfPlot.Render();
        }


        private void RandomizeNeuronsOnClick(object sender, RoutedEventArgs e)
        {
            wpfPlot1.plt.Clear();
            dataService.GenerateNeurons(Int32.Parse(Range_min.Text), Int32.Parse(Range_max.Text), groupQty, Int32.Parse(Density_min.Text));
            GenerateChart(dataService.dataGroup, dataService.neurons, wpfPlot1);
        }
        private void RandomizeEmptyNeuronsOnClick(object sender, RoutedEventArgs e)
        {
            wpfPlot1.plt.Clear();
            dataService.GenerateEmptyNeurons(groupQty);
            GenerateChart(dataService.dataGroup, dataService.neurons, wpfPlot1);
        }


        private void RandomizeOnClick(object sender, RoutedEventArgs e)
        {
            wpfPlot1.plt.Clear();
            dataService.GenerateData(Int32.Parse(Range_min.Text), Int32.Parse(Range_max.Text), Int32.Parse(Step_Range_min.Text), Int32.Parse(Step_Range_max.Text), groupQty, elementQty, Int32.Parse(Density_min.Text));
     
            GenerateChart(dataService.dataGroup, dataService.neurons, wpfPlot1);
        }

        private void ScatterSeries_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void StartTraining(object sender, RoutedEventArgs e)
        {
            var balancing = BalancingEnable.IsChecked == true ? Int32.Parse(Balancing.Text) : 0;
            var isDisabler = false;
            var iterationDisable = 0;
            var cycleQty = 0;

            if(Iteration_dis_qty_Enable.IsChecked == true)
            {
                isDisabler = true;
                iterationDisable = Int32.Parse(Iteration_dis_qty.Text);
                cycleQty = Int32.Parse(PenaltyCycleNo.Text);
            }

            wtaService = new WTAService(dataService.dataGroup, dataService.neurons);
            wtaService.Calc(Double.Parse(Training_Ratio.Text), balancing,isDisabler,iterationDisable,cycleQty);
            wpfPlot2.plt.Clear();
            GenerateChart(wtaService.dataGroup, wtaService.neuronsList, wpfPlot2);
        }
    }
}
