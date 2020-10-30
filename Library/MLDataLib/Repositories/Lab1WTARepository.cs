using MLDataLib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using WTA.Model;

namespace MLDataLib.Repositories
{

    public class Lab1WTARepository
    {
        private List<WTANeuronModel> GetNeurons()
        {
            var neurons = new List<WTANeuronModel>();
            neurons.Add(new WTANeuronModel() { Id = 0, InputsWeight = new List<double> { 0.2, 0.8, 0.7, 0.1 } });
            neurons.Add(new WTANeuronModel() { Id = 1, InputsWeight = new List<double> { 0.4, 0.4, 0.2, 0.8 } });
            neurons.Add(new WTANeuronModel() { Id = 2, InputsWeight = new List<double> { 0.8, 0.8, 0.1, 0.5 } });
            return neurons;
        }
        private List<List<double>> GetIterationData()
        {
            var result = new List<List<double>>();
            result.Add(new List<double> { 0.4, 0.3, 0.1, 0.7 });
            result.Add(new List<double> { 0.8, 0.8, 0.8, 0.7 });
            result.Add(new List<double> { 0.2, 0.5, 0.2, 0.1 });
            result.Add(new List<double> { 0.5, 0.3, 0.2, 0.7 });
            return result;
        }

        public WTAMLModel GetDataFromFiles()
        {
            var result = new WTAMLModel();
            result.Neurons = GetNeurons();
            result.Iterations = GetIterationData();
            return result;

        }

    }
}
