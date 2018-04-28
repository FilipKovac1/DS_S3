using OSPABA;
using simulation;
using agents;
using Actors;
using continualAssistants;
using instantAssistants;
namespace managers
{
	//meta! id="1"
	public class ASimManager : Manager
	{
		public ASimManager(int id, Simulation mySim, Agent myAgent) :
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

		//meta! sender="AEnv", id="13", type="Notice"
		public void ProcessEnterT1(MessageForm message)
		{
		}

		//meta! sender="AEnv", id="12", type="Notice"
		public void ProcessEnterCR(MessageForm message)
		{
		}

		//meta! sender="AEnv", id="11", type="Notice"
		public void ProcessEnterT2(MessageForm message)
		{
		}

		//meta! sender="AAirport", id="19", type="Response"
		public void ProcessServePassenger(MessageForm message)
		{
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
                case Mc.Init:
                    // start planning enters
                    message.Addressee = MySim.FindAgent(SimId.AEnv);
                    Notice(message);

                    MessageForm m = message.CreateCopy();
                    m.Addressee = MySim.FindAgent(SimId.AAirport);
                    Notice(m);
                    break;
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
			case Mc.ServePassenger:
				ProcessServePassenger(message);
			break;

			case Mc.EnterT1:
				ProcessEnterT1(message);
			break;

			case Mc.EnterCR:
				ProcessEnterCR(message);
			break;

			case Mc.EnterT2:
				ProcessEnterT2(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new ASim MyAgent
		{
			get
			{
				return (ASim)base.MyAgent;
			}
		}
	}
}