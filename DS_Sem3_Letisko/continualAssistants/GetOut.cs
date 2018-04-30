using OSPABA;
using simulation;
using agents;
using Generator;

namespace continualAssistants
{
	//meta! id="40"
	public class GetOut : Process
	{
		public GetOut(int id, Simulation mySim, CommonAgent myAgent) : base(id, mySim, myAgent)
		{
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AMinibus", id="41", type="Start"
		public void ProcessStart(MessageForm message)
		{
            message.Code = Mc.Done;
            Hold(Distributions.GetNormWithInterval(((MyMessage)message).Minibus.GetOutRandom, Const.GetOutTime[0], Const.GetOutTime[1]) * ((MyMessage)message).Passenger.SizeOfGroup, message);
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
                case Mc.Done:
                    AssistantFinished(message);
                    break;
			}
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public override void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.Start:
				ProcessStart(message);
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