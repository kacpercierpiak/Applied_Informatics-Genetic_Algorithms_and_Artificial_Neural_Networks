using MLDataLib.Services;
using System;

namespace NeuralGas
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataService = new MLDataService();

            var t = dataService.GetMLData(MLDataService.MLRepos.Iris);

            Console.WriteLine("Hello World!");
        }
    }
}
