using OSPABA;
using simulation;
using agents;
namespace managers
{
	//meta! id="30"
	public class ACRManager : Manager
	{
		public ACRManager(int id, Simulation mySim, Agent myAgent) :
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

		//meta! sender="AAirport", id="52", type="Notice"
		public void ProcessStatReset(MessageForm message)
		{
		}

		//meta! sender="AAirport", id="55", type="Notice"
		public void ProcessEnterToGo(MessageForm message)
		{
		}

		/*!
		 * When passanger come to take a car
		 */
		//meta! sender="AAirport", id="33", type="Request"
		public void ProcessEnterToServe(MessageForm message)
		{
		}

		//meta! sender="AAirport", id="56", type="Request"
		public void ProcessRequestResponse(MessageForm message)
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
			case Mc.StatReset:
				ProcessStatReset(message);
			break;

			case Mc.EnterToGo:
				ProcessEnterToGo(message);
			break;

			case Mc.RequestResponse:
				ProcessRequestResponse(message);
			break;

			case Mc.EnterToServe:
				ProcessEnterToServe(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new ACR MyAgent
		{
			get
			{
				return (ACR)base.MyAgent;
			}
		}
	}
}