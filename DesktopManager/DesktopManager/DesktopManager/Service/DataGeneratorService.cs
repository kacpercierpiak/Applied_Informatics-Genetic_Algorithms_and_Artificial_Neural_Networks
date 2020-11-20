using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopManager.Service
{
    class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class DataGroup
    {
        public Position Position { get; set; }
        public List<Position> SubPoints { get; set; }
    }


    class DataGeneratorService
    {
        public List<DataGroup> dataGroup { get; private set; }
        public List<DataGroup> neurons { get; private set; }
        Random random { get; set; }

        public DataGeneratorService()
        {
            dataGroup = new List<DataGroup>();
            
            neurons = new List<DataGroup>();
            random = new Random();
            
        }
        private double DistanceCalc(Position position, Position newPosition)
        {
            return Math.Sqrt(Math.Pow(position.X - newPosition.X, 2) + Math.Pow(position.Y - newPosition.Y, 2));
        }

        private bool isValidPosition(Position position, Position newPosition, int densinity)
        {
            if (DistanceCalc(position, newPosition) <= densinity)
                return true;
            else
                return false;          
        }

        private bool PositionVerification(Position position, List<DataGroup> dataGroup, int densinity)
        {

            if (dataGroup.Any(d => (isValidPosition(d.Position, position, densinity) || d.SubPoints.Any(p => isValidPosition(p, position, densinity)))))
                return false;
            else
                return true;
                  
        }
        private Position GeneratePoint(int min, int max, List<DataGroup> dataGroup, int densinity)
        {
            var position = new Position();
            random = new Random();
            do
            {
                position.X = random.Next(min, max);
                position.Y = random.Next(min, max);
            } while (!PositionVerification(position, dataGroup, densinity));

            return position;
        }
        

        private List<Position> GenerateSubPoints(Position position, int qty, int stepMin, int stepMax)
        {
            var positionList = new List<Position>();
            random = new Random();
            for (int i = 0;i< qty;i++)
            {
                var subPosition = new Position();
                var stepx = random.Next(stepMin, stepMax);
                var stepy = random.Next(stepMin, stepMax);
                subPosition.X = random.Next(position.X - stepx, position.X + stepx);
                subPosition.Y = random.Next(position.Y - stepy, position.Y + stepy);
                positionList.Add(subPosition);

            }
            return positionList;
        }
        
        public void GenerateNeurons(int min, int max, int groupQty, int densinity)
        {
            neurons.Clear();         
            
            for (int i = 0; i < groupQty; i++)
            {
                var neuron = new DataGroup();
                neuron.SubPoints = new List<Position>();
                neuron.Position = GeneratePoint(min, max, neurons, densinity);
                neurons.Add(neuron);
            }
        }

        public void GenerateEmptyNeurons(int groupQty)
        {
            neurons.Clear();

            for (int i = 0; i < groupQty; i++)
            {
                var neuron = new DataGroup();
                neuron.SubPoints = new List<Position>();
                neuron.Position =new Position() { X=0, Y=0};
                neurons.Add(neuron);
            }
        }

        public void GenerateData(int min, int max, int stepMin, int stepMax, int groupQty, int elementQty, int densinity)
        {
            dataGroup.Clear();
           
            for (int i = 0; i < groupQty; i++)
            {
                var data = new DataGroup();
                data.Position = GeneratePoint(min, max, dataGroup, densinity);
                data.SubPoints = GenerateSubPoints(data.Position, elementQty, stepMin, stepMax);
                dataGroup.Add(data);
            }
        }    

     
    }
}
