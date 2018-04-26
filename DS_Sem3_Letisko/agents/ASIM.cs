using OSPABA;
using simulation;
using managers;
using continualAssistants;
using instantAssistants;
namespace agents
{
	//meta! id="1"
	public class ASim : Agent
	{
		public ASim(int id, Simulation mySim, Agent parent) :
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
			new ASimManager(SimId.ASimManager, MySim, this);
			AddOwnMessage(Mc.ServePassenger);
			AddOwnMessage(Mc.EnterT1);
			AddOwnMessage(Mc.EnterCR);
			AddOwnMessage(Mc.EnterT2);
		}
		//meta! tag="end"
	}
}