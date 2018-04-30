using OSPABA;
using simulation;
using agents;
using System;
using Generator;

namespace continualAssistants
{
	//meta! id="80"
	public class EnterT2 : Scheduler
    {
        private Random Rand = new Random(Generator.Seed.GetSeed());
        private double Lambda;

        public EnterT2(int id, Simulation mySim, CommonAgent myAgent) : base(id, mySim, myAgent)
		{
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
            Lambda = 280;
        }

		//meta! sender="AEnv", id="81", type="Start"
		public void ProcessStart(MessageForm message)
        {
            message.Code = Mc.ProcessPassenger;
            Hold(GenerateEnter(), message);
        }

        private double GenerateEnter()
        {
            return Distributions.GetExp(Rand, Lambda);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
            {
                case Mc.ProcessPassenger:
                    MessageForm m = message.CreateCopy();
                    Hold(GenerateEnter(), m);
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