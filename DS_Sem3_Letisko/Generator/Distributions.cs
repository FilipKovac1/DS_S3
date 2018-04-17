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
        public static double GetExp(Random rand, double e)
        {
            return (-e) * Math.Log(1 - rand.NextDouble());
        }

        /// <summary>
        /// Norm(e, +-)
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="e"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
        public static int GetNormWithInterval(Random rand, int e, int pm)
        {
            return rand.Next(2 * pm + 1) + (e - pm);
        }
    }
}
