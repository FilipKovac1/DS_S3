namespace Statistics
{
    /// <summary>
    /// Class for counting statistics of times
    /// like whole time in simulation, waiting time in front as so on
    /// </summary>
    public class StatTime
    {
        private int count;
        private double sum;

        public StatTime()
        {
            Reset();
        }
        public void Reset()
        {
            count = 0;
            sum = 0;
        }

        public void AddStat (double value)
        {
            count++;
            sum += value;
        }

        public double GetStat() => sum / count; 
    }
}
