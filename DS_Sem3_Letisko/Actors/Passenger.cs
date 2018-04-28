using System;
using OSPABA;
using simulation;

namespace Actors
{
    /// <summary>
    /// THis class represents passenger actor in simulation
    /// Inherits IComparable interface because of CompareTo method for sort purposes
    /// </summary>
    public class Passenger : Entity, IComparable 
    {

        public double ArrivalTime { get; set; } // when the passanger/s arrived to terminal or cr to return car
        public double ArrivalTimeCR { get; set; } // when the passanger/s arrived to CR
        public int ArrivedAt { get; set; } // on which terminal passanger/s arrived
        public int SizeOfGroup { get; set; } // how much people came together as group

        public Passenger (MySimulation sim, int ArrivedAt, int SizeOfGroup) : base (sim)
        {
            this.ArrivedAt = ArrivedAt;
            ArrivalTime = sim.CurrentTime;
            this.SizeOfGroup = SizeOfGroup;
        }

        public int CompareTo (object o)
        {
            Passenger c = (Passenger)o;
            if (this.ArrivalTime > c.ArrivalTime)
            {
                return -1;
            } else if (this.ArrivalTime < c.ArrivalTime)
            {
                return 1;
            }
            return 0;
        }
    }
}
