using System;
using System.Collections.Generic;
using System.Text;
using WTA.Model;

namespace MLDataLib.Models
{
    public class WTAMLModel
    {
        public List<WTANeuronModel> Neurons { get; set; }
        public List<List<double>> Iterations { get; set; }
    }
}
