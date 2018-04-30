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
		public GetIn(int id, Simulation mySim, CommonAgent myAgent) : base(id, mySim, myAgent)
		{
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

        //meta! sender="AMinibus", id="39", type="Start"
        public void ProcessStart(MessageForm message)
        {
            Minibus m = ((MyMessage)message).Minibus;
            if (m.IsFull())
            {
                message.Code = Mc.Done;
                Hold(0, message);
            }
            else
            {
                Passenger pass = null;
                bool Go = true;
                foreach (Passenger p in MyAgent.GetQueue(m.State))
                {
                    if (m.GetIn(p))
                    {
                        pass = p;
                        MyAgent.RemoveFromQueue(m.State, p);
                        Go = false;
                        break;
                    }
                }
                if (Go)
                {
                    message.Code = Mc.Done;
                    Hold(0, message);
                }
                else
                {
                    message.Code = Mc.Start;
                    Hold(Distributions.GetNormWithInterval(m.GetInRandom, Const.GetInTime[0], Const.GetInTime[1]) * pass.SizeOfGroup, message);
                }
            }
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