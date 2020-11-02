using MLDataLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTA.Model;

namespace WTA.Services
{
    class NetworkService : INetworkService
    {
        private IOutputService _outputService { get; set; }

        public NetworkService(IOutputService outputService)
        {
            _outputService = outputService;
        }
        private double SimiliarityCalc(WTANeuronModel neuron, List<double> iteration)
        {
            var d2 = 0d;
            for(int i=0;i<iteration.Count;i++)
            {
                d2 += Math.Pow((iteration[i] - neuron.InputsWeight[i]), 2);
            }
            
            return Math.Sqrt(d2);
        }

        private WinningNeuronModel winningNeuron(List<WTANeuronModel> neurons, List<double> iteration)
        {
            WinningNeuronModel winningNeuron = new WinningNeuronModel() { Value = null, Id = 0 };
            neurons.ForEach(n =>
            {
                var d = SimiliarityCalc(n, iteration);
                _outputService.SimilarityLog(n.Id+1,d);

                if (winningNeuron.Value == null || d < winningNeuron.Value)
                {
                    winningNeuron.Value = d;
                    winningNeuron.Id = n.Id;
                }

            });
            return winningNeuron;
        }

        private WTAMLModel GetNewWeight(WTAMLModel data, WinningNeuronModel winningNeuron, List<double> iteration)
        {
            var weight = data.Neurons.Find(p => p.Id == winningNeuron.Id).InputsWeight.ToList();
            for (int i = 0; i < weight.Count(); i++)
            {
                weight[i] = Math.Round(weight[i] + (0.5 * (iteration[i] - weight[i])), 3);
            }
            data.Neurons.Find(p => p.Id == winningNeuron.Id).InputsWeight = weight;
            return data;
        }

        public void Calc(WTAMLModel data)
        {
            _outputService.InitInformation(data.Neurons);
            for (var it = 0; it < data.Iterations.Count; it++)
            {                

                var iteration = data.Iterations[it];

                _outputService.WriteIterationData(iteration,it+1);

                WinningNeuronModel winningNeuron = this.winningNeuron(data.Neurons, iteration);

                _outputService.WriteWinningNeuron(winningNeuron.Id + 1);      
               
                data = GetNewWeight(data, winningNeuron, iteration);

                _outputService.WriteNeuronState(data.Neurons);       
            };
        }
    }
}
