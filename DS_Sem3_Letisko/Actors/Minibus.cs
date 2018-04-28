using DS_Sem3_Letisko;
using Generator;
using OSPABA;
using simulation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Actors
{
    public class Minibus : Entity
    {
        public int Capacity { get; set; } = 0;
        private readonly int capacity1 = 12, capacity2 = 18, capacity3 = 22;
        public readonly double speed = 35; // average speed of minibus in km/h
        public int Index;
        /// <summary>
        /// 1 - T1
        /// 2 - T2_TIME
        /// 3 - CR
        /// </summary>
        public int State;
        public List<Passenger> OnBoard;
        public double LastStop;
        public Random GetIn = new Random(Seed.GetSeed());
        public Random GetOut = new Random(Seed.GetSeed());

        public Minibus (MySimulation sim, int state, int cap, int index) : base (sim)
        {
            Capacity = cap > 2 ? capacity3 : cap > 1 ? capacity2 : capacity1;
            Index = index;
            LastStop = 0;
            State = state;
            OnBoard = new List<Passenger>(Capacity);
        }

        public void Reinit (int state)
        {
            State = state;
            //GetIn = new Random(Seed.GetSeed());
            //GetOut = new Random(Seed.GetSeed());
            OnBoard.Clear();
            LastStop = 0;
        }

        public bool IsEmpty ()
        {
            return OnBoard.Count == 0;
        }

        public bool IsFull ()
        {
            return OnBoard.Count == Capacity;
        }

        /// <summary>
        /// Comute how much time takes a way long <paramref name="route"/> in seconds
        /// </summary>
        /// <param name="route">in kilometers</param>
        /// <returns></returns>
        public double ComputeRoute (double time)
        {
            // copmute where minibus is now
            double pl = speed * ((time - LastStop) / 60 / 60);
            // compute difference from destination
            /// TODO don't forget
            return 0;
            //return -pl + (State > 2 ? Simulation.cr_t1 : (State > 1 ? Simulation.t2_cr : Simulation.t1_t2));
        }

        private string ComputePlace (double time)
        {
            string s1 = State > 2 ? "Car rental" : "Teminal " + State;
            string s2 = State > 2 ? "Terminal 1" : State > 1 ? "Car rental" : "Terminal 2";

            return String.Format("{0} -> {2} | {1} km", s1, Math.Round(ComputeRoute(time), 2), s2);
        }

        public Passenger GetFirst()
        {
            if (IsEmpty()) return null;
            Passenger c = OnBoard.First();
            OnBoard.Remove(c);
            return c;
        }

        public string ToString(double time)
        {
            return String.Format("{0} passengers. {1}", OnBoard.Count, ComputePlace(time));
        }
    }
}
