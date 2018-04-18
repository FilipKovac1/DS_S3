using OSPABA;
using simulation;
using managers;
namespace agents
{
	//meta! id="22"
	public class AT1 : Agent
	{
		public AT1(int id, Simulation mySim, Agent parent) :
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
			new AT1Manager(SimId.AT1Manager, MySim, this);
			AddOwnMessage(Mc.StatReset);
			AddOwnMessage(Mc.EnterPassanger);
			AddOwnMessage(Mc.EnterMinibus);
		}
		//meta! tag="end"
	}
}