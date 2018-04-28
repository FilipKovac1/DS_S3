using OSPABA;
using Actors;
using Statistics;
using simulation;
using agents;
using continualAssistants;
using instantAssistants;
using System;

namespace managers
{
	//meta! id="2"
	public class AEnvManager : Manager
	{
        private Random random = new Random(Generator.Seed.GetSeed());

        public AEnvManager(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
			Init();
        }

        public override void PrepareReplication()
		{
			if (PetriNet != null)
			{
				PetriNet.Clear();
			}
		}

		//meta! sender="ASim", id="29", type="Notice"
		public void ProcessResetStat(MessageForm message)
		{
		}

		//meta! sender="ASim", id="15", type="Notice"
		public void ProcessInit(MessageForm message)
		{
            MyMessage m1 = (MyMessage)message.CreateCopy();
            m1.Addressee = MyAgent.FindAssistant(SimId.EnterT1);
            StartContinualAssistant(m1);
            MyMessage m2 = (MyMessage)message.CreateCopy();
            m2.Addressee = MyAgent.FindAssistant(SimId.EnterT2);
            StartContinualAssistant(m2);
            MyMessage m3 = (MyMessage)message.CreateCopy();
            m3.Addressee = MyAgent.FindAssistant(SimId.EnterCR);
            StartContinualAssistant(m3);
        }

		//meta! sender="ASim", id="14", type="Notice"
		public void ProcessLeaveT3(MessageForm message)
        {
            MyAgent.AddToStat(2, (MyMessage)message);
        }

		//meta! sender="ASim", id="16", type="Notice"
		public void ProcessLeaveCR(MessageForm message)
		{
            MyAgent.AddToStat(1, (MyMessage)message);
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! sender="EnterT2", id="81", type="Finish"
		public void ProcessFinishEnterT2(MessageForm message)
        {
            ((MyMessage)message).Passenger = new Passenger((MySimulation)MySim, 2, GetSizeOfGroup());
            MyAgent.IncrementEnter(1);

            message.Code = Mc.EnterT2;
            message.Addressee = MySim.FindAgent(SimId.ASim);
            Notice(message);
        }

		//meta! sender="EnterT1", id="79", type="Finish"
		public void ProcessFinishEnterT1(MessageForm message)
		{
            ((MyMessage)message).Passenger = new Passenger((MySimulation)MySim, 1, GetSizeOfGroup());
            MyAgent.IncrementEnter(1);

            message.Code = Mc.EnterT1;
            message.Addressee = MySim.FindAgent(SimId.ASim);
            Notice(message);
        }

		//meta! sender="EnterCR", id="83", type="Finish"
		public void ProcessFinishEnterCR(MessageForm message)
        {
            ((MyMessage)message).Passenger = new Passenger((MySimulation)MySim, 3, GetSizeOfGroup());
            MyAgent.IncrementEnter(3);

            message.Code = Mc.EnterCR;
            message.Addressee = MySim.FindAgent(SimId.ASim);
            Notice(message);
        }

        private int GetSizeOfGroup()
        {
            int ret = 1;
            double prob = random.NextDouble();
            if (prob <= 0.4)
            {
                if (prob <= 0.2)
                {
                    if (prob <= 0.05)
                        ret++;
                    ret++;
                }
                ret++;
            }
            ret++;
            return ret;
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.LeaveT3:
				ProcessLeaveT3(message);
			break;

			case Mc.Init:
				ProcessInit(message);
			break;

			case Mc.LeaveCR:
				ProcessLeaveCR(message);
			break;

			case Mc.Finish:
				switch (message.Sender.Id)
				{
				case SimId.EnterT2:
					ProcessFinishEnterT2(message);
				break;

				case SimId.EnterT1:
					ProcessFinishEnterT1(message);
				break;

				case SimId.EnterCR:
					ProcessFinishEnterCR(message);
				break;
				}
			break;

			case Mc.ResetStat:
				ProcessResetStat(message);
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