using MLDataLib.Models;
using MLDataLib.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WTA.Model;

namespace MLDataLib.Services
{
    public class WTANeuronIteration : WTANeuronModel
    {
        public double Similarities { get; set; }

        public WTANeuronIteration(){}
        public WTANeuronIteration(WTANeuronModel parent) {
            this.InputsWeight = parent.InputsWeight;
            this.Id = parent.Id;
        }

    }

     public class WTANetworkStructure
     {

        private WTANeuronService NeuronService { get; set; } = new WTANeuronService();
        public List<WTANeuronModel> ActualNeurons { get; private set; } = new List<WTANeuronModel>();
        public List<WTAIteration> Iterations { get; private set; } = new List<WTAIteration>();
        public double LearningRate { get; set; } = 0.5;


        public WTANetworkStructure(List<WTANeuronModel> neurons)
        {
            ActualNeurons = neurons.ToList();
            Iterations = new List<WTAIteration>();
            var Iteration = new WTAIteration();
            Iteration.NeuronsState = neurons.Select(x => new WTANeuronIteration() { Id = x.Id, InputsWeight = x.InputsWeight }).ToList();
            Iterations.Add(Iteration);
        }

 
        private double GetSimilarity(WTANeuronModel neuron, List<double> inputs)
        {
            return NeuronService.GetSimilarity(neuron, inputs);
        }

        private void GenerateSimilarity(List<double> inputs)
        {
            var iteration = new WTAIteration();
            iteration.Id = Iterations.Count;           
            iteration.NeuronsState = ActualNeurons.Select(x => new WTANeuronIteration() { Id = x.Id, InputsWeight = x.InputsWeight, Similarities = GetSimilarity(x, inputs) }).ToList();
           
            Iterations.Add(iteration);
        }
        private WTANeuronIteration IncreaseWeight(WTANeuronIteration bestNeuron, List<double> inputs)
        {

            bestNeuron.InputsWeight = NeuronService.SetWeight(bestNeuron.InputsWeight, inputs, LearningRate);            
            return bestNeuron;

        }
        public void TeachNetwork(List<List<double>> iterations)
        {

            iterations.ForEach(inputs => {
                GenerateSimilarity(inputs);
                var bestNeuron = this.Iterations.Last().NeuronsState.OrderBy(x => x.Similarities).First();
                bestNeuron = IncreaseWeight(bestNeuron, inputs);

               // ActualNeurons = this.Iterations.Last().NeuronsState;


                });

        }

    }
    public class WTAIteration
    {
        public int Id { get; set; } = 0;
        private List<decimal> inputs { get; set; }
        public List<WTANeuronIteration> NeuronsState { get; set; } = new List<WTANeuronIteration>();        
             

        public List<decimal> Inputs
        {
            get { return inputs; }
            set
            {
                inputs = value.ToList();
            }       
        }
    }
    public class WTANetworkService
    {        
        private WTANetworkStructure wtaNetworkStructure { get; set; }
        private List<List<double>> iterations  { get; set; }
        public WTANetworkService(WTAMLModel mlModel)
        {
            this.wtaNetworkStructure = new WTANetworkStructure(mlModel.Neurons);
            this.iterations = mlModel.Iterations;
        }

        public WTANetworkStructure RunTeaching()
        {
            wtaNetworkStructure.TeachNetwork(iterations);
            return wtaNetworkStructure;
        }


    }
}
