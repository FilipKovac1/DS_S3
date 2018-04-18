using OSPABA;
using simulation;
using agents;
namespace managers
{
	//meta! id="32"
	public class AEmployeeManager : Manager
	{
		public AEmployeeManager(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication

			if (PetriNet != null)
			{
				PetriNet.Clear();
			}
		}

		//meta! sender="AAirport", id="34", type="Request"
		public void ProcessServePassanger(MessageForm message)
		{
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.ServePassanger:
				ProcessServePassanger(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AEmployee MyAgent
		{
			get
			{
				return (AEmployee)base.MyAgent;
			}
		}
	}
}