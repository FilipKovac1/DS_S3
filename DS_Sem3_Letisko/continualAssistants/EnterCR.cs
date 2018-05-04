using OSPABA;
using simulation;
using agents;
using System;

namespace continualAssistants
{
	//meta! id="82"
	public class EnterCR : Scheduler
    {
        private Random Rand = new Random(Generator.Seed.GetSeed());

        public EnterCR(int id, Simulation mySim, CommonAgent myAgent) : base(id, mySim, myAgent)
		{
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
		}

		//meta! sender="AEnv", id="83", type="Start"
		public void ProcessStart(MessageForm message)
        {
            message.Code = Mc.Done; // here its still 0 cause its just init
            Hold(MyAgent.GetEnter(Rand, 3), message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
                case Mc.Done:
                    double l = MyAgent.GetEnter(Rand, 3);
                    if (l >= 0) {
                        MessageForm m = message.CreateCopy();
                        Hold(l, m);
                    } else
                        MyAgent.Generate = false;
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