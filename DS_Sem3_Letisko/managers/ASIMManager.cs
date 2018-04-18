using OSPABA;
using simulation;
using agents;
namespace managers
{
	//meta! id="1"
	public class ASIMManager : Manager
	{
		public ASIMManager(int id, Simulation mySim, Agent myAgent) :
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

		/*!
		 * Enter of customer into simulation on Terminal 1
		 */
		//meta! sender="AEnv", id="13", type="Notice"
		public void ProcessEnterT1(MessageForm message)
		{
		}

		/*!
		 * Enter of customer into simulation on Terminal 2
		 */
		//meta! sender="AEnv", id="12", type="Notice"
		public void ProcessEnterT2(MessageForm message)
		{
		}

		//meta! sender="AAirport", id="26", type="Response"
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
			case Mc.EnterT2:
				ProcessEnterT2(message);
			break;

			case Mc.Passanger:
				ProcessPassanger(message);
			break;

			case Mc.EnterT1:
				ProcessEnterT1(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new ASIM MyAgent
		{
			get
			{
				return (ASIM)base.MyAgent;
			}
		}
	}
}