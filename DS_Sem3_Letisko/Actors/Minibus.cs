using Generator;
using OSPABA;
using simulation;
using System;
using System.Collections.Generic;

namespace Actors
{
    public class Minibus : Entity
    {
        public int Type { get; set; }
        public int Index;
        /// <summary>
        /// 1 - T1
        /// 2 - T2
        /// 3 - T3
        /// 4 - CR
        /// </summary>
        public int State { get; set; }
        public bool OnWay { get; set; }
        public double MileAge { get; set; }
        public Queue<Passenger> OnBoard;


        public double LastStop;
        public Random GetIn = new Random(Seed.GetSeed());
        public Random GetOut = new Random(Seed.GetSeed());

        public Minibus (MySimulation sim, int state, int type, int index) : base (sim)
        {
            Type = type;
            Index = index;
            LastStop = 0;
            State = state;
            OnBoard = new Queue<Passenger>(Const.CapacityOptions[Type]);
            OnWay = true;
            MileAge = 0;
        }

        public void Reinit (int state)
        {
            State = state;
            OnWay = true;
            //GetIn = new Random(Seed.GetSeed());
            //GetOut = new Random(Seed.GetSeed());
            OnBoard.Clear();
            LastStop = 0;
            MileAge = 0;
        }

        public bool IsEmpty() => OnBoard.Count == 0;
        public bool IsFull() => OnBoard.Count == Const.CapacityOptions[Type];

        /// <summary>
        /// Comute how much time takes a way long <paramref name="route"/> in seconds
        /// </summary>
        /// <param name="route">in kilometers</param>
        /// <returns></returns>
        public double ComputeRoute (double time)
        {
            // copmute where minibus is now
            double pl = Const.SpeedOfMini * ((time - LastStop) / 3600);
            // compute difference from destination
            return (State > 3 ? (IsEmpty() ? Const.Distances[0] : Const.Distances[State]) : Const.Distances[State]) - pl;
        }

        private string ComputePlace (double time)
        {
            if (OnWay)
            {
                return String.Format("{0} -> {2} | {1} km", GetPlaceFromState(), Math.Round(ComputeRoute(time), 2), GetPlaceFromState(State < 3 ? State + 1 : (State == 3 ? 1 : 3)));
            } else
            {
                return String.Format("Staining on {0}", GetPlaceFromState());
            }
        }

        private string GetPlaceFromState(int? state = null)
        {
            state = state.HasValue ? state : State;
            switch (state.Value)
            {
                case 1:
                case 2:
                case 3:
                    return "Terminal " + State;
                case 4:
                    return "AirCar Rental";
                default:
                    return "404 - NotFound";
            }
        }

        private double GetCosts() => Const.MiniCostPerKM[Type] * MileAge;

        public Passenger GetFirst()
        {
            if (IsEmpty()) return null;
            return OnBoard.Dequeue();
        }

        public void GoToNextStop()
        {
            if (State == 4 && !IsEmpty())
                State = 3;
            else if (State == 4 || State == 3)
                State = 1;
            else if (State == 2)
                State = 4;
            else
                State = 2;
        }

        public double GetTime()
        {
            return State > 3 ? (IsEmpty() ? Const.DistancesTime[0] : Const.DistancesTime[State]) : Const.DistancesTime[State];
        }

        public string ToString(double time) => String.Format("{0,2:##}/{2} passengers. {1}", OnBoard.Count, ComputePlace(time), Const.CapacityOptions[Type]);
    }
}
