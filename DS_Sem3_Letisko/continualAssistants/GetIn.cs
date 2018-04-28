using OSPABA;
using simulation;
using agents;
using Generator;
using Actors;

namespace continualAssistants
{
	//meta! id="38"
	public class GetIn : Process
	{
		public GetIn(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

        //meta! sender="AMinibus", id="39", type="Start"
        public void ProcessStart(MessageForm message)
        {
            /// TODO enter of group check passengers if fit into bus
            if (true)
            {
                message.Code = Mc.Done;
                Hold(0, message);
            }
            else
            {
                Passenger p = null;
                message.Code = Mc.Start;
                Hold(Distributions.GetNormWithInterval(((MyMessage)message).Minibus.GetIn, Const.GetInTime[0], Const.GetInTime[1]) * p.SizeOfGroup, message);
            }
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
		public new AMinibus MyAgent
		{
			get
			{
				return (AMinibus)base.MyAgent;
			}
		}
	}
}