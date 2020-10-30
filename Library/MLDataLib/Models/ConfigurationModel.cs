using System.IO;

namespace MLDataLib.Models
{
   public class DataSetPath
    {
        public DataElementPath Iris { get; set; }
    }

    public class DataElementPath
    {
        private string _ML { get; set; }
        private string _Test { get; set; }
        public string ML { 
            get {
                return _ML; 
            }
            set {
                _ML = Directory.GetCurrentDirectory() + "\\Data\\" + value;
            }
        }
        public string Test {
            get {
                return _Test;
            }
            set {
                _Test = Directory.GetCurrentDirectory() + "\\Data\\" + value;
            }
        }
    }
}
