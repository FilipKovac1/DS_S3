using System;

namespace Generator
{
    /// <summary>
    /// Class contains method to get random variables of different probability distribution
    /// </summary>
    static class Distributions
    {
        /// <summary>
        /// Exponential(e)
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static double GetExp(Random rand, double e) => (-e) * Math.Log(1 - rand.NextDouble());

        public static double GetTriangular(Random rand, int[] values) // values = {min, max, modus}
        {
            double mid = (values[2] - values[0]) / (values[1] - values[0]);
            double r = rand.NextDouble();
            return r < mid ? values[0] + Math.Sqrt(r * (values[1] - values[0]) * (values[2] - values[0])) : values[1] - Math.Sqrt((1 - r) * (values[1] - values[0]) * (values[1] - values[2]));
        }

        /// <summary>
        /// Norm(e, +-)
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="e"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
        public static int GetNormWithInterval(Random rand, int e, int pm) => rand.Next(2 * pm + 1) + (e - pm);
    }
}
