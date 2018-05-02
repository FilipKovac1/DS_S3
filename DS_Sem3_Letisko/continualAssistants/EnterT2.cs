using OSPABA;
using simulation;
using agents;
using System;

namespace continualAssistants
{
	//meta! id="80"
	public class EnterT2 : Scheduler
    {
        private Random Rand = new Random(Generator.Seed.GetSeed());

        public EnterT2(int id, Simulation mySim, CommonAgent myAgent) : base(id, mySim, myAgent)
		{
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
        }

		//meta! sender="AEnv", id="81", type="Start"
		public void ProcessStart(MessageForm message)
        {
            message.Code = Mc.Done;
            Hold(MyAgent.GetEnter(Rand, 2), message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
            {
                case Mc.Done:
                    double l = MyAgent.GetEnter(Rand, 2);
                    if (l >= 0)
                    {
                        MessageForm m = message.CreateCopy();
                        Hold(l, m);
                    }
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
		public new AEnv MyAgent
		{
			get
			{
				return (AEnv)base.MyAgent;
			}
		}
	}
}