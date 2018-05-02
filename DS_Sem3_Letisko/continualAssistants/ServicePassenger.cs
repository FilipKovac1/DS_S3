using OSPABA;
using simulation;
using agents;
using System;
using Generator;

namespace continualAssistants
{
	//meta! id="51"
	public class ServicePassenger : Process
	{
        private Random TriangProb;
        private double time_to_hold = 0;

        public ServicePassenger(int id, Simulation mySim, CommonAgent myAgent) : base(id, mySim, myAgent)
		{
            TriangProb = new Random(Seed.GetSeed());
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AEmployee", id="52", type="Start"
		public void ProcessStart(MessageForm message)
		{
            message.Code = Mc.Done;
            bool passCR = ((MyMessage)message).Passenger.ArrivedAt == 3;
            if (TriangProb.NextDouble() <= (passCR ? Const.TriangularOutRatio : Const.TriangularInRatio))
                time_to_hold = Distributions.GetTriangular(((MyMessage)message).Employee.Random, !passCR ? Const.TriangularIn1 : Const.TriangularOut1);
            else
                time_to_hold = Distributions.GetTriangular(((MyMessage)message).Employee.Random, !passCR ? Const.TriangularIn2 : Const.TriangularOut2);
            Hold(time_to_hold, message);
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
		public new AEmployee MyAgent
		{
			get
			{
				return (AEmployee)base.MyAgent;
			}
		}
	}
}