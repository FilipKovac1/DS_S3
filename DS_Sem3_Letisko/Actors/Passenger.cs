using System;

namespace Actors
{
    /// <summary>
    /// THis class represents passenger actor in simulation
    /// Inherits IComparable interface because of CompareTo method for sort purposes
    /// </summary>
    class Passenger : IComparable
    {

        public double ArrivalTime { get; set; } // when the passanger/s arrived to terminal
        public double ArrivalTimeCR { get; set; } // when the passanger/s arrived to CR
        public bool ArrivedAtT1 { get; set; } // on which terminal passanger/s arrived
        public int NumberOfGroup { get; set; } // how much people came together as group

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
