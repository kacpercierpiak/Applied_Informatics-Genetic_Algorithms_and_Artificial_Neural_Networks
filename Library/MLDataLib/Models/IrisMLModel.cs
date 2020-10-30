using System.Collections.Generic;

namespace MLDataLib.Models
{
    public enum IrisClassEnum { IrisSetosa, IrisVersicolour, IrisVirginica, Unknown }
    public class IrisMLModel
    {
        public List<IrisData> LearningData { get; set; } = new List<IrisData>();
        public List<IrisData> TestData { get; set; } = new List<IrisData>();
    }
    public class IrisData
    {        
        public decimal SepalLenght { get; set; }
        public decimal SepalWidth { get; set; }
        public decimal PetalLenght { get; set; }
        public decimal PetalWidth { get; set; }
        public IrisClassEnum IrisClass { get; set; } = new IrisClassEnum();  

    }


}
