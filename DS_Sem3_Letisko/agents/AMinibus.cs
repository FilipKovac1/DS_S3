using OSPABA;
using simulation;
using managers;
namespace agents
{
	//meta! id="31"
	public class AMinibus : Agent
	{
		public AMinibus(int id, Simulation mySim, Agent parent) :
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
			new AMinibusManager(SimId.AMinibusManager, MySim, this);
			AddOwnMessage(Mc.MoveFromT2);
			AddOwnMessage(Mc.MoveFromT1);
			AddOwnMessage(Mc.MoveFromT3);
			AddOwnMessage(Mc.MoveFromCR);
		}
		//meta! tag="end"
	}
}