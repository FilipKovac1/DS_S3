using System;

namespace Statistics
{
    /// <summary>
    /// Class represents interval of confidance and 
    /// has implemented method to count it
    /// </summary>
    public class SIntervalOfConfidence
    {
        private double sum;
        private double powSum;
        private int n;
        private const double P_VALUE = 1.654; // value for 90% Interval of confidence

        public SIntervalOfConfidence()
        {
            Reset();
        }

        public void Reset()
        {
            sum = 0;
            n = 0;
        }

        public void AddStat (double e)
        {
            sum += e;
            powSum += Math.Pow(e, 2);
            n++;
        }

        private double GetStandardDeviation ()
        {
            double left = powSum / n;
            double right = Math.Pow((sum / n), 2);

            return left - right;
        }

        private double GetStatAverage () => sum / n;

        public double GetLowerBound () => GetStatAverage() - ((P_VALUE * Math.Sqrt(GetStandardDeviation())) / (Math.Sqrt(n - 1)));

        public double GetHigherBound () => GetStatAverage() + ((P_VALUE * Math.Sqrt(GetStandardDeviation())) / (Math.Sqrt(n - 1)));
    }
}
