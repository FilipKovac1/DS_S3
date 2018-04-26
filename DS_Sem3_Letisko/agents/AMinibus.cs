using OSPABA;
using simulation;
using managers;
using continualAssistants;
using instantAssistants;
namespace agents
{
	//meta! id="4"
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
			new GetOut(SimId.GetOut, MySim, this);
			new GetIn(SimId.GetIn, MySim, this);
			new AddToFront(SimId.AddToFront, MySim, this);
			new SetActualPosition(SimId.SetActualPosition, MySim, this);
			new GetStats(SimId.GetStats, MySim, this);
			AddOwnMessage(Mc.ResetStat);
			AddOwnMessage(Mc.Move);
			AddOwnMessage(Mc.ProcessPassenger);
		}
		//meta! tag="end"
	}
}