using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopManager.Service
{
    class WinningNeuronModel
    {
        public int Id { get; set; }
        public double? Value { get; set; }
    }
    class Neuron
    {
       public int Id { get; set; }
       public Position Position { get; set; }
        public int Balance { get; set; } = 0;
        public int StopCount { get; set; } = 0;

    }
 
    class WTAService
    {
        public List<DataGroup> dataGroup { get; private set; }
        public List<Position> dataList { get; private set; }
        private List<Neuron> neurons { get; set; }

        public List<DataGroup> neuronsList { get {
                return NeronsToDataGroup();
            } }

        public WTAService(List<DataGroup> dataGroups, List<DataGroup> neurons)
        {
            dataGroup = dataGroups.ToList();
            this.neurons = NeuronsMapping(neurons);
            DataMapping();
        }

        private List<DataGroup> NeronsToDataGroup()
        {
            var value = new List<DataGroup>();
            neurons.ForEach(v => value.Add(new DataGroup() { Position = v.Position }));            
            return value;
        }

        private void DataMapping()
        {
            dataList = new List<Position>();
            dataGroup.ForEach(it => {
                dataList.Add(it.Position);
                it.SubPoints.ForEach(sit => dataList.Add(sit));
            });              

        }

        private List<Neuron> NeuronsMapping (List<DataGroup> neurons)
        {
            var neuronsList = new List<Neuron>();
            for(var i=0; i< neurons.Count;i++)
            {
                neuronsList.Add(new Neuron() { Id = i, Position = new Position() { X = neurons[i].Position.X, Y = neurons[i].Position.Y } });
            }
            neuronsList.OrderBy(x => Guid.NewGuid()).ToList();
            return neuronsList;
        }
        private double SimiliarityCalc(Position neuron, Position iteration)
        {
            var d2 = Math.Pow((iteration.X - neuron.X), 2);
            d2 += Math.Pow((iteration.Y - neuron.Y), 2);
            return Math.Sqrt(d2);
        }
        private WinningNeuronModel winningNeuron(Position iteration, int counter, bool isDisabler, int IterationDisable, int CycleQty)
        {
            WinningNeuronModel winningNeuron = new WinningNeuronModel() { Value = null, Id = 0 };
            neurons.ForEach(n =>
            {
                if (isDisabler && (CycleQty == 0 || counter < CycleQty) && n.StopCount <= IterationDisable && n.StopCount > 0)
                {

                    n.StopCount++;

                }
                else
                {
                    n.StopCount = 0;
                    var d = SimiliarityCalc(n.Position, iteration) + n.Balance;


                    if (winningNeuron.Value == null || d < winningNeuron.Value)
                    {
                        winningNeuron.Value = d;
                        winningNeuron.Id = n.Id;
                    }
                }

            });
            return winningNeuron;
        }

        private void GetNewWeight(WinningNeuronModel winningNeuron, Position iteration, double LearnRation, int balance)
        {
            var position = neurons.Find(p => p.Id == winningNeuron.Id).Position;

            position.X = (int) Math.Round(position.X + (LearnRation * (iteration.X - position.X)), 3);
            position.Y = (int) Math.Round(position.Y + (LearnRation * (iteration.Y - position.Y)), 3);

            neurons.Find(p => p.Id == winningNeuron.Id).Position = new Position() { X = position.X, Y = position.Y };
            neurons.Find(p => p.Id == winningNeuron.Id).Balance += balance;
            neurons.Find(p => p.Id == winningNeuron.Id).StopCount = 1;


        }

        public void Calc(double LearnRation, int balance = 0, bool isDisabler = false, int IterationDisable = 0, int CycleQty =0)
        {
            int counter = 0;
            dataList.ForEach(it => {
                WinningNeuronModel winningNeuron = this.winningNeuron(it, counter, isDisabler, IterationDisable, CycleQty);
                GetNewWeight(winningNeuron,it, LearnRation, balance);
                counter++;
            });
        }


    }
}
