using OSPABA;
using simulation;
using agents;
namespace managers
{
	//meta! id="31"
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

		//meta! sender="AAirport", id="39", type="Request"
		public void ProcessMoveFromT2(MessageForm message)
		{
		}

		//meta! sender="AAirport", id="41", type="Request"
		public void ProcessMoveFromT1(MessageForm message)
		{
		}

		//meta! sender="AAirport", id="58", type="Request"
		public void ProcessMoveFromT3(MessageForm message)
		{
		}

		//meta! sender="AAirport", id="42", type="Request"
		public void ProcessMoveFromCR(MessageForm message)
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
			case Mc.MoveFromT2:
				ProcessMoveFromT2(message);
			break;

			case Mc.MoveFromT3:
				ProcessMoveFromT3(message);
			break;

			case Mc.MoveFromCR:
				ProcessMoveFromCR(message);
			break;

			case Mc.MoveFromT1:
				ProcessMoveFromT1(message);
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