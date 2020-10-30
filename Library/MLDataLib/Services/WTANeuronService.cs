using System;
using System.Collections.Generic;
using System.Text;
using WTA.Model;

namespace MLDataLib.Services
{
    
    public class WTANeuronService
    {

        public double GetSimilarity(WTANeuronModel neuron, List<double> inputs)
        {
            if (inputs.Count != neuron.InputsWeight.Count)
                throw new System.ArgumentException("Lenght of inputs array must be the same to neuron inputs", "inputs");
            var d = Math.Pow(inputs[0] - neuron.InputsWeight[0], 2);
            for(var i = 1; i < inputs.Count; i++)
            {
                d += Math.Pow(inputs[i] - neuron.InputsWeight[i], 2);
            }
            d = Math.Sqrt(d);
            return d;
        } 

        public List<double> SetWeight(List<double> InputsWeight, List<double> inputs, double LearningRate)
        {
            var result = new List<double>();
            if (inputs.Count != InputsWeight.Count)
                throw new System.ArgumentException("Lenght of inputs array must be the same to neuron inputs", "inputs");

            for (var i = 0; i < inputs.Count; i++)
            {
                result.Add(InputsWeight[i] + (LearningRate * (inputs[i] - InputsWeight[i])));
            }

            return InputsWeight;
        }

    }
}
