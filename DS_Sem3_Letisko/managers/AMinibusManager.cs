using OSPABA;
using simulation;
using agents;
using continualAssistants;
using instantAssistants;
namespace managers
{
	//meta! id="4"
	public class AMinibusManager : Manager
	{
		public AMinibusManager(int id, Simulation mySim, Agent myAgent) :
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

		//meta! sender="AAirport", id="25", type="Notice"
		public void ProcessResetStat(MessageForm message)
		{
		}

		/*!
		 * move of bus
		 */
		//meta! sender="AAirport", id="21", type="Request"
		public void ProcessMove(MessageForm message)
		{
		}

		//meta! sender="GetIn", id="39", type="Finish"
		public void ProcessFinishGetIn(MessageForm message)
		{
		}

		//meta! sender="GetOut", id="41", type="Finish"
		public void ProcessFinishGetOut(MessageForm message)
		{
		}

		/*!
		 * Request - Enter to front to wait for a bus
		 * Response - leave a bus
		 */
		//meta! sender="AAirport", id="58", type="Request"
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
			case Mc.Finish:
				switch (message.Sender.Id)
				{
				case SimId.GetOut:
					ProcessFinishGetOut(message);
				break;

				case SimId.GetIn:
					ProcessFinishGetIn(message);
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
		public new AMinibus MyAgent
		{
			get
			{
				return (AMinibus)base.MyAgent;
			}
		}
	}
}