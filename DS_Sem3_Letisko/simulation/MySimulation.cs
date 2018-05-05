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
		}

        public void SetParams(int Minis_C, int Minis_T, int Empl_C, int Days_C, double Days_S, double Days_E, double HeatUp_L, int Repl_C)
        {
            AMinibus.SetMinis(Minis_C, Minis_T);
            AEmployee.SetEmpl(Empl_C);
            AAirport.DayStart = Days_S;
            AAirport.SetHeatUp(HeatUp_L);

            this.Repl_C = Repl_C;
            this.Repl_Days_C = Days_C;

            Slow = true;
            Slow_interval = 500;
            Slow_duration = 0.0001;
            if (Slow)
                SetSimSpeed(Slow_interval, Slow_duration);
        }

        public void Start() => SimulateAsync(Repl_C);

		protected override void PrepareSimulation()
		{
			base.PrepareSimulation();
            // Create global statistcis
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

            Console.WriteLine(AEnv.temp);

            STimeFromTerminal.AddStat(AEnv.GetReplStats(1));
            STimeFromAirRental.AddStat(AEnv.GetReplStats(2));
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