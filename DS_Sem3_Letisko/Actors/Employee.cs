using Generator;
using OSPABA;
using simulation;
using System;

namespace Actors
{
    /// <summary>
    /// This class represents actor employee in simulation
    /// </summary>
    public class Employee : Entity
    {
        public Random random = new Random(Seed.GetSeed());
        public bool Free = true;
        public int Index { get; }

        public Employee(MySimulation sim, int id) : base (sim) => Index = id;

        public override string ToString()
        {
            return String.Format("{0}", !Free ? "Working" : "Do nothing");
        }

        internal void Reinit(bool free)
        {
            Free = free;
            //random = new Random(Seed.GetSeed());
        }
    }
}
