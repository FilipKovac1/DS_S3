using OSPABA;
using simulation;
using managers;
namespace agents
{
	//meta! id="23"
	public class AT2 : Agent
	{
		public AT2(int id, Simulation mySim, Agent parent) :
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
			new AT2Manager(SimId.AT2Manager, MySim, this);
			AddOwnMessage(Mc.StatReset);
			AddOwnMessage(Mc.EnterPassanger);
			AddOwnMessage(Mc.EnterMinibus);
		}
		//meta! tag="end"
	}
}