using OSPABA;
using agents;
namespace simulation
{
	public class MySimulation : Simulation
	{
		public MySimulation()
		{
			Init();
		}

		protected override void PrepareSimulation()
		{
			base.PrepareSimulation();
			// Create global statistcis
		}

        protected override void PrepareReplication()
		{
			base.PrepareReplication();
			// Reset entities, queues, local statistics, etc...
		}

        protected override void ReplicationFinished()
		{
			// Collect local statistics into global, update UI, etc...
			base.ReplicationFinished();
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