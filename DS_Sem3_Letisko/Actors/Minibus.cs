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
        public int OnBoard_Count { get; set; }
        
        public double LastStop;
        public Random GetInRandom = new Random(Seed.GetSeed());
        public Random GetOutRandom = new Random(Seed.GetSeed());

        public Minibus (MySimulation sim, int state, int type, int index) : base (sim)
        {
            Type = type;
            Index = index;
            LastStop = 0;
            State = state;
            OnBoard = new Queue<Passenger>(Const.CapacityOptions[Type]);
            OnBoard_Count = 0;
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
            OnBoard_Count = 0;
            LastStop = 0;
            MileAge = 0;
        }

        public bool IsEmpty() => OnBoard_Count == 0;
        public bool IsFull() => OnBoard_Count == GetCapacity();
        public int GetCapacity() => Const.CapacityOptions[Type];

        /// <summary>
        /// Comute how much time takes a way long <paramref name="route"/> in seconds
        /// </summary>
        /// <param name="route">in kilometers</param>
        /// <returns></returns>
        public double ComputeRoute (double time)
        {
            // copmute where minibus is now
            double pl = Const.SpeedOfMini * ((time - LastStop) / Const.HourToSecond);
            // compute difference from destination
            return (State > 3 ? (IsEmpty() ? Const.Distances[0] : Const.Distances[State]) : Const.Distances[State]) - pl;
        }

        public bool GetIn (Passenger p)
        {
            if (OnBoard_Count + p.SizeOfGroup > GetCapacity())
                return false;
            OnBoard_Count += p.SizeOfGroup;
            OnBoard.Enqueue(p);
            return true;
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
            Passenger p = OnBoard.Dequeue();
            OnBoard_Count -= p.SizeOfGroup;
            return p;
        }

        public void GoToNextStop()
        {
            LastStop = MySim.CurrentTime;
            if (State == 4 && !IsEmpty())
            {
                MileAge += Const.Distances[State];
                State = 3;
            }
            else if (State == 4 || State == 3)
            {
                MileAge += Const.Distances[State == 4 ? 0 : 3];
                State = 1;
            }
            else if (State == 2)
            {
                MileAge += Const.Distances[State];
                State = 4;
            }
            else
            {
                MileAge += Const.Distances[State];
                State = 2;
            }
        }

        public double GetTime()
        {
            return State > 3 ? (IsEmpty() ? Const.DistancesTime[0] : Const.DistancesTime[State]) : Const.DistancesTime[State];
        }

        public string ToString(double time) => String.Format("{0,2:##}/{2} passengers. {1} | {3}", OnBoard_Count, ComputePlace(time), GetCapacity(), MileAge);
    }
}
