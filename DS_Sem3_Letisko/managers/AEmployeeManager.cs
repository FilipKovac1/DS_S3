using OSPABA;
using simulation;
using agents;
using continualAssistants;
using instantAssistants;
namespace managers
{
	//meta! id="5"
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

		//meta! sender="AAirport", id="26", type="Notice"
		public void ProcessResetStat(MessageForm message)
		{
		}

		//meta! sender="AAirport", id="23", type="Request"
		public void ProcessServePassenger(MessageForm message)
		{
		}

		//meta! sender="ServicePassenger", id="52", type="Finish"
		public void ProcessFinish(MessageForm message)
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
			case Mc.ResetStat:
				ProcessResetStat(message);
			break;

			case Mc.Finish:
				ProcessFinish(message);
			break;

			case Mc.ServePassenger:
				ProcessServePassenger(message);
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