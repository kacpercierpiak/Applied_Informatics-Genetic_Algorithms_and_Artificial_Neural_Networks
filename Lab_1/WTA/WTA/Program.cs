using System;
using System.Collections.Generic;
using System.Linq;
using MLDataLib.Models;
using MLDataLib.Repositories;
using MLDataLib.Services;
using WTA.Model;

namespace WTA
{
    class Program
    {
        static void Main(string[] args)
        {
            var mlService = new MLDataService();

            WTAMLModel data = mlService.GetMLData(MLDataService.MLRepos.Lab1);
            Console.WriteLine("Neuron init state");
            WriteNeuronData(data.Neurons);
            Console.WriteLine();
            int it = 1;
            data.Iterations.ForEach(x =>
            {
                
                Console.WriteLine($"---------------ITERATION {it}---------------");                
                WriteIterationData(x);
                double? value = null;
                int winnerId = 0;
                Console.WriteLine();
                Console.WriteLine("Similarity:");
                data.Neurons.ForEach( n =>
                {
                    var d = Math.Sqrt(Math.Pow((x[0] - n.InputsWeight[0]),2) + Math.Pow((x[1] - n.InputsWeight[1]), 2) + Math.Pow((x[2] - n.InputsWeight[2]), 2) + Math.Pow((x[3] - n.InputsWeight[3]), 2));
                    Console.WriteLine($"ne{n.Id + 1}: {d}");

                    if(value == null || d < value)
                    {
                        value = d;
                        winnerId = n.Id;
                    }                                   
                  
                });
                Console.WriteLine();
                Console.WriteLine($"Winner neuron: ne{winnerId + 1}");
                Console.WriteLine();
                var weight = data.Neurons.Find(p => p.Id == winnerId).InputsWeight.ToList();
                for(int i =0;i<weight.Count();i++)
                {
                    weight[i] = Math.Round(weight[i] + (0.5 * (x[i] - weight[i])),3);
                }
                data.Neurons.Find(p => p.Id == winnerId).InputsWeight = weight;
                Console.WriteLine("Neuron state");
                WriteNeuronData(data.Neurons);
                Console.WriteLine();
                Console.WriteLine();

                it++;
            });





            Console.WriteLine("Hello World!");
        }

        public static void WriteIterationData(List<double> data)
        {            
            Console.WriteLine("Inputs:");
            int i = 1;
            data.ForEach(y => {
                Console.Write($"x{i}:{y:N3} ");
                i++;
            });
            Console.WriteLine();

        }

        public static void WriteNeuronData(List<WTANeuronModel> data)
        {            
            
            data.ForEach(x => {
                int i = 1;
                Console.Write($"ne{x.Id+1}: ");
                x.InputsWeight.ForEach(y => {
                Console.Write($"w{x.Id + 1}{i}:{y:N3} ");
                    i++;
                });
                Console.WriteLine();
            });

        }
    }
}
