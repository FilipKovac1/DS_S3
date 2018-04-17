using System;

namespace Actors
{
    /// <summary>
    /// THis class represents passenger actor in simulation
    /// Inherits IComparable interface because of CompareTo method for sort purposes
    /// </summary>
    class Passenger : IComparable
    {

        public double ArrivalTime { get; set; }
        public double ArrivalTimeCR { get; set; }
        public double GetInMiniTime { get; set; }
        public bool ArrivedAtT1 { get; set; }

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
