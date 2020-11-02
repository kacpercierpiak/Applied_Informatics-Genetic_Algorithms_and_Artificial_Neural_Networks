using System;
using System.Collections.Generic;
using System.Text;
using WTA.Model;

namespace WTA.Services
{
    public class OutputService : IOutputService
    {
        public void WriteIterationData(List<double> data,int id)
        {
            Console.WriteLine($"---------------ITERATION {id}---------------");
            Console.WriteLine("Inputs:");
            int i = 1;
            data.ForEach(y => {
                Console.Write($"x{i}:{y:N3} ");
                i++;
            });
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Similarity:");

        }

        public void SimilarityLog(int id, double d)
        {
            Console.WriteLine($"ne{id}: {d}");
        }

        public void WriteWinningNeuron(int id)
        {
            Console.WriteLine();
            Console.WriteLine($"Winner neuron: ne{id}");
            Console.WriteLine();
        }

        public void WriteNeuronState(List<WTANeuronModel> data)
        {
            Console.WriteLine("Neuron state");
            WriteNeuronData(data);
            Console.WriteLine();
            Console.WriteLine();
        }

        public void WriteNeuronData(List<WTANeuronModel> data)
        {

            data.ForEach(x => {
                int i = 1;
                Console.Write($"ne{x.Id + 1}: ");
                x.InputsWeight.ForEach(y => {
                    Console.Write($"w{x.Id + 1}{i}:{y:N3} ");
                    i++;
                });
                Console.WriteLine();
            });

        }

        public void InitInformation(List<WTANeuronModel> data)
        {
            Console.WriteLine("Neuron init state");
            WriteNeuronData(data);
            Console.WriteLine();
        }
    }
}
