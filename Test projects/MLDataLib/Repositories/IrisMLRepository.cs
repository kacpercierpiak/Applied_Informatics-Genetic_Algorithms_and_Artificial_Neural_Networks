using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using MLDataLib.Models;

namespace MLDataLib.Repositories
{
    public class IrisMLRepository
    {
        private IConfiguration _configuration { get; set; }
        private DataSetPath dataSetPath { get; set; } = new DataSetPath();
        public IrisMLRepository()
        {
            _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()  
            .Build();          
            _configuration.GetSection("DataSetPath").Bind(dataSetPath);              
        }

        public IrisMLModel GetDataFromFiles()
        {
            var irisMLModel = new IrisMLModel();
            irisMLModel.LearningData = ReadDataFromFile(dataSetPath.Iris.ML);
            irisMLModel.TestData = ReadDataFromFile(dataSetPath.Iris.Test);
            return irisMLModel;
        }

        private List<IrisData> ReadDataFromFile(string path)
        {
            var result = new List<IrisData>();
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var record = new IrisData();
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    record.SepalLenght = decimal.Parse(values[0]);
                    record.SepalWidth = decimal.Parse(values[1]);
                    record.PetalLenght = decimal.Parse(values[2]);
                    record.PetalWidth = decimal.Parse(values[3]);
                    record.IrisClass = values[4] switch
                    {
                        "Iris-setosa" => IrisClassEnum.IrisSetosa,
                        "Iris-versicolor" => IrisClassEnum.IrisVersicolour,
                        "Iris-virginica" => IrisClassEnum.IrisVirginica,
                        _ => IrisClassEnum.Unknown
                    };

                    result.Add(record);
                }
            }
            return result;
        }


    }
}
