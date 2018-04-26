using OSPABA;
using simulation;
using agents;
using continualAssistants;
using instantAssistants;
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

		//meta! sender="ASim", id="17", type="Notice"
		public void ProcessResetStat(MessageForm message)
		{
		}

		//meta! sender="ASim", id="18", type="Notice"
		public void ProcessInit(MessageForm message)
		{
		}

		//meta! sender="AEmployee", id="23", type="Response"
		public void ProcessServePassengerAEmployee(MessageForm message)
		{
		}

		//meta! sender="ASim", id="19", type="Request"
		public void ProcessServePassengerASim(MessageForm message)
		{
		}

		/*!
		 * move of bus
		 */
		//meta! sender="AMinibus", id="21", type="Response"
		public void ProcessMove(MessageForm message)
		{
		}

		/*!
		 * Request - Enter to front to wait for a bus
		 * Response - leave a bus
		 */
		//meta! sender="AMinibus", id="58", type="Response"
		public void ProcessProcessPassenger(MessageForm message)
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
			case Mc.Init:
				ProcessInit(message);
			break;

			case Mc.ServePassenger:
				switch (message.Sender.Id)
				{
				case SimId.ASim:
					ProcessServePassengerASim(message);
				break;

				case SimId.AEmployee:
					ProcessServePassengerAEmployee(message);
				break;
				}
			break;

			case Mc.Move:
				ProcessMove(message);
			break;

			case Mc.ProcessPassenger:
				ProcessProcessPassenger(message);
			break;

			case Mc.ResetStat:
				ProcessResetStat(message);
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