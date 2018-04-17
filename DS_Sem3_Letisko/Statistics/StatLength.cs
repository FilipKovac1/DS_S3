namespace Statistics
{
    /// <summary>
    /// This class represents statistics for length of fronts
    /// Add this before removing from front and before adding into
    /// </summary>
    class StatLength
    {
        private double sum;
        private double lastTimeUpd;

        public StatLength ()
        {
            Reset();
        }

        public void Reset ()
        {
            sum = 0;
            lastTimeUpd = 0;
        }

        public void AddStat (double time, double value)
        {
            sum += (time - lastTimeUpd) * value;
            lastTimeUpd = time;
        }

        public double GetStat (double time) => sum / time;
        public double GetStat() => sum / lastTimeUpd;
    }
}
