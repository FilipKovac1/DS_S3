using OSPABA;
using simulation;
using agents;
namespace managers
{
	//meta! id="2"
	public class AEnvManager : Manager
	{
		public AEnvManager(int id, Simulation mySim, Agent myAgent) :
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
		 * Init message to start creating of customers with arrival time
		 */
		//meta! sender="ASIM", id="14", type="Notice"
		public void ProcessInit(MessageForm message)
		{
		}

		/*!
		 * Leave of customer from the simulation
		 */
		//meta! sender="ASIM", id="15", type="Notice"
		public void ProcessLeave(MessageForm message)
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

			case Mc.Leave:
				ProcessLeave(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AEnv MyAgent
		{
			get
			{
				return (AEnv)base.MyAgent;
			}
		}
	}
}