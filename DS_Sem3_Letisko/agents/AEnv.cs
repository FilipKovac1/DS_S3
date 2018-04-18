using OSPABA;
using simulation;
using managers;
namespace agents
{
	//meta! id="2"
	public class AEnv : Agent
	{
		public AEnv(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new AEnvManager(SimId.AEnvManager, MySim, this);
			AddOwnMessage(Mc.Init);
			AddOwnMessage(Mc.Leave);
		}
		//meta! tag="end"
	}
}