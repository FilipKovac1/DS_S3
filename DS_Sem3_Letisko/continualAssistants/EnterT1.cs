using OSPABA;
using simulation;
using agents;
using System;
using Generator;

namespace continualAssistants
{
	//meta! id="78"
	public class EnterT1 : Scheduler
	{
        private Random Rand = new Random();
        private double Lambda;

		public EnterT1(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
            this.Lambda = 240;
		}

		//meta! sender="AEnv", id="79", type="Start"
		public void ProcessStart(MessageForm message)
		{
            double nextEnter = Distributions.GetExp(Rand, Lambda);
            Hold(nextEnter, message);
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
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
		public new AEnv MyAgent
		{
			get
			{
				return (AEnv)base.MyAgent;
			}
		}
    }
}