using OSPABA;
using simulation;
using managers;
namespace agents
{
	//meta! id="30"
	public class ACR : Agent
	{
		public ACR(int id, Simulation mySim, Agent parent) :
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
			new ACRManager(SimId.ACRManager, MySim, this);
			AddOwnMessage(Mc.StatReset);
			AddOwnMessage(Mc.EnterToGo);
			AddOwnMessage(Mc.EnterToServe);
			AddOwnMessage(Mc.RequestResponse);
		}
		//meta! tag="end"
	}
}