using OSPABA;
using simulation;
using agents;
namespace managers
{
	//meta! id="3"
	public class AAirportManager : Manager
	{
		public AAirportManager(int id, Simulation mySim, Agent myAgent) :
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

		//meta! sender="ASIM", id="16", type="Notice"
		public void ProcessInit(MessageForm message)
		{
		}

		//meta! sender="AT3", id="51", type="Response"
		public void ProcessEnterPassanger(MessageForm message)
		{
		}

		//meta! sender="AEmployee", id="34", type="Response"
		public void ProcessServePassanger(MessageForm message)
		{
		}

		/*!
		 * When passanger come to take a car
		 */
		//meta! sender="ACR", id="33", type="Response"
		public void ProcessEnterToServe(MessageForm message)
		{
		}

		//meta! sender="AMinibus", id="39", type="Response"
		public void ProcessMoveFromT2(MessageForm message)
		{
		}

		//meta! sender="AMinibus", id="41", type="Response"
		public void ProcessMoveFromT1(MessageForm message)
		{
		}

		//meta! sender="AMinibus", id="42", type="Response"
		public void ProcessMoveFromCR(MessageForm message)
		{
		}

		//meta! sender="ACR", id="56", type="Response"
		public void ProcessRequestResponse(MessageForm message)
		{
		}

		//meta! sender="AMinibus", id="58", type="Response"
		public void ProcessMoveFromT3(MessageForm message)
		{
		}

		//meta! sender="AT1", id="35", type="Response"
		public void ProcessEnterMinibusAT1(MessageForm message)
		{
		}

		//meta! sender="AT2", id="37", type="Response"
		public void ProcessEnterMinibusAT2(MessageForm message)
		{
		}

		//meta! sender="AT3", id="29", type="Response"
		public void ProcessEnterMinibusAT3(MessageForm message)
		{
		}

		//meta! sender="ASIM", id="26", type="Request"
		public void ProcessPassanger(MessageForm message)
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
			case Mc.EnterMinibus:
				switch (message.Sender.Id)
				{
				case SimId.AT1:
					ProcessEnterMinibusAT1(message);
				break;

				case SimId.AT2:
					ProcessEnterMinibusAT2(message);
				break;

				case SimId.AT3:
					ProcessEnterMinibusAT3(message);
				break;
				}
			break;

			case Mc.EnterPassanger:
				ProcessEnterPassanger(message);
			break;

			case Mc.Passanger:
				ProcessPassanger(message);
			break;

			case Mc.MoveFromCR:
				ProcessMoveFromCR(message);
			break;

			case Mc.RequestResponse:
				ProcessRequestResponse(message);
			break;

			case Mc.MoveFromT2:
				ProcessMoveFromT2(message);
			break;

			case Mc.MoveFromT3:
				ProcessMoveFromT3(message);
			break;

			case Mc.ServePassanger:
				ProcessServePassanger(message);
			break;

			case Mc.MoveFromT1:
				ProcessMoveFromT1(message);
			break;

			case Mc.EnterToServe:
				ProcessEnterToServe(message);
			break;

			case Mc.Init:
				ProcessInit(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AAirport MyAgent
		{
			get
			{
				return (AAirport)base.MyAgent;
			}
		}
	}
}