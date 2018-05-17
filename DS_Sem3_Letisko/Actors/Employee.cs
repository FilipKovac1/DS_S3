using OSPABA;
using simulation;
using System;
using OSPRNG;
using Statistics;

namespace Actors
{
    /// <summary>
    /// This class represents actor employee in simulation
    /// </summary>
    public class Employee : Entity
    {
        public TriangularRNG RandomIn1 = new TriangularRNG(Const.TriangularIn1[0], Const.TriangularIn1[2], Const.TriangularIn1[1], Const.Seed );
        public TriangularRNG RandomIn2 = new TriangularRNG(Const.TriangularIn2[0], Const.TriangularIn2[2], Const.TriangularIn2[1], Const.Seed );
        public TriangularRNG RandomOut1 = new TriangularRNG(Const.TriangularOut1[0], Const.TriangularOut1[2], Const.TriangularOut1[1], Const.Seed );
        public TriangularRNG RandomOut2 = new TriangularRNG(Const.TriangularOut2[0], Const.TriangularOut2[2], Const.TriangularOut2[1], Const.Seed );

        public bool Free = true;
        public int Index { get; }
        public double TimeOfWorking = 0;
        public StatTime Workload { get; set; }

        public Employee(MySimulation sim, int id) : base(sim) { Index = id; Workload = new StatTime(); }

        public void ResetEmployee()
        {
            Free = true;
            TimeOfWorking = 0;
        }

        public double GetWorkload(double time) // actual replication
        {
            if (time <= 0 || TimeOfWorking <= 0) return 0;
            return (TimeOfWorking / time) * 100;
        }

        public string ToString(double time)
        {
            time -= ((MySimulation)MySim).AAirport.DayStart;
            return String.Format("{0,10} | {1,6:0.00}%", !Free ? "Working" : "Do nothing", GetWorkload(time));
        }
    }
}
