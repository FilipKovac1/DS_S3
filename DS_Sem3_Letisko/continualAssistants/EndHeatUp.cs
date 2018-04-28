using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="101"
	public class EndHeatUp : Scheduler
	{
        public int HeatUp { get; set; }
		public EndHeatUp(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AAirport", id="102", type="Start"
		public void ProcessStart(MessageForm message)
		{
            message.Code = Mc.Done;
            Hold(HeatUp, message);
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
                case Mc.Done:
                    message.Code = Mc.Finish;
                    AssistantFinished(message);
                    break;
			}
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
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
		public new AAirport MyAgent
		{
			get
			{
				return (AAirport)base.MyAgent;
			}
		}
	}
}