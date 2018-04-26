using OSPABA;
using simulation;
using managers;
using continualAssistants;
using instantAssistants;
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

        public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new AAirportManager(SimId.AAirportManager, MySim, this);
			AddOwnMessage(Mc.ServePassenger);
			AddOwnMessage(Mc.ResetStat);
			AddOwnMessage(Mc.Init);
			AddOwnMessage(Mc.Move);
			AddOwnMessage(Mc.ProcessPassenger);
		}
		//meta! tag="end"
	}
}