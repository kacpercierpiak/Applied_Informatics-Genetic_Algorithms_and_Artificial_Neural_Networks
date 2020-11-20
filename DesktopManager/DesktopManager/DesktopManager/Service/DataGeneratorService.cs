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
        public DataGroup dataGroup { get; private set; }
        public DataGroup neurons { get; private set; }
        public int Point_Densinity { get; set; }
        Random random { get; set; }

        public DataGeneratorService()
        {
            dataGroup = new DataGroup();
            dataGroup.Position = new Position();
            dataGroup.SubPoints = new List<Position>();
            neurons = new DataGroup();
            neurons.Position = new Position();
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

        private bool PostionVerification(Position position, DataGroup dataGroup)
        {
            if (isValidPosition(dataGroup.Position, position, Point_Densinity) || dataGroup.SubPoints.Any(p => isValidPosition(p, position, Point_Densinity)))
                return true;
            else
                return false;        
        }
        private Position GeneratePoint(int min, int max, DataGroup dataGroup)
        {
            var position = new Position();
            do
            {
                position.X = random.Next(min, max);
                position.Y = random.Next(min, max);
            } while (!PostionVerification(position, dataGroup));

            return position;
        }
        

        private List<Position> GenerateSubPoints(Position position, int qty, int stepMin, int stepMax)
        {
            var positionList = new List<Position>();
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
        

        public void Generate(int min, int max, int stepMin, int stepMax, int groupQty, int elementQty)
        {
            dataGroup = new DataGroup();
            dataGroup.Position = new Position();
            dataGroup.SubPoints = new List<Position>();
            neurons = new DataGroup();
            neurons.Position = new Position();

            for (int i = 0; i < groupQty; i++)
            {                
                dataGroup.Position = GeneratePoint(min, max, dataGroup);
                dataGroup.SubPoints = GenerateSubPoints(dataGroup.Position, elementQty, stepMin, stepMax);                
                neurons.Position = GeneratePoint(min, max, neurons);
            }

             
        }
    }
}
