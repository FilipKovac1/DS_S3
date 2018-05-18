using Generator;
using OSPABA;
using simulation;
using Statistics;
using System;
using System.Collections.Generic;

namespace Actors
{
    public class Minibus : Entity
    {
        public int Type { get; set; }
        public int Index;
        public bool Stop;
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

        public StatTime OnBoardStat { get; set; }
        public StatTime OnBoardStat_Global { get; set; }

        public Minibus (MySimulation sim, int state, int type, int index) : base (sim)
        {
            Type = type;
            Index = index;
            LastStop = 0;
            State = state;
            OnBoard = new Queue<Passenger>(Const.CapacityOptions[Type]);
            OnBoard_Count = 0;
            OnWay = true;
            Stop = false;
            MileAge = 0;
            OnBoardStat = new StatTime();
            OnBoardStat_Global = new StatTime();
        }

        /// <summary>
        /// Reset minibus data
        /// </summary>
        /// <param name="state"></param>
        public void Reinit (int state)
        {
            State = state;
            Stop = false;
            OnWay = true;
            //GetIn = new Random(Seed.GetSeed());
            //GetOut = new Random(Seed.GetSeed());
            OnBoard.Clear();
            OnBoard_Count = 0;
            LastStop = 0;
            MileAge = 0;
            OnBoardStat.Reset();
        }

        /// <summary>
        /// Add into stats how many people minibus had on board before their leaving
        /// </summary>
        public void AddStatOnBoard()
        {
            if (State == 4)
            {
                if (!IsEmpty())
                    OnBoardStat.AddStat(OnBoard_Count);
            }
            else
                OnBoardStat.AddStat(OnBoard_Count);
        }

        /// <summary>
        /// Indicates if is minibus empty or not
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() => OnBoard_Count == 0;
        /// <summary>
        /// Indicates if the number of passengers on board equals to capacity of minibus
        /// </summary>
        /// <returns></returns>
        public bool IsFull() => OnBoard_Count == GetCapacity();
        /// <summary>
        /// Returns minibus's capacity
        /// </summary>
        /// <returns></returns>
        public int GetCapacity() => Const.CapacityOptions[Type];

        /// <summary>
        /// Add passenger on board
        /// </summary>
        /// <param name="p"></param>
        /// <returns>true if passenger was added, otherwise return false</returns>
        public bool GetIn(Passenger p)
        {
            if (OnBoard_Count + p.SizeOfGroup > GetCapacity())
                return false;
            OnBoard_Count += p.SizeOfGroup;
            OnBoard.Enqueue(p);
            return true;
        }

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
            return (State > 3 && IsEmpty() ? Const.Distances[0] : Const.Distances[State]) - pl;
        }

        /// <summary>
        /// Compute the place of minibus
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string ComputePlace (double time)
        {
            if (OnWay)
                return String.Format("{0,-20} -> {2,20} | {1} km", GetPlaceFromState(), Math.Round(ComputeRoute(time), 2), GetPlaceFromState(State == 1 ? 2 : (State == 2 ? 4 : (State == 3 ? 1 : (IsEmpty() ? 1 : 3)))));
            else
                return String.Format("Staying on {0, -50}", GetPlaceFromState());
        }

        /// <summary>
        /// Get string text of place by parameter, default minibus's state
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private string GetPlaceFromState(int? state = null)
        {
            int s = state ?? State;
            switch (s)
            {
                case 1:
                case 2:
                case 3:
                    return "Terminal " + s;
                case 4:
                    return "AirCar Rental";
                default:
                    return "404 - NotFound";
            }
        }

        /// <summary>
        /// Return actual costs of minibus (just mileage * cost per mile/km)
        /// </summary>
        /// <returns></returns>
        public double GetCosts() => Const.MiniCostPerKM[Type] * MileAge;

        /// <summary>
        /// Get first passenger to go out from a bus
        /// </summary>
        /// <returns>null if bus is empty</returns>
        public Passenger GetFirst()
        {
            if (IsEmpty()) return null;
            Passenger p = OnBoard.Dequeue();
            OnBoard_Count -= p.SizeOfGroup;
            return p;
        }

        /// <summary>
        /// Go to next stop defined by the actual position of minibus
        /// </summary>
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

        /// <summary>
        /// Return time in seconds, how much it takes to next stop
        /// </summary>
        /// <returns></returns>
        public double GetTime()
        {
            return State > 3 ? (IsEmpty() ? Const.DistancesTime[0] : Const.DistancesTime[State]) : Const.DistancesTime[State];
        }

        /// <summary>
        /// To string method of minibus object 
        /// Showing number of passengers on board, capacity, where is now, where he is going, how much he already gone
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public string ToString(double time) => String.Format("{0,2:##0}/{2} passengers. {1, -65} | {3,4:0} km", OnBoard_Count, ComputePlace(time), GetCapacity(), MileAge);
    }
}
