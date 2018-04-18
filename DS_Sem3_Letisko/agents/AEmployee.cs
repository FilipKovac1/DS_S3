using OSPABA;
using simulation;
using managers;
namespace agents
{
	//meta! id="32"
	public class AEmployee : Agent
	{
		public AEmployee(int id, Simulation mySim, Agent parent) :
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
			new AEmployeeManager(SimId.AEmployeeManager, MySim, this);
			AddOwnMessage(Mc.ServePassanger);
		}
		//meta! tag="end"
	}
}