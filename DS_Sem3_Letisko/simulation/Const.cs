﻿namespace simulation
{
    public class Const
    {
        public static readonly int SpeedOfMini = 35;
        public static readonly double[] Distances = new double[] { 2.5, 0.5, 3.4, 0.9, 2.9}; // in km
        public static readonly double[] DistancesTime = new double[] { 
            Distances[0] / (double)SpeedOfMini * 3600,
            Distances[1] / (double)SpeedOfMini * 3600,
            Distances[2] / (double)SpeedOfMini * 3600,
            Distances[3] / (double)SpeedOfMini * 3600,
            Distances[4] / (double)SpeedOfMini * 3600
        }; // in second
        public static readonly int[] CapacityOptions = new int[] { 12, 18, 30 };
        public static readonly double[] MiniCostPerKM = new double[] { 0.28, 0.43, 0.54 }; 
        public static readonly double[] GroupSizeCumProb = new double[] { 0.4, 0.2, 0.05 };
        public static readonly double RiderSalary = 12.5;
        public static readonly double ServiceSalary = 11.5;
        public static readonly int[] GetInTime = new int[] { 12, 2 };
        public static readonly int[] GetOutTime = new int[] { 6, 4 };
    }
}
