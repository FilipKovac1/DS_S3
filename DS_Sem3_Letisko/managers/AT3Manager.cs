using OSPABA;
using simulation;
using agents;
namespace managers
{
	//meta! id="24"
	public class AT3Manager : Manager
	{
		public AT3Manager(int id, Simulation mySim, Agent myAgent) :
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

		//meta! sender="AAirport", id="51", type="Request"
		public void ProcessEnterPassanger(MessageForm message)
		{
		}

		//meta! sender="AAirport", id="29", type="Request"
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
			case Mc.EnterMinibus:
				ProcessEnterMinibus(message);
			break;

			case Mc.EnterPassanger:
				ProcessEnterPassanger(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AT3 MyAgent
		{
			get
			{
				return (AT3)base.MyAgent;
			}
		}
	}
}