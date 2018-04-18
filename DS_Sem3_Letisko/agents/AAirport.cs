using OSPABA;
using simulation;
using managers;
namespace agents
{
	//meta! id="3"
	public class AAirport : Agent
	{
		public AAirport(int id, Simulation mySim, Agent parent) :
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
			new AAirportManager(SimId.AAirportManager, MySim, this);
			AddOwnMessage(Mc.Init);
			AddOwnMessage(Mc.EnterPassanger);
			AddOwnMessage(Mc.ServePassanger);
			AddOwnMessage(Mc.EnterToServe);
			AddOwnMessage(Mc.MoveFromT2);
			AddOwnMessage(Mc.MoveFromT1);
			AddOwnMessage(Mc.MoveFromCR);
			AddOwnMessage(Mc.RequestResponse);
			AddOwnMessage(Mc.MoveFromT3);
			AddOwnMessage(Mc.EnterMinibus);
			AddOwnMessage(Mc.Passanger);
		}
		//meta! tag="end"
	}
}