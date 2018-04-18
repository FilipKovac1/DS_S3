using OSPABA;
using simulation;
using managers;
namespace agents
{
	//meta! id="1"
	public class ASIM : Agent
	{
		public ASIM(int id, Simulation mySim, Agent parent) :
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
			new ASIMManager(SimId.ASIMManager, MySim, this);
			AddOwnMessage(Mc.EnterT1);
			AddOwnMessage(Mc.EnterT2);
			AddOwnMessage(Mc.Passanger);
		}
		//meta! tag="end"
	}
}