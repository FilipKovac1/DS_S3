using OSPABA;
using simulation;
using System;
using OSPRNG;

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

        public Employee(MySimulation sim, int id) : base (sim) => Index = id;

        public override string ToString()
        {
            return String.Format("{0}", !Free ? "Working" : "Do nothing");
        }
    }
}
