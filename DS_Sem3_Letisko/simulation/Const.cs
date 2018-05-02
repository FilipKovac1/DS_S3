using System;

namespace simulation
{
    public class Const
    {
        public static int HourToSecond = 3600;
        public static int DayToSecond = 24 * HourToSecond;

        public static readonly int SpeedOfMini = 35;
        public static readonly double[] Distances = new double[] { 2.5, 0.5, 3.4, 0.9, 2.9 }; // in km
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
        public static readonly double TriangularRatio = 0.6; // ratio between Triang1 : Triang2, with which probability i pick from first triangle
        // for that ones who go to rent a car
        public static readonly int[] TriangularIn1 = new int[] { 10, 11, 12 }; // min, max, modus
        public static readonly int[] TriangularIn2 = new int[] { 15, 16, 19 }; // min, max, modus
        // for taht ones who go to return car 
        public static readonly int[] TriangularOut1 = new int[] { 12, 16, 19 }; // min, max, modus
        public static readonly int[] TriangularOut2 = new int[] { 20, 21, 22 }; // min, max, modus

        public static readonly int EnterIntervalCount = 18;
        public static readonly int EnterIntervalSize = 15 * 60;
        public static readonly double[,] EnterExpLambda = new double[,] {
            {
                GetLambda(4),
                GetLambda(8),
                GetLambda(12),
                GetLambda(15),
                GetLambda(18),
                GetLambda(14),
                GetLambda(13),
                GetLambda(10),
                GetLambda(4),
                GetLambda(6),
                GetLambda(10),
                GetLambda(14),
                GetLambda(16),
                GetLambda(15),
                GetLambda(7),
                GetLambda(3),
                GetLambda(4),
                GetLambda(2)
            },{
                GetLambda(3),
                GetLambda(6),
                GetLambda(9),
                GetLambda(15),
                GetLambda(17),
                GetLambda(19),
                GetLambda(14),
                GetLambda(6),
                GetLambda(3),
                GetLambda(4),
                GetLambda(21),
                GetLambda(14),
                GetLambda(19),
                GetLambda(12),
                GetLambda(5),
                GetLambda(2),
                GetLambda(3),
                GetLambda(3)
            }, {
                GetLambda(4),
                GetLambda(8),
                GetLambda(12),
                GetLambda(15),
                GetLambda(18),
                GetLambda(14),
                GetLambda(13),
                GetLambda(10),
                GetLambda(4),
                GetLambda(6),
                GetLambda(10),
                GetLambda(14),
                GetLambda(16),
                GetLambda(15),
                GetLambda(7),
                GetLambda(3),
                GetLambda(4),
                GetLambda(2)
            }
        }; // lambda in seconds of arrivals
        public static readonly double[] EnterIntervalEndTime = new double[] {
            GetEnterEndTime(16.25),
            GetEnterEndTime(16.5),
            GetEnterEndTime(16.75),
            GetEnterEndTime(17),
            GetEnterEndTime(17.25),
            GetEnterEndTime(17.5),
            GetEnterEndTime(17.75),
            GetEnterEndTime(18),
            GetEnterEndTime(18.25),
            GetEnterEndTime(18.5),
            GetEnterEndTime(18.75),
            GetEnterEndTime(19),
            GetEnterEndTime(19.25),
            GetEnterEndTime(19.5),
            GetEnterEndTime(19.75),
            GetEnterEndTime(20),
            GetEnterEndTime(20.25),
            GetEnterEndTime(20.5)
        }; // in seconds

        private static double GetLambda(int enter) => 60 / enter * 60; // number of enters in hour
        private static double GetEnterEndTime(double hour) => hour * HourToSecond;
    }
}
