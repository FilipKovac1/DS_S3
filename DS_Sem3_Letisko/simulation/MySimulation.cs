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

		public override void PrepareSimulation()
		{
			base.PrepareSimulation();
			// Create global statistcis
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Reset entities, queues, local statistics, etc...
		}

		public override void ReplicationFinished()
		{
			// Collect local statistics into global, update UI, etc...
			base.ReplicationFinished();
		}

		public override void SimulationFinished()
		{
			// Dysplay simulation results
			base.SimulationFinished();
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			ASIM = new ASIM(SimId.ASIM, this, null);
			AEnv = new AEnv(SimId.AEnv, this, ASIM);
			AAirport = new AAirport(SimId.AAirport, this, ASIM);
			AT1 = new AT1(SimId.AT1, this, AAirport);
			AT2 = new AT2(SimId.AT2, this, AAirport);
			AT3 = new AT3(SimId.AT3, this, AAirport);
			ACR = new ACR(SimId.ACR, this, AAirport);
			AMinibus = new AMinibus(SimId.AMinibus, this, AAirport);
			AEmployee = new AEmployee(SimId.AEmployee, this, AAirport);
		}
		public ASIM ASIM 
		{ get; set; }
		public AEnv AEnv
		{ get; set; }
		public AAirport AAirport
		{ get; set; }
		public AT1 AT1
		{ get; set; }
		public AT2 AT2
		{ get; set; }
		public AT3 AT3
		{ get; set; }
		public ACR ACR
		{ get; set; }
		public AMinibus AMinibus
		{ get; set; }
		public AEmployee AEmployee
		{ get; set; }
		//meta! tag="end"
	}
}