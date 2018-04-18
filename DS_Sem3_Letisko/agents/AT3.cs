using OSPABA;
using simulation;
using managers;
namespace agents
{
	//meta! id="24"
	public class AT3 : Agent
	{
		public AT3(int id, Simulation mySim, Agent parent) :
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
			new AT3Manager(SimId.AT3Manager, MySim, this);
			AddOwnMessage(Mc.EnterPassanger);
			AddOwnMessage(Mc.EnterMinibus);
		}
		//meta! tag="end"
	}
}