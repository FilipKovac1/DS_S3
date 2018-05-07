using OSPABA;
using simulation;
using agents;
using Generator;
using Actors;
using System;

namespace continualAssistants
{
	//meta! id="40"
	public class GetOut : Process
	{
        private Passenger localP;
        private Minibus localM;

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
            localM = ((MyMessage)message).Minibus;
            localP = localM.GetFirst();

            message.Code = Mc.Done;
            ((MyMessage)message).Passenger = localP;
            Hold(Distributions.GetNormWithInterval(localM.GetOutRandom, Const.GetOutTime[0], Const.GetOutTime[1]) * localP.SizeOfGroup, message);
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
                case Mc.Done:
                    MessageForm m = message.CreateCopy();
                    ((MyMessage)m).Passenger = null;
                    if (((MyMessage)m).Minibus.IsEmpty())
                        AssistantFinished(m); // minibus is empty
                    else
                        ProcessStart(m);
                    ((MyMessage)message).Minibus = null;
                    AssistantFinished(message); // passenger get out
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