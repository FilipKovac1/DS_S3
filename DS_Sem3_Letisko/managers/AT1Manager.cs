using OSPABA;
using simulation;
using agents;
namespace managers
{
	//meta! id="22"
	public class AT1Manager : Manager
	{
		public AT1Manager(int id, Simulation mySim, Agent myAgent) :
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

		//meta! sender="AAirport", id="54", type="Notice"
		public void ProcessStatReset(MessageForm message)
		{
		}

		//meta! sender="AAirport", id="27", type="Notice"
		public void ProcessEnterPassanger(MessageForm message)
		{
		}

		//meta! sender="AAirport", id="35", type="Request"
		public void ProcessEnterMinibus(MessageForm message)
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
			case Mc.EnterPassanger:
				ProcessEnterPassanger(message);
			break;

			case Mc.EnterMinibus:
				ProcessEnterMinibus(message);
			break;

			case Mc.StatReset:
				ProcessStatReset(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AT1 MyAgent
		{
			get
			{
				return (AT1)base.MyAgent;
			}
		}
	}
}