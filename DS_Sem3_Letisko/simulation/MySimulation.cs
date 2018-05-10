using OSPABA;
using agents;
using Statistics;
using System;

namespace simulation
{
	public class MySimulation : Simulation
	{
        private int Repl_C;
        public int Repl_Days_C;
        public bool Slow;
        public double Slow_interval;
        public double Slow_duration;

        private StatTime STimeFromTerminal { get; set; }
        private StatTime STimeFromAirRental { get; set; }

        private StatTime SWaitCR { get; set; }
        private StatTime SWaitCR_Length { get; set; }
        private StatTime SWaitT1 { get; set; }
        private StatTime SWaitT1_Length { get; set; }
        private StatTime SWaitT2 { get; set; }
        private StatTime SWaitT2_Length { get; set; }

        public MySimulation()
		{
			Init();

            STimeFromTerminal = new StatTime();
            STimeFromAirRental = new StatTime();
            SWaitCR = new StatTime();
            SWaitCR_Length = new StatTime();
            SWaitT1 = new StatTime();
            SWaitT1_Length = new StatTime();
            SWaitT2 = new StatTime();
            SWaitT2_Length = new StatTime();
		}

        public void SetParams(int Minis_C, int Minis_T, int Empl_C, int Days_C, double Days_S, double Days_E, double HeatUp_L, int Repl_C, bool slow, double interval, double duration)
        {
            AMinibus.SetMinis(Minis_C, Minis_T);
            AEmployee.SetEmpl(Empl_C);
            AAirport.DayStart = Days_S;
            AAirport.SetHeatUp(HeatUp_L);

            this.Repl_C = Repl_C;
            this.Repl_Days_C = Days_C;

            if (slow)
                SetSpeed(interval, duration);
            else
                SetMaxSpeed();
        }

        public void Start() => SimulateAsync(Repl_C);

        public void SetSpeed(double interval, double duration)
        {
            Slow = true;
            Slow_interval = interval;
            Slow_duration = duration;
            base.SetSimSpeed(Slow_interval, Slow_duration);
        }

        public void SetMaxSpeed()
        {
            Slow = false;
            base.SetMaxSimSpeed();
        }

		protected override void PrepareSimulation()
		{
			base.PrepareSimulation();
            // Create global statistcis

            STimeFromTerminal.Reset();
            STimeFromAirRental.Reset();
            SWaitCR.Reset();
            SWaitCR_Length.Reset();
            SWaitT1.Reset();
            SWaitT1_Length.Reset();
            SWaitT2.Reset();
            SWaitT2_Length.Reset();
        }

        protected override void PrepareReplication()
		{
			base.PrepareReplication();
            // Reset entities, queues, local statistics, etc...

            ASim.InitReplication();
        }

        protected override void ReplicationFinished()
		{
			// Collect local statistics into global, update UI, etc...
			base.ReplicationFinished();

            STimeFromTerminal.AddStat(AEnv.GetStats(1));
            STimeFromAirRental.AddStat(AEnv.GetStats(2));
        }

        protected override void SimulationFinished()
		{
			// Dysplay simulation results
			base.SimulationFinished();
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			ASim = new ASim(SimId.ASim, this, null);
			AEnv = new AEnv(SimId.AEnv, this, ASim);
			AAirport = new AAirport(SimId.AAirport, this, ASim);
			AMinibus = new AMinibus(SimId.AMinibus, this, AAirport);
			AEmployee = new AEmployee(SimId.AEmployee, this, AAirport);
		}
		public ASim ASim
		{ get; set; }
		public AEnv AEnv
		{ get; set; }
		public AAirport AAirport
		{ get; set; }
		public AMinibus AMinibus
		{ get; set; }
		public AEmployee AEmployee
		{ get; set; }
		//meta! tag="end"
	}
}