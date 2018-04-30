using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="108"
	public class Transport : Scheduler
	{
		public Transport(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AMinibus", id="109", type="Start"
		public void ProcessStart(MessageForm message)
        {
            ((MyMessage)message).Minibus.OnWay = true;
            message.Code = Mc.Done;
            Hold(((MyMessage) message).Minibus.GetTime(), message);
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
                case Mc.Done:
                    ((MyMessage)message).Minibus.GoToNextStop();
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
