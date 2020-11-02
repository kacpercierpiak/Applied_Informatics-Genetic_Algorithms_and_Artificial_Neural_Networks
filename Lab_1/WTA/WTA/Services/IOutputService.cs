using System;
using System.Collections.Generic;
using System.Text;
using WTA.Model;

namespace WTA.Services
{
    interface IOutputService
    {
        public void WriteIterationData(List<double> data, int id);
        public void SimilarityLog(int id, double d);
        public void WriteWinningNeuron(int id);
        public void WriteNeuronState(List<WTANeuronModel> data);
        public void WriteNeuronData(List<WTANeuronModel> data);
        public void InitInformation(List<WTANeuronModel> data);
    }
}
